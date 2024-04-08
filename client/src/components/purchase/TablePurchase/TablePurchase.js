import styles from './TablePurchase.module.css'

function TablePurchase ({ purchase, openPurchaseDetails }) {
    return (
        <tr onClick={() => openPurchaseDetails(purchase.id)}>
            <td className={styles['text-left']}>{purchase.date}</td>
            <td className={styles['text-left']}>{purchase.amount}</td>
        </tr>
    );
}

export default TablePurchase;