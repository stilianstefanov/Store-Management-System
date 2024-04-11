import styles from './TablePurchasedProduct.module.css'

function TablePurchasedProduct({ product, deleteProduct }) {
    return (
        <tr className={styles['row']}>
            <td className={styles['text-left']}>{product.productDetails.name}</td>
            <td className={styles['text-left']}>{product.productDetails.description ? product.productDetails.description : 'N/A'}</td>
            <td className={styles['text-left']}>{product.boughtQuantity}</td>
            <td className={styles['text-left']}>{product.purchasePrice.toFixed(2)}</td>
            <td><button onClick={deleteProduct} className={styles.removeButton}>Remove</button></td>
        </tr>
    );
};

export default TablePurchasedProduct;