import styles from './TransactionsTable.module.css';
import { useTranslation } from 'react-i18next';

function TransactionsTable({ transactions }) {
    const { t } = useTranslation();

    return (
        <table className={styles['table-fill']}>
            <thead>
                <tr>
                    <th className={styles['text-left']}>{t('gmv.transactionsTable.dateTime')}</th>
                    <th className={styles['text-left']}>{t('gmv.transactionsTable.amount')}</th>
                    <th className={styles['text-left']}>{t('gmv.transactionsTable.type')}</th>
                </tr>
            </thead>
            <tbody className={styles['table-hover']}>
                {transactions.map(transaction => (
                    <tr key={transaction.id}>
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