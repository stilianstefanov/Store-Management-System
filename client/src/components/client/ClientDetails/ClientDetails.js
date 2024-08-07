import { useState, useEffect, useCallback } from 'react';
import { toast } from 'react-toastify'
import { useNavigate } from 'react-router-dom';
import ReactLoading from 'react-loading'
import { useAuth } from '../../../context/AuthContext';
import styles from './ClientDetails.module.css';
import TablePurchase from '../../purchase/TablePurchase/TablePurchase';
import ClientForm from '../ClientForm/ClientForm'
import DecreaseCreditModal from './DecreaseCreditModal/DecreaseCreditModal';
import DeleteClientModal from './DeleteClientModal/DeleteClientModal';
import PurchaseDetails from '../../purchase/PurchaseDetails/PurchaseDetails';
import * as PurchaseService from '../../../services/purchaseService';
import { useTranslation } from 'react-i18next';

function ClientDetails({ client, closeClientDetails, refreshClients }) {
    const [purchases, setPurchases] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(0);
    const [isLoading, setIsLoading] = useState(false);
    const [date, setDate] = useState("");
    const [sorting, setSorting] = useState(0);
    const [clientFormIsOpen, setclientFormIsOpen] = useState(false);
    const [decreaseCreditModalIsOpen, setDecreaseCreditModalIsOpen] = useState(false);
    const [deleteModalIsOpen, setDeleteModalIsOpen] = useState(false);
    const [purchaseDetailsIsOpen, setPurchaseDetailsIsOpen] = useState(false);
    const [selectedPurchaseId, setSelectedPurchaseId] = useState("");
    const navigate = useNavigate();
    const { logout } = useAuth();
    const { t } = useTranslation();

    const handleError = useCallback((error) => {
        if (error.response && error.response.status === 401) {
            logout();
            navigate('/login');
            toast.warning(t('common.sessionExp'));
        } else {
            toast.error(error.response ? error.response.data : "An error occurred");
        }
        console.error(error);
    }, [logout, navigate, t]);

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

    const openPurchaseDetailsHandler = (purchaseId) => {
        setSelectedPurchaseId(purchaseId);
        setPurchaseDetailsIsOpen(true);
    };

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
                                <p>{t('clientDetails.name')} <span className={styles['list-info-span']}>{client.name}</span></p>
                            </li>
                            <li>
                                <p>{t('clientDetails.surname')} <span className={styles['list-info-span']}>{client.surname ? client.surname : 'N/A'}</span></p>
                            </li>
                            <li>
                                <p>{t('clientDetails.lastname')} <span className={styles['list-info-span']}>{client.lastName}</span></p>
                            </li>
                            <li>
                                <p>{t('clientDetails.currCredit')} <span className={styles['list-info-span']}>{client.currentCredit.toFixed(2)}</span></p>
                            </li>
                            <li>
                                <p>{t('clientDetails.limit')} <span className={styles['list-info-span']}>{client.creditLimit.toFixed(2)}</span></p>
                            </li>
                        </ul>
                        <div className={styles['buttons-container']}>
                            <button
                                className={styles['update-button']}
                                onClick={() => setclientFormIsOpen(true)}>
                                {t('clientDetails.update')}
                            </button>
                            <button
                                className={styles['decr-credit-button']}
                                onClick={() => setDecreaseCreditModalIsOpen(true)}>
                                {t('clientDetails.decrCredit')}
                            </button>
                            <button
                                className={styles['delete-button']}
                                onClick={() => setDeleteModalIsOpen(true)}>
                                {t('clientDetails.delete')}
                            </button>
                        </div>
                        <button
                            className={styles['close-button']}
                            onClick={() => closeClientDetails()}>
                            {t('clientDetails.close')}
                        </button>
                    </div>
                </div>
                <div className={styles['purchases-wrapper']}>
                    <h2 className={styles['list-info-span']}>{t('clientDetails.purchases.header')}</h2>
                    <div className="d-flex justify-content-center flex-wrap">
                        <div className={styles['input-group']}>
                            <label htmlFor="select-date">{t('clientDetails.purchases.dateLabel')}</label>
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
                            <label htmlFor="order-select">{t('clientDetails.purchases.sortLabel')}</label>
                            <select
                                id="order-select"
                                className={`form-control ${styles['input-field']}`}
                                onChange={(e) => {
                                    setCurrentPage(1);
                                    setSorting(e.target.value);
                                }}
                            >
                                <option value="0">{t('clientDetails.purchases.sortInput.dateLatest')}</option>
                                <option value="1">{t('clientDetails.purchases.sortInput.dateOldest')}</option>
                                <option value="2">{t('clientDetails.purchases.sortInput.amountDesc')}</option>
                                <option value="3">{t('clientDetails.purchases.sortInput.amountAsc')}</option>
                            </select>
                        </div>
                    </div>
                    <div className={`table-responsive ${styles['table-wrapper']}`}>
                        <table className={styles['table-fill']}>
                            <thead>
                                <tr>
                                    <th className={styles['text-left']}>{t('clientDetails.purchases.table.date')}</th>
                                    <th className={styles['text-left']}>{t('clientDetails.purchases.table.amount')}</th>
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
                                            openPurchaseDetails={openPurchaseDetailsHandler}
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
            {deleteModalIsOpen && <DeleteClientModal
                clientId={client.id}
                closeModal={() => setDeleteModalIsOpen(false)}
                refreshClients={() => refreshClients()}
                closeClientDetails={() => closeClientDetails()} />}
            {purchaseDetailsIsOpen && <PurchaseDetails
                clientId={client.id}
                purchase={purchases.find(p => p.id === selectedPurchaseId)}
                refreshClients={() => refreshClients()}
                closePurchaseDetails={() => setPurchaseDetailsIsOpen(false)} />}
        </div>
    );
};

export default ClientDetails;