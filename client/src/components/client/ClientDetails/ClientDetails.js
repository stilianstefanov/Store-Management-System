import { useState, useEffect, useCallback } from 'react';
import { toast } from 'react-toastify'
import { useNavigate } from 'react-router-dom';
import ReactLoading from 'react-loading'
import { useAuth } from '../../../context/AuthContext';
import styles from './ClientDetails.module.css';
import TablePurchase from '../../purchase/TablePurchase/TablePurchase';
import ClientForm from '../ClientForm/ClientForm'
import DecreaseCreditModal from './DecreaseCreditModal/DecreaseCreditModal';
import * as PurchaseService from '../../../services/purchaseService';

function ClientDetails({ client, closeClientDetails, refreshClients }) {
    const [purchases, setPurchases] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(0);
    const [isLoading, setIsLoading] = useState(false);
    const [date, setDate] = useState("");
    const [sorting, setSorting] = useState(0);
    const [clientFormIsOpen, setclientFormIsOpen] = useState(false);
    const [decreaseCreditModalIsOpen, setDecreaseCreditModalIsOpen] = useState(false);
    const navigate = useNavigate();
    const { logout } = useAuth();

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

    const getPurchases = useCallback(async () => {
        setIsLoading(true);
        try {
            const request = {
                currentPage,
                date,
                sorting
            };
            const response = await PurchaseService.GetAll(client.id, request);
            setPurchases(response.purchases);
            setTotalPages(response.totalPages);
        } catch (error) {
            handleError(error);
        } finally {
            setIsLoading(false);
        }
    }, [handleError, client, currentPage, date, sorting]);

    useEffect(() => {
        getPurchases();
    }, [getPurchases]);

    const PAGE_BUTTONS_DISPLAY_LIMIT = 5;
    const startPage = Math.max(currentPage - Math.floor(PAGE_BUTTONS_DISPLAY_LIMIT / 2), 1);
    const endPage = Math.min(startPage + PAGE_BUTTONS_DISPLAY_LIMIT - 1, totalPages);

    const pageNumbers = Array.from({ length: (endPage - startPage) + 1 }, (_, index) => startPage + index);

    return (
        <div>
            <div className={styles['modal']}>
                <div className={styles['client-info-wrapper']}>
                    <div className={styles['flex-container']}>
                        <ul className={styles['list-info']}>
                            <li>
                                <p>Name: <span className={styles['list-info-span']}>{client.name}</span></p>
                            </li>
                            <li>
                                <p>Surname: <span className={styles['list-info-span']}>{client.surname ? client.surname : 'N/A'}</span></p>
                            </li>
                            <li>
                                <p>Lastname: <span className={styles['list-info-span']}>{client.lastName}</span></p>
                            </li>
                            <li>
                                <p>Current credit: <span className={styles['list-info-span']}>{client.currentCredit.toFixed(2)}</span></p>
                            </li>
                            <li>
                                <p>Credit limit: <span className={styles['list-info-span']}>{client.creditLimit.toFixed(2)}</span></p>
                            </li>
                        </ul>
                        <div className={styles['buttons-container']}>
                            <button
                                className={styles['update-button']}
                                onClick={() => setclientFormIsOpen(true)}>
                                Update
                            </button>
                            <button
                                className={styles['decr-credit-button']}
                                onClick={() => setDecreaseCreditModalIsOpen(true)}>
                                Decrease credit
                            </button>
                            <button className={styles['delete-button']}>Delete</button>
                        </div>
                        <button
                            className={styles['close-button']}
                            onClick={() => closeClientDetails()}>
                            Close
                        </button>
                    </div>
                </div>
                <div className={styles['purchases-wrapper']}>
                    <h2 className={styles['list-info-span']}>Purchases</h2>
                    <div className="d-flex justify-content-center flex-wrap">
                        <div className={styles['input-group']}>
                            <label htmlFor="select-date">Date:</label>
                            <input
                                id="select-date"
                                type="date"
                                placeholder="Select date"
                                className={`form-control ${styles['input-field']}`}
                                onChange={(e) => {
                                    setCurrentPage(1);
                                    setDate(e.target.value);
                                }}
                            />
                        </div>
                        <div className={styles['input-group']}>
                            <label htmlFor="order-select">Sort by:</label>
                            <select
                                id="order-select"
                                className={`form-control ${styles['input-field']}`}
                                onChange={(e) => {
                                    setCurrentPage(1);
                                    setSorting(e.target.value);
                                }}
                            >
                                <option value="0">Date (Latest)</option>
                                <option value="1">Date (Oldest)</option>
                                <option value="2">Amount (Descending)</option>
                                <option value="3">Amount (Ascending)</option>
                            </select>
                        </div>
                    </div>
                    <div className={`table-responsive ${styles['table-wrapper']}`}>
                        <table className={styles['table-fill']}>
                            <thead>
                                <tr>
                                    <th className={styles['text-left']}>Date</th>
                                    <th className={styles['text-left']}>Amount</th>
                                </tr>
                            </thead>
                            <tbody className={styles['table-hover']}>
                                {isLoading ? (
                                    <tr>
                                        <td colSpan="2">
                                            <div className={styles['loading-container']}>
                                                <ReactLoading type="spin" color="#808080" />
                                            </div>
                                        </td>
                                    </tr>
                                ) : (
                                    purchases.map(purchase => (
                                        <TablePurchase
                                            key={purchase.id}
                                            purchase={purchase}
                                        //Todo Implement purchase details
                                        />
                                    ))
                                )}
                            </tbody>
                        </table>
                    </div>
                    <div className={styles['buttons-wrapper']}>
                        {currentPage > 1 && (
                            <button
                                onClick={() => setCurrentPage(currentPage - 1)}
                                className={styles['page-control-button']}>
                                Prev
                            </button>
                        )}
                        {pageNumbers.map(pageNumber => (
                            <button
                                className={`${styles['page-button']} ${currentPage === pageNumber ? styles['current-page'] : ''}`}
                                key={pageNumber}
                                onClick={() => setCurrentPage(pageNumber)}
                            >
                                {pageNumber}
                            </button>
                        ))}
                        {currentPage < totalPages && (
                            <button
                                onClick={() => setCurrentPage(currentPage + 1)}
                                className={styles['page-control-button']}>
                                Next
                            </button>
                        )}
                    </div>
                </div>
            </div>
            <div className={styles['backdrop']} />
            {clientFormIsOpen && <ClientForm
                client={client}
                closeAddNewClient={() => setclientFormIsOpen(false)}
                refreshClients={() => refreshClients()} />}
            {decreaseCreditModalIsOpen && <DecreaseCreditModal
                client={client}
                closeModal={() => setDecreaseCreditModalIsOpen(false)}
                refreshClients={() => refreshClients()} />}
        </div>
    );
};

export default ClientDetails;