import { useState, useEffect, useCallback } from 'react';
import { toast } from 'react-toastify'
import { useNavigate } from 'react-router-dom';
import ReactLoading from 'react-loading'
import { useAuth } from '../../../context/AuthContext';
import styles from './ClientDetails.module.css';
import * as PurchaseService from '../../../services/purchaseService';

function ClientDetails({ client, closeClientDetails, refreshClients }) {
    const [purchases, setPurchases] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(0);
    const [isLoading, setIsLoading] = useState(false);
    const [date, setDate] = useState("");
    const [sorting, setSorting] = useState(0);
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

    return (
        <div>
            <div className={styles['modal']}>
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
                            <p>Current credit: <span className={styles['list-info-span']}>{client.currentCredit}</span></p>
                        </li>
                        <li>
                            <p>Credit limit: <span className={styles['list-info-span']}>{client.creditLimit}</span></p>
                        </li>
                    </ul>
                    <div className={styles['buttons-container']}>
                        <button className={styles['update-button']}>Update</button>
                        <button className={styles['decr-credit-button']}>Decrease credit</button>
                        <button className={styles['delete-button']}>Delete</button>
                    </div>
                </div>
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
                            {purchases.map(p => {
                                return (
                                    <tr>
                                        <td>{p.date}</td>
                                        <td>{p.amount}</td>
                                    </tr>
                                )
                            })}
                        </tbody>
                    </table>
                </div>
            </div>
            <div className={styles['backdrop']} />
        </div>
    );
};

export default ClientDetails;