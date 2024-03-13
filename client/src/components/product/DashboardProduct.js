import styles from './DashboardProduct.module.css'

function DashboardProduct({ product, updateQty }) {
    const qtyChangeHandler = (event) => {
        const newQty = event.target.value;
        updateQty(product.id, newQty);
    }

    return (
        <tr>
            <td>{product.name}</td>
            <td>{product.price}</td>
            <td> <input
                value={product.quantity}
                onChange={qtyChangeHandler}
                type="number"
                className='form-control'
            /></td>
        </tr>
    );
}

export default DashboardProduct;