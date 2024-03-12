import styles from './Dashboard.module.css'
import DashboardProduct from '../product/DashboardProduct';

function DashBoard() {
    const products = [
        { id: 1, name: 'Product 1', price: '10$', quantity: 100 },
        { id: 2, name: 'Product 2', price: '20$', quantity: 200 },
        { id: 2, name: 'Product 2', price: '20$', quantity: 200 },
        { id: 2, name: 'Product 2', price: '20$', quantity: 200 },
        { id: 2, name: 'Product 2', price: '20$', quantity: 200 },
        { id: 2, name: 'Product 2', price: '20$', quantity: 200 },
        { id: 2, name: 'Product 2', price: '20$', quantity: 200 },
        { id: 2, name: 'Product 2', price: '20$', quantity: 200 },
        { id: 2, name: 'Product 2', price: '20$', quantity: 200 },
        { id: 2, name: 'Product 2', price: '20$', quantity: 200 },
        { id: 2, name: 'Product 2', price: '20$', quantity: 200 },
    ];

    return (
        <div className={`container ${styles['dash-container']}`}>
            <h1 className={`text-center ${styles['dash-title']}`}>DASHBOARD</h1>
            <div className="d-flex justify-content-center">
                <input
                    type="text"
                    placeholder="Barcode"
                    className={`form-control ${styles['barcode-input']}`}
                // ToDo: Implement onChange
                />
            </div>
            <div className={`table-responsive ${styles['table-wrapper']}`}>
                <table className={`table table-striped ${styles.tableCustom}`}>
                    <thead className={styles.tableHeader}>
                        <tr>
                            <th>Product</th>
                            <th>Price</th>
                            <th>Quantity</th>
                        </tr>
                    </thead>
                    <tbody className={styles.tableRow}>
                        {products.map(product => (
                            <DashboardProduct key={product.id} product={product} />
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
}

export default DashBoard;