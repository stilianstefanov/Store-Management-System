import styles from './Dashboard.module.css'
import DashboardProduct from '../product/DashboardProduct/DashboardProduct';
import DelayedPaymentModal from '../dashboard/delayedPaymentModal/DelayedPaymentModal'
import { useState } from 'react';
import * as ProductService from '../../services/productService'
import { toast } from 'react-toastify';
import ReactLoading from 'react-loading';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import { useTranslation } from 'react-i18next';

function DashBoard() {
    const [products, setProducts] = useState([]);
    const [currentBarcode, setCurrentBarcode] = useState("");
    const [isLoading, setIsLoading] = useState(false);
    const [dPaymentModalIsOpen, setDPaymentModalIsOpen] = useState(false);
    const { logout } = useAuth();
    const navigate = useNavigate();
    const { t } = useTranslation();

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
                product.id === productId ? { ...product, quantity: Number(newQty) } : product
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
            <h1 className={`text-center ${styles['dash-title']}`}>{t('dashboard.dashboard')}</h1>
            <div className="d-flex justify-content-center">
                <input
                    type="text"
                    value={currentBarcode}
                    placeholder={t('dashboard.barcode')}
                    className={`form-control ${styles['barcode-input']}`}
                    onChange={getProductHandler}
                />
            </div>
            <div className={`table-responsive ${styles['table-wrapper']}`}>
                <table className={`table table-striped ${styles.tableCustom}`}>
                    <thead className={styles.tableHeader}>
                        <tr>
                            <th>{t('dashboard.product')}</th>
                            <th>{t('dashboard.price')}</th>
                            <th>{t('dashboard.quantity')}</th>
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
                <p className={styles['total-p']}>{t('dashboard.total')}: {calculateTotalCost(products).toFixed(2)} </p>
                <button
                    onClick={finishTransactionHandler}
                    type="button"
                    className={`btn btn-success ${styles['button-custom']}`}>
                    {t('dashboard.finish')}
                </button>
                <button
                    onClick={openDPaymentModalHandler}
                    type="button"
                    className={`btn btn-warning ${styles['button-custom']}`}>
                    {t('dashboard.delayed')}
                </button>
            </div>
            {dPaymentModalIsOpen && (<DelayedPaymentModal
                closeDPaymentModal={() => setDPaymentModalIsOpen(false)}
                products={products}
                clearProducts={() => setProducts([])}
            />)}
        </div>
    );
}

export default DashBoard;