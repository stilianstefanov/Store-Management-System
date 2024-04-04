import { useState, useEffect, useCallback } from 'react';
import { toast } from 'react-toastify'
import { useNavigate } from 'react-router-dom';
import ReactLoading from 'react-loading'
import { useAuth } from '../../context/AuthContext';
import styles from './DelayedPayments.module.css'
import TableClient from '../../components/client/TableClient/TableClient';
import ClientForm from '../../components/client/ClientForm/ClientForm'
import * as ClientService from '../../services/clientService'

function DelayedPaymentsPage() {
    const [clients, setClients] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(0);
    const [clientsPerPage, setClientsPerPage] = useState(10);
    const [searchTerm, setSearchTerm] = useState("");
    const [sorting, setSorting] = useState(0);
    const [isLoading, setIsLoading] = useState(false);
    const [clientFormIsOpen, setClientFormIsOpen] = useState(false);
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

    const getClients = useCallback(async () => {
        setIsLoading(true);
        try {
            const request = {
                currentPage,
                clientsPerPage,
                searchTerm,
                sorting
            };
            const response = await ClientService.GetAll(request);
            setClients(response.clients);
            setTotalPages(response.totalPages);
        } catch (error) {
            handleError(error);
        } finally {
            setIsLoading(false);
        }
    }, [handleError, currentPage, clientsPerPage, sorting, searchTerm]);

    useEffect(() => {
        getClients();
    }, [getClients]);

    const PAGE_BUTTONS_DISPLAY_LIMIT = 5;
    const startPage = Math.max(currentPage - Math.floor(PAGE_BUTTONS_DISPLAY_LIMIT / 2), 1);
    const endPage = Math.min(startPage + PAGE_BUTTONS_DISPLAY_LIMIT - 1, totalPages);

    const pageNumbers = Array.from({ length: (endPage - startPage) + 1 }, (_, index) => startPage + index);

    return (
        <div className={`container ${styles['table-container']}`}>
            <div className={styles['header-container']}>
                <div className={styles['spacer']}></div>
                <h1 className={`text-center ${styles['title']}`}>Clients</h1>
                <button
                    className={styles['add-client-button']}
                    onClick={() => setClientFormIsOpen(true)}>
                    Add New Client
                </button>
            </div>
            <div className="d-flex justify-content-center flex-wrap">
                <div className={styles['input-group']}>
                    <label htmlFor="search-input">Search:</label>
                    <input
                        id="search-input"
                        type="text"
                        value={searchTerm}
                        placeholder="Search client"
                        className={`form-control ${styles['input-field']}`}
                        onChange={(e) => {
                            setCurrentPage(1);
                            setSearchTerm(e.target.value);
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
                        }}>
                        <option value="0">Name (Ascending)</option>
                        <option value="1">Name (Descending)</option>
                        <option value="2">Current credit (Descending)</option>
                        <option value="3">Current credit (Ascending)</option>
                        <option value="4">Credit limit (Descending)</option>
                        <option value="5">Credit limit (Ascending)</option>
                    </select>
                </div>
                <div className={styles['input-group']}>
                    <label htmlFor="order-select">Clients per Page :</label>
                    <select
                        id="order-select"
                        className={`form-control ${styles['input-field']}`}
                        onChange={(e) => {
                            setCurrentPage(1);
                            setClientsPerPage(e.target.value)
                        }}>
                        <option value="10">10</option>
                        <option value="15">15</option>
                        <option value="20">20</option>
                    </select>
                </div>
            </div>
            <div className={`table-responsive ${styles['table-wrapper']}`}>
                <table className={styles['table-fill']}>
                    <thead>
                        <tr>
                            <th className={styles['text-left']}>Name</th>
                            <th className={styles['text-left']}>Surname</th>
                            <th className={styles['text-left']}>Lastname</th>
                            <th className={styles['text-left']}>Current credit</th>
                            <th className={styles['text-left']}>Credit limit</th>
                        </tr>
                    </thead>
                    <tbody className={styles['table-hover']}>
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
                                <TableClient
                                    key={client.id}
                                    client={client}
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
            {clientFormIsOpen && <ClientForm
                closeAddNewClient={() => setClientFormIsOpen(false)}
                refreshClients={() => getClients()} />}
        </div>
    );
}

export default DelayedPaymentsPage;