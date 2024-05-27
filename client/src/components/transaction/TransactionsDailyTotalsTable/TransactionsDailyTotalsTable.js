import styles from './TransactionsDailyTotalsTable.module.css';
import { useTranslation } from 'react-i18next';

function TransactionsDailyTotalsTable({ transactionsDailyTotals }) {
    const { t } = useTranslation();
    
    return (
        <table className={styles['table-fill']}>
            <thead>
                <tr>
                    <th className={styles['text-left']}>{t('gmv.dailyTotalsTable.date')}</th>
                    <th className={styles['text-left']}>{t('gmv.dailyTotalsTable.total')}</th>
                    <th className={styles['text-left']}>{t('gmv.dailyTotalsTable.totalRegular')}</th>
                    <th className={styles['text-left']}>{t('gmv.dailyTotalsTable.totalDelayed')}</th>
                </tr>
            </thead>
            <tbody className={styles['table-hover']}>
                {transactionsDailyTotals.map(trDailyTotal => (
                    <tr key={trDailyTotal.date}>
                        <td className={styles['text-left']}>{trDailyTotal.date}</td>
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