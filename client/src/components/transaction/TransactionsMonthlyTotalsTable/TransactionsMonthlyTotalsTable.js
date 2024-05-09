import styles from './TransactionsMonthlyTotalsTable.module.css'

function TransactionsDailyTotalsTable({ transactionsMonthlyTotals }) {
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
                        <td className={styles['text-left']}>{trMonthlyTotal.month}</td>
                        <td className={styles['text-left']}>{trMonthlyTotal.totalGmv.toFixed(2)}</td>
                        <td className={styles['text-left']}>{trMonthlyTotal.totalRegularGmv.toFixed(2)}</td>
                        <td className={styles['text-left']}>{trMonthlyTotal.totalDelayedGmv.toFixed(2)}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
};

export default TransactionsDailyTotalsTable;