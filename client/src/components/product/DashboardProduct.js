import styles from './DashboardProduct.module.css'

function DashboardProduct(props) {
    return (
        <li key={props.product.id}>
            <p>{props.product.name}</p>
            <p>{props.product.price}</p>
            <p>{props.quantity}</p>
        </li>
    );
}

export default DashboardProduct;