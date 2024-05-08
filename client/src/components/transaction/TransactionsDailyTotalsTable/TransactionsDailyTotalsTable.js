import styles from './TransactionsDailyTotalsTable.module.css'

function TransactionsDailyTotalsTable({ transactionsDailyTotals }) {
    return (
        <table className={styles['table-fill']}>
            <thead>
                <tr>
                    <th className={styles['text-left']}>Date</th>
                    <th className={styles['text-left']}>Total GMV</th>
                    <th className={styles['text-left']}>Total Regular GMV</th>
                    <th className={styles['text-left']}>Total Delayed GMV</th>
                </tr>
            </thead>
            <tbody className={styles['table-hover']}>
                {transactionsDailyTotals.map(trDailyTotal => (
                    <tr>
                        <td className={styles['text-left']}>{trDailyTotal.date.toISOString().split('T')[0]}</td>
                        <td className={styles['text-left']}>{trDailyTotal.totalGmv.toFixed(2)}</td>
                        <td className={styles['text-left']}>{trDailyTotal.totalRegularGmv.toFixed(2)}</td>
                        <td className={styles['text-left']}>{trDailyTotal.totalDelayedGmv.toFixed(2)}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
};

export default TransactionsDailyTotalsTable;