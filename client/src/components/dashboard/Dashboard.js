import styles from './Dashboard.module.css'
import DashboardProduct from '../product/DashboardProduct';

function DashBoard() {
    return (
        <div className={styles['Dash-container']}>
            <h1>Dashboard</h1>
            <input 
                placeholder='Barcode'
                className={styles['Barcode-input']}
                //ToDO Implement onChange
            />
            <div className={styles['Products-header']}>
                <h3>Product</h3>
                <h3>Price</h3>
                <h3>Quantity</h3>
            </div>
            <div className={styles['Products-wrapper']}>
                <ul>
                    
                </ul>
            </div>
        </div>
    );
}

export default DashBoard;