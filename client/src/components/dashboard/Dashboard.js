import styles from './Dashboard.module.css'
import DashboardProduct from '../product/DashboardProduct';
import { useState } from 'react';
import * as ProductService from '../../services/productService'
import { toast } from 'react-toastify';
import ReactLoading from 'react-loading';

function DashBoard() {
    const [products, setProducts] = useState([]);
    const [currentBarcode, setCurrentBarcode] = useState("");
    const [isLoading, setIsLoading] = useState(false);

    const getProductHandler = async (event) => {
        const barcode = event.target.value;
        setCurrentBarcode(barcode);

        if (barcode.length === 13) {
            setIsLoading(true);
            try {
                const product = await ProductService.GetByBarcode(barcode);
                setProducts(prevProducts => [...prevProducts, product]);
            } catch (error) {
                toast.error(error.response.data);
                console.error(error);
            } finally {
                setIsLoading(false);
                setCurrentBarcode("");
            }
        }
    };

    return (
        <div className={`container ${styles['dash-container']}`}>
            <h1 className={`text-center ${styles['dash-title']}`}>DASHBOARD</h1>
            <div className="d-flex justify-content-center">
                <input
                    type="text"
                    value={currentBarcode}
                    placeholder="Barcode"
                    className={`form-control ${styles['barcode-input']}`}
                    onChange={getProductHandler}
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
                        {isLoading ? (
                            <tr>
                                <td colSpan="3">
                                    <div className={styles['loading-container']}>
                                        <ReactLoading type="spin" color="#808080"/>
                                    </div>
                                </td>
                            </tr>
                        ) : (
                            products.map(product => (
                                <DashboardProduct key={product.id} product={product} />
                            ))
                        )}
                    </tbody>
                </table>
            </div>
            <div className={styles['button-wrapper']}>
                <p className={styles['total-p']}>Total: </p>
                <button type="button" className={`btn btn-success ${styles['button-custom']}`}>Finish Transaction</button>
                <button type="button" className={`btn btn-warning ${styles['button-custom']}`}>Delayed Payment</button>
            </div>
        </div>
    );
}

export default DashBoard;