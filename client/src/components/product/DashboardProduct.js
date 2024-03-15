import styles from './DashboardProduct.module.css'

function DashboardProduct({ product, updateQty, removeProduct }) {
    const qtyChangeHandler = (event) => {
        const newQty = event.target.value;
        updateQty(product.id, newQty);
    };

    const removeProductHandler = () => {
        removeProduct(product.id);
    };

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
                    <button onClick={removeProductHandler} className={styles.removeButton}>X</button>
                </div>
            </td>
        </tr>
    );
}

export default DashboardProduct;