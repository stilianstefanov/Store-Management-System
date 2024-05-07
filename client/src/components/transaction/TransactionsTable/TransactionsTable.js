import styles from './TransactionsTable.module.css'

function TransactionsTable({ transactions }) {
    return (
        <table className={styles['table-fill']}>
            <thead>
                <tr>
                    <th className={styles['text-left']}>Date/Time</th>
                    <th className={styles['text-left']}>Amount</th>
                    <th className={styles['text-left']}>Type</th>
                </tr>
            </thead>
            <tbody className={styles['table-hover']}>
                {transactions.map(transaction => (
                    <tr>
                        <td className={styles['text-left']}>{transaction.dateTime}</td>
                        <td className={styles['text-left']}>{transaction.amount.toFixed(2)}</td>
                        <td className={styles['text-left']}>{transaction.type}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
};

export default TransactionsTable;