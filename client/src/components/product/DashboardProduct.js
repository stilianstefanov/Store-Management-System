import styles from './DashboardProduct.module.css'

function DashboardProduct({ product, updateQty }) {
    const qtyChangeHandler = (event) => {
        const newQty = event.target.value;
        updateQty(product.id, newQty);
    }

    return (
        <tr>
            <td>{product.name}</td>
            <td>{product.price.toFixed(2)}</td>
            <td className={styles.inputContainer}>
                <input
                    value={product.quantity}
                    onChange={qtyChangeHandler}
                    type="number"
                    className={`form-control ${styles.inputField}`}
                />
            </td>
        </tr>
    );
}

export default DashboardProduct;