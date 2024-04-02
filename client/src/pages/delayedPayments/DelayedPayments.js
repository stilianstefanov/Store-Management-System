import { useState, useEffect, useCallback } from 'react';
import { toast } from 'react-toastify'
import { useNavigate } from 'react-router-dom';
import ReactLoading from 'react-loading'
import { useAuth } from '../../context/AuthContext';
import styles from './DelayedPayments.module.css'
import TableClient from '../../components/client/TableClient/TableClient';
import * as ClientService from '../../services/clientService'

function DelayedPaymentsPage() {
    const [clients, setClients] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(0);
    const [clientsPerPage, setClientsPerPage] = useState(10);
    const [searchTerm, setSearchTerm] = useState("");
    const [orderBy, setOrderBy] = useState(0);
    const [isLoading, setIsLoading] = useState(false);
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
                orderBy
            };
            const response = await ClientService.GetAll(request);
            setClients(response.clients);
            setTotalPages(response.totalPages);
        } catch (error) {
            handleError(error);
        } finally {
            setIsLoading(false);
        }
    }, [handleError, currentPage, clientsPerPage, orderBy, searchTerm]);

    useEffect(() => {
        getClients();
    }, [getClients]);

    return (
        <div className={`container ${styles['table-container']}`}>
            <h1 className={`text-center ${styles['title']}`}>Clients</h1>
            <div className="d-flex justify-content-center flex-wrap">
                <div className={styles['input-group']}>
                    <label htmlFor="search-input">Search:</label>
                    <input
                        id="search-input"
                        type="text"
                        value={searchTerm}
                        placeholder="Search client"
                        className={`form-control ${styles['input-field']}`}
                        onChange={(e) => setSearchTerm(e.target.value)}
                    />
                </div>
                <div className={styles['input-group']}>
                    <label htmlFor="order-select">Sort by:</label>
                    <select
                        id="order-select"
                        className={`form-control ${styles['input-field']}`}
                        onChange={(e) => setOrderBy(e.target.value)}>
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
                        onChange={(e) => setClientsPerPage(e.target.value)}>
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
                {Array.from({ length: Math.min(5, totalPages) }, (_, index) => {
                    const pageNumber = index + 1;
                    const isCurrentPage = pageNumber === currentPage;
                    return (
                        <button
                            className={`${styles['page-button']} ${isCurrentPage ? styles['current-page'] : ''}`}
                            key={pageNumber}
                            onClick={() => setCurrentPage(pageNumber)}>
                            {pageNumber}
                        </button>
                    );
                })}
                {totalPages > 5 && (
                    <>
                        <span>...</span>
                        <button
                            className={`${styles['page-button']} ${currentPage === totalPages ? styles['current-page'] : ''}`}
                            onClick={() => setCurrentPage(totalPages)}>
                            {totalPages}
                        </button>
                    </>
                )}
                {currentPage < totalPages && (
                    <button
                        onClick={() => setCurrentPage(currentPage + 1)}
                        className={styles['page-control-button']}>
                        Next
                    </button>
                )}
            </div>
        </div>
    );
}

export default DelayedPaymentsPage;