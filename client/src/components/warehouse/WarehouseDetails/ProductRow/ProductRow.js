import styles from './ProductRow.module.css';

function ProductRow({ product }) {
    return (
        <tr>
            <td className={styles['text-left']}>{product.name}</td>
            <td className={styles['text-left']}>{product.quantity}</td>
            <td className={styles['text-left']}>{product.minQuantity}</td>
            <td className={styles['text-left']}>{product.maxQuantity}</td>
            <td className={styles['text-left']}>{product.suggestedOrderQty}</td>
        </tr>
    );
}

export default ProductRow;