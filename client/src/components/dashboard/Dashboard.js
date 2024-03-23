import styles from './Dashboard.module.css'
import DashboardProduct from '../product/DashboardProduct';
import DelayedPaymentModal from '../dashboard/delayedPaymentModal/DelayedPaymentModal'
import { useState } from 'react';
import * as ProductService from '../../services/productService'
import { toast } from 'react-toastify';
import ReactLoading from 'react-loading';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext'

function DashBoard() {
    const [products, setProducts] = useState([]);
    const [currentBarcode, setCurrentBarcode] = useState("");
    const [isLoading, setIsLoading] = useState(false);
    const [dPaymentModalIsOpen, setDPaymentModalIsOpen] = useState(false);
    const { logout } = useAuth();
    const navigate = useNavigate();

    const getProductHandler = async (event) => {
        const barcode = event.target.value;
        setCurrentBarcode(barcode);

        if (barcode.length === 13) {
            setIsLoading(true);
            try {
                let product = await ProductService.GetByBarcode(barcode);
                product = { ...product, quantity: 1 };
                setProducts(prevProducts => {
                    const existingProductIndex = prevProducts.findIndex(p => p.id === product.id);

                    if (existingProductIndex !== -1) {
                        const updatedProducts = [...prevProducts];
                        updatedProducts[existingProductIndex].quantity += 1;
                        return updatedProducts;

                    } else {
                        return [...prevProducts, product];
                    }
                });
            } catch (error) {
                handleError(error);
            } finally {
                setIsLoading(false);
                setCurrentBarcode("");
            }
        }
    };

    const finishTransactionHandler = async () => {
        if (products.length > 0) {
            setIsLoading(true);
            try {
                await ProductService.UpdateStocks(products);
                setProducts([]);
                toast.success("Successful operation!")
            } catch (error) {
                handleError(error);
            } finally {
                setIsLoading(false);
            }
        }
    }

    const openDPaymentModalHandler = () => {
        if (products.length > 0) {
            setDPaymentModalIsOpen(true);
        }
    };

    const updateProductQty = (productId, newQty) => {
        setProducts(currentProducts =>
            currentProducts.map(product =>
                product.id === productId ? { ...product, quantity: newQty } : product
            )
        );
    }

    const removeProduct = (productId) => {
        setProducts(products => products.filter(product => product.id !== productId));
    };

    const calculateTotalCost = (productsArr) => {
        return productsArr.reduce((total, product) => total + product.price * product.quantity, 0);
    }

    const handleError = (error) => {
        if (error.response && error.response.status === 401) {
            logout();
            navigate('/login');
            toast.warning('Your session has expired. Please login again.');
        } else {
            toast.error(error.response.data);
        }
        console.error(error);
    }

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
                                        <ReactLoading type="spin" color="#808080" />
                                    </div>
                                </td>
                            </tr>
                        ) : (
                            products.map(product => (
                                <DashboardProduct
                                    key={product.id}
                                    product={product}
                                    updateQty={updateProductQty}
                                    removeProduct={removeProduct}
                                />
                            ))
                        )}
                    </tbody>
                </table>
            </div>
            <div className={styles['button-wrapper']}>
                <p className={styles['total-p']}>Total: {calculateTotalCost(products).toFixed(2)} </p>
                <button
                    onClick={finishTransactionHandler}
                    type="button"
                    className={`btn btn-success ${styles['button-custom']}`}>
                    Finish Transaction
                </button>
                <button
                    onClick={openDPaymentModalHandler}
                    type="button"
                    className={`btn btn-warning ${styles['button-custom']}`}>
                    Delayed Payment
                </button>
            </div>
            {dPaymentModalIsOpen && (<DelayedPaymentModal
                onCancel={() => setDPaymentModalIsOpen(false)} 
                products = {products}/>)}
        </div>
    );
}

export default DashBoard;