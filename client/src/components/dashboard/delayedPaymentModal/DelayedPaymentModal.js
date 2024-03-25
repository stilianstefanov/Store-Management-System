import styles from './DelayedPaymentModal.module.css'
import ReactLoading from 'react-loading'
import { useState, useEffect, useCallback } from 'react';
import { toast } from 'react-toastify'
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../context/AuthContext';
import DashboardClient from '../../client/DashboardClient';
import InsufficientCreditModal from './InsufficientCreditModal/InsufficientCreditModal';
import * as ClientService from '../../../services/clientService'
import * as PurchaseService from '../../../services/purchaseService'

function DelayedPaymentModal(props) {
    const [clients, setClients] = useState([]);
    const [currentsearchTerm, setCurrentSearchTerm] = useState("");
    const [selectedClientId, setSelectedClientId] = useState(null);
    const [isLoading, setIsLoading] = useState(false);
    const [creditModalIsOpen, setCreditModalIsOpen] = useState(false);
    const { logout } = useAuth();
    const navigate = useNavigate();

    const handleError = useCallback((error) => {
        if (error.response && error.response.status === 401) {
            logout();
            navigate('/login');
            toast.warning('Your session has expired. Please login again.');
        } else {
            toast.error(error.response ? error.response.data : "An error occurred");
        }
        console.error(error);
    }, [logout, navigate]);

    useEffect(() => {
        const getInitialClients = async () => {
            setIsLoading(true);
            try {
                const response = await ClientService.GetAll();
                setClients(response.clients);
            } catch (error) {
                handleError(error);
            } finally {
                setIsLoading(false);
            }
        };

        getInitialClients();
    }, [handleError]);

    const searchClientsHandler = async (event) => {
        const searchTerm = event.target.value;
        setCurrentSearchTerm(searchTerm);

        if (searchTerm.trim()) {
            setIsLoading(true);
            try {
                const requestParams = { searchTerm };
                const response = await ClientService.GetAll(requestParams);
                setClients(response.clients);
            } catch (error) {
                handleError(error);
            } finally {
                setIsLoading(false);
            }
        }
    };

    const confirmHandler = async () => {
        if (!selectedClientId) {
            toast.warning('Please select a client!');
            return;
        }
        let selectedClient = clients.find(c => c.id === selectedClientId);
        let creditLeft = selectedClient.creditLimit - selectedClient.currentCredit;
        if (calculateTotalCost(props.products) > creditLeft) {
            setCreditModalIsOpen(true);
            return;
        }
       
        try {
            setIsLoading(true);
            await PurchaseService.CreatePurchase(props.products, selectedClientId);
            props.clearProducts();
            props.closeDPaymentModal();
            toast.success("Successful operation!");
        } catch (error) {
            handleError(error);
        } finally {
            setIsLoading(false);
        }
    }

    const selectClientHandler = (clientId) => {
        setSelectedClientId(clientId);
    };

    const calculateTotalCost = (productsArr) => {
        return productsArr.reduce((total, product) => total + product.price * product.quantity, 0);
    }

    return (
        <div>
            <div className={styles['modal']}>
                <h1 className={styles['header']}>Select client</h1>
                <input
                    type='text'
                    value={currentsearchTerm}
                    placeholder='Search'
                    className={`form-control ${styles.input}`}
                    onChange={searchClientsHandler}
                />
                <div className={`table-responsive ${styles['table-wrapper']}`}>
                    <table className={styles.tableCustom}>
                        <thead className={styles.tableHeader}>
                            <tr>
                                <th>Name</th>
                                <th>Surname</th>
                                <th>Lastname</th>
                                <th>Credit</th>
                                <th>Limit</th>
                            </tr>
                        </thead>
                        <tbody>
                            {isLoading ? (
                                <tr>
                                    <td colSpan="5">
                                        <div className={styles['loading-container']}>
                                            <ReactLoading type="spin" color="#808080" />
                                        </div>
                                    </td>
                                </tr>
                            ) : (
                                clients.map(client => (
                                    <DashboardClient
                                        key={client.id}
                                        client={client}
                                        isSelected={selectedClientId === client.id}
                                        select={selectClientHandler}
                                    />
                                ))
                            )}
                        </tbody>
                    </table>
                </div>
                <div className={styles['buttons-container']}>
                    <button className={styles['button-cancel']} onClick={props.closeDPaymentModal} >
                        Cancel
                    </button>
                    <button className={styles['button-confirm']} onClick={confirmHandler}>
                        Confirm
                    </button>
                </div>
            </div>
            <div className={styles['backdrop']} />
            {creditModalIsOpen && (<InsufficientCreditModal 
                closeCreditModal={() => setCreditModalIsOpen(false)}
                client={clients.find(c => c.id === selectedClientId)}
                totalCost={calculateTotalCost(props.products)}
            />)}
        </div>
    );
}

export default DelayedPaymentModal;