import styles from './Gmv.module.css';
import { useState, useEffect, useCallback } from 'react';

function GmvPage() {
    const [period, setPeriod] = useState('day');
    const [date, setDate] = useState(new Date());
    const [transactions, setTransactions] = useState([]);

    const handleDateChange = (e) => {
        if (period === 'day') {
            setDate(new Date(e.target.value));
        } else if (period === 'month') {
            const [year, month] = e.target.value.split('-');
            setDate(new Date(year, month - 1));
        } else if (period === 'year') {
            setDate(new Date(e.target.value, 0));
        }
    };

    return (
        <div className={`container ${styles['table-container']}`}>
            <h1 className={`text-center ${styles['title']}`}>GMV</h1>
            <div className="d-flex justify-content-center flex-wrap">
                <div className={styles['input-group']}>
                    <label htmlFor="period-select">Period:</label>
                    <select value={period}
                        id="period-select"
                        className={`form-control ${styles['input-field']}`}
                        onChange={(e) => setPeriod(e.target.value)}>
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
                            value={`${date.getFullYear()}-${date.getMonth() + 1}`}
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
            </div>
        </div>
    );
};

export default GmvPage;