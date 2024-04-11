import styles from './TablePurchasedProduct.module.css'

function TablePurchasedProduct({ product, deleteProduct }) {
    return (
        <tr>
            <td className={styles['text-left']}>{product.name}</td>
            <td className={styles['text-left']}>{product.description ? product.description : 'N/A'}</td>
            <td className={styles['text-left']}>{product.boughtQuantity}</td>
            <td className={styles['text-left']}>{product.purchasePrice.toFixed(2)}</td>
        </tr>
    );
};

export default TablePurchasedProduct;