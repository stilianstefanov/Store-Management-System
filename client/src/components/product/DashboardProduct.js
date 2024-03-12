import styles from './DashboardProduct.module.css'

function DashboardProduct({ product }) {
    return (
        <tr>
            <td>{product.name}</td>
            <td>{product.price}</td>
            <td>{product.quantity}</td>
        </tr>
    );
}

export default DashboardProduct;