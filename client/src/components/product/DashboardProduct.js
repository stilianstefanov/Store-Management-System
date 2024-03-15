import styles from './DashboardProduct.module.css'

function DashboardProduct({ product, updateQty }) {
    const qtyChangeHandler = (event) => {
        const newQty = event.target.value;
        updateQty(product.id, newQty);
    }

    return (
        <tr className={styles.row}>
            <td>{product.name}</td>
            <td>{product.price.toFixed(2)}</td>
            <td className={styles.inputContainer}>
                <div className={styles.quantityWrapper}>
                    <input
                        value={product.quantity}
                        onChange={qtyChangeHandler}
                        type="number"
                        className={`form-control ${styles.inputField}`}
                    />
                    <button className={styles.removeButton}>X</button>
                </div>
            </td>
        </tr>
    );
}

export default DashboardProduct;