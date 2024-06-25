import styles from './Gmv.module.css';
import { useState, useEffect, useCallback } from 'react';
import ReactLoading from 'react-loading';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import TransactionsTable from '../../components/transaction/TransactionsTable/TransactionsTable';
import TransactionsDailyTotalsTable from '../../components/transaction/TransactionsDailyTotalsTable/TransactionsDailyTotalsTable';
import TransactionsMonthlyTotalsTable from '../../components/transaction/TransactionsMonthlyTotalsTable/TransactionsMonthlyTotalsTable';
import * as GmvService from '../../services/gmvService';
import { useTranslation } from 'react-i18next';

function GmvPage() {
    const [transactions, setTransactions] = useState([]);
    const [transactionsDailyTotals, setTransactionsDailyTotals] = useState([]);
    const [transactionMonthlyTotals, settransactionMonthlyTotals] = useState([]);
    const [period, setPeriod] = useState('day');
    const [date, setDate] = useState(new Date());
    const [currentPage, setCurrentPage] = useState(1);
    const [itemsPerPage, setItemsPerPage] = useState(10);
    const [totalPages, setTotalPages] = useState(0);
    const [totalGmv, setTotalGmv] = useState(0);
    const [totalRegularGmv, setTotalRegularGmv] = useState(0);
    const [totalDelayedGmv, setTotalDelayedGmv] = useState(0);
    const [isLoading, setIsLoading] = useState(false);
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
    }, [logout, navigate]);

    const getTransactionsData = useCallback(async () => {
        setIsLoading(true);
        try {
            const request = {
                currentPage,
                itemsPerPage,
                period,
                date: date.toISOString()
            };
            const response = await GmvService.GetTransactionsData(request);
            if (period === 'day') {
                setTransactions(response.transactions);
            } else if (period === 'month') {
                setTransactionsDailyTotals(response.transactionDailyTotals);
            } else if (period === 'year') {
                settransactionMonthlyTotals(response.transactionMonthlyTotals);
            }
            setTotalGmv(response.totalGmv);
            setTotalRegularGmv(response.totalRegularGmv);
            setTotalDelayedGmv(response.totalDelayedGmv);
            setTotalPages(response.totalPages);
        } catch (error) {
            handleError(error);
        } finally {
            setIsLoading(false);
        }
    }, [handleError, currentPage, itemsPerPage, period, date]);

    useEffect(() => {
        getTransactionsData();
    }, [getTransactionsData]);

    const handleDateChange = (e) => {
        setCurrentPage(1);
        if (period === 'day') {
            setDate(new Date(e.target.value));
        } else if (period === 'month') {
            const [year, month] = e.target.value.split('-');
            setDate(new Date(year, month - 1, 1));
        } else if (period === 'year') {
            setDate(new Date(e.target.value, 0, 1));
        }
    };

    const renderTable = () => {
        switch (period) {
            case 'month':
                return (
                    <TransactionsDailyTotalsTable
                        transactionsDailyTotals={transactionsDailyTotals} />
                )
            case 'year':
                return (
                    <TransactionsMonthlyTotalsTable
                        transactionsMonthlyTotals={transactionMonthlyTotals} />
                )
            default:
                return (
                    <TransactionsTable
                        transactions={transactions} />
                )
        }
    };

    const PAGE_BUTTONS_DISPLAY_LIMIT = 5;
    const startPage = Math.max(currentPage - Math.floor(PAGE_BUTTONS_DISPLAY_LIMIT / 2), 1);
    const endPage = Math.min(startPage + PAGE_BUTTONS_DISPLAY_LIMIT - 1, totalPages);

    const pageNumbers = Array.from({ length: (endPage - startPage) + 1 }, (_, index) => startPage + index);

    return (
        <div className={`container ${styles['table-container']}`}>
            <div className={styles['header-container']}>
                <div className={styles['spacer']}></div>
                <h1 className={`text-center ${styles['title']}`}>{t('gmv.header')}</h1>
                <div className={styles['total-gmv-container']}>
                    <h4 className={styles['total-gmv-h']}>{t('gmv.dailyTotalsTable.total')} {totalGmv.toFixed(2)}</h4>
                    <h4 className={styles['regular-gmv-h']}>{t('gmv.dailyTotalsTable.totalRegular')} {totalRegularGmv.toFixed(2)}</h4>
                    <h4 className={styles['delayed-gmv-h']}>{t('gmv.dailyTotalsTable.totalDelayed')} {totalDelayedGmv.toFixed(2)}</h4>
                </div>
            </div>
            <div className="d-flex justify-content-center flex-wrap">
                <div className={styles['input-group']}>
                    <label htmlFor="period-select">{t('gmv.periodLabel')}</label>
                    <select value={period}
                        id="period-select"
                        className={`form-control ${styles['input-field']}`}
                        onChange={(e) => {
                            setCurrentPage(1)
                            setPeriod(e.target.value)
                        }}
                    >
                        <option value="day">{t('gmv.periodInput.day')}</option>
                        <option value="month">{t('gmv.periodInput.month')}</option>
                        <option value="year">{t('gmv.periodInput.year')}</option>
                    </select>
                </div>
                {period === 'day' && (
                    <div className={styles['input-group']}>
                        <label htmlFor="date-select">{t('gmv.dateLabel')}</label>
                        <input
                            id="date-select"
                            type="date"
                            className={`form-control ${styles['input-field']}`}
                            value={date.toISOString().split('T')[0]}
                            onChange={handleDateChange} />
                    </div>
                )}
                {period === 'month' && (
                    <div className={styles['input-group']}>
                        <label htmlFor="month-select">{t('gmv.monthLabel')}</label>
                        <input
                            id="month-select"
                            type="month"
                            className={`form-control ${styles['input-field']}`}
                            value={`${date.getFullYear()}-${(date.getMonth() + 1).toString().padStart(2, '0')}`}
                            onChange={handleDateChange} />
                    </div>
                )}
                {period === 'year' && (
                    <div className={styles['input-group']}>
                        <label htmlFor="year-select">{t('gmv.yearLabel')}</label>
                        <input
                            id="year-select"
                            type="number"
                            className={`form-control ${styles['input-field']}`}
                            value={date.getFullYear()} min="2000" max="2099"
                            onChange={handleDateChange} />
                    </div>
                )}
                {period !== 'year' && (
                    <div className={styles['input-group']}>
                        <label htmlFor="order-select">{t('gmv.perPageLabel')}</label>
                        <select
                            id="order-select"
                            className={`form-control ${styles['input-field']}`}
                            onChange={(e) => {
                                setCurrentPage(1);
                                setItemsPerPage(e.target.value)
                            }} >
                            <option value="10">10</option>
                            <option value="15">15</option>
                            <option value="20">20</option>
                        </select>
                    </div>
                )}
            </div>
            <div className={`table-responsive ${styles['table-wrapper']}`}>
                {isLoading ? (
                    <div className={styles['loading-container']}>
                        <ReactLoading type="spin" color="#808080" />
                    </div>
                ) : (
                    renderTable()
                )}
            </div>
            <div className={styles['buttons-wrapper']}>
                {currentPage > 1 && (
                    <button
                        onClick={() => setCurrentPage(currentPage - 1)}
                        className={styles['page-control-button']}>
                        {t('gmv.prevButton')}
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
                        {t('gmv.nextButton')}
                    </button>
                )}
            </div>
        </div>
    );
};

export default GmvPage;