import styles from './TableProduct.module.css'

function TableProduct({ product, openProductDetails }) {
    return (
        <tr onClick={() => openProductDetails(product.id)}>
            <td className={styles['text-left']}>{product.name}</td>
            <td className={styles['text-left']}>{product.description ? product.description : 'N/A'}</td>
            <td className={styles['text-left']}>{product.price.toFixed(2)}</td>
            <td className={styles['text-left']}>{product.quantity}</td>
        </tr>
    );
};

export default TableProduct;