import styles from './TransactionsMonthlyTotalsTable.module.css';

const monthNames = {
    1: "January", 2: "February", 3: "March",
    4: "April", 5: "May", 6: "June",
    7: "July", 8: "August", 9: "September",
    10: "October", 11: "November", 12: "December"
};

function TransactionsMonthlyTotalsTable({ transactionsMonthlyTotals }) {
    return (
        <table className={styles['table-fill']}>
            <thead>
                <tr>
                    <th className={styles['text-left']}>Month</th>
                    <th className={styles['text-left']}>Total GMV</th>
                    <th className={styles['text-left']}>Total Regular GMV</th>
                    <th className={styles['text-left']}>Total Delayed GMV</th>
                </tr>
            </thead>
            <tbody className={styles['table-hover']}>
                {transactionsMonthlyTotals.map(trMonthlyTotal => (
                    <tr>
                        <td className={styles['text-left']}>{monthNames[trMonthlyTotal.month]}</td>
                        <td className={styles['text-left']}>{trMonthlyTotal.totalGmv.toFixed(2)}</td>
                        <td className={styles['text-left']}>{trMonthlyTotal.totalRegularGmv.toFixed(2)}</td>
                        <td className={styles['text-left']}>{trMonthlyTotal.totalDelayedGmv.toFixed(2)}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
};

export default TransactionsMonthlyTotalsTable;