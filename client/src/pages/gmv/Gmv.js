import styles from './Gmv.module.css';
import { useState, useEffect, useCallback } from 'react';
import ReactLoading from 'react-loading';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import TransactionsTable from '../../components/transaction/TransactionsTable/TransactionsTable';
import TransactionsDailyTotalsTable from '../../components/transaction/TransactionsDailyTotalsTable/TransactionsDailyTotalsTable';
import TransactionsMonthlyTotalsTable from '../../components/transaction/TransactionsMonthlyTotalsTable/TransactionsMonthlyTotalsTable';

function GmvPage() {
    const [transactionsData, setTransactionsData] = useState([]);
    const [period, setPeriod] = useState('day');
    const [date, setDate] = useState(new Date());
    const [currentPage, setCurrentPage] = useState(1);
    const [itemsPerPage, setItemsPerPage] = useState(10);
    const [totalPages, setTotalPages] = useState(0);
    const [totalGmv, setTotalGmv] = useState(0);
    const [totalRegularGmv, setTotalRegularGmv] = useState(0);
    const [totalDelayedGmv, setTotalDelayedGmv] = useState(0);
    const [isLoading, setIsLoading] = useState(false);

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
                        transactionsDailyTotals={transactionsData} />
                )
            case 'year':
                return (
                    <TransactionsMonthlyTotalsTable
                        transactionsMonthlyTotals={transactionsData} />
                )
            default:
                return (
                    <TransactionsTable
                        transactions={transactionsData} />
                )
        }
    };

    return (
        <div className={`container ${styles['table-container']}`}>
            <div className={styles['header-container']}>
                <div className={styles['spacer']}></div>
                <h1 className={`text-center ${styles['title']}`}>GMV</h1>
                <div className={styles['total-gmv-container']}>
                    <h4 className={styles['total-gmv-h']}>Total GMV: {totalGmv.toFixed(2)}</h4>
                    <h4 className={styles['regular-gmv-h']}>Regular GMV: {totalRegularGmv.toFixed(2)}</h4>
                    <h4 className={styles['delayed-gmv-h']}>Delayed GMV: {totalDelayedGmv.toFixed(2)}</h4>
                </div>
            </div>
            <div className="d-flex justify-content-center flex-wrap">
                <div className={styles['input-group']}>
                    <label htmlFor="period-select">Period:</label>
                    <select value={period}
                        id="period-select"
                        className={`form-control ${styles['input-field']}`}
                        onChange={(e) => {
                            setCurrentPage(1)
                            setPeriod(e.target.value)
                        }}
                    >
                        <option value="day">Day</option>
                        <option value="month">Month</option>
                        <option value="year">Year</option>
                    </select>
                </div>
                {period === 'day' && (
                    <div className={styles['input-group']}>
                        <label htmlFor="date-select">Date:</label>
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
                        <label htmlFor="month-select">Month:</label>
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
                        <label htmlFor="year-select">Year:</label>
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
                        <label htmlFor="order-select">Items per Page :</label>
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
        </div>
    );
};

export default GmvPage;