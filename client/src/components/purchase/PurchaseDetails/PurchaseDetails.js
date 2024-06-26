import styles from './PurchaseDetails.module.css';
import ReactLoading from 'react-loading';
import { useState, useEffect, useCallback } from 'react';
import { toast } from 'react-toastify'
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../context/AuthContext';
import TablePurchasedProduct from '../../purchasedProduct/TablePurchasedProduct/TablePurchasedProduct';
import DeletePurchaseModal from './DeletePurchaseModal/DeletePurchaseModal';
import DeletePurchasedProductModal from '../../purchasedProduct/DeletePurchasedProductModal/DeletePurchasedProductModal';
import * as PurchasedProductService from '../../../services/purchasedProductService';
import { useTranslation } from 'react-i18next';

function PurchaseDetails({ clientId, purchase, refreshClients, closePurchaseDetails }) {
    const [purchasedProducts, setPurchasedProducts] = useState([]);
    const [isLoading, setIsLoading] = useState(false);
    const [deleteModalIsOpen, setDeleteModalIsOpen] = useState(false);
    const [selectedProductId, setSelectedProductId] = useState("");
    const [deleteProductModalIsOpen, setDeleteProductModalIsOpen] = useState(false);
    const navigate = useNavigate();
    const { logout } = useAuth();
    const { t } = useTranslation();

    const handleError = useCallback((error) => {
        if (error.response && error.response.status === 401) {
            logout();
            navigate('/login');
            toast.warning(t('common.sessionExp'));
        } else {
            toast.error(error.response ? error.response.data : t('common.error'));
        }
        console.error(error);
    }, [logout, navigate, t]);

    const getPurchasedProducts = useCallback(async () => {
        setIsLoading(true);
        try {
            const response = await PurchasedProductService.GetAll(clientId, purchase.id);
            setPurchasedProducts(response);
        } catch (error) {
            handleError(error);
        } finally {
            setIsLoading(false);
        }
    }, [handleError, purchase, clientId]);

    useEffect(() => {
        getPurchasedProducts();
    }, [getPurchasedProducts]);

    const deletePurchasedProductHandler = (productId) => {
        setSelectedProductId(productId);
        setDeleteProductModalIsOpen(true);
    };

    return (
        <div>
            <div className={styles["container"]}>
                <div className={styles['header-container']}>
                    <p className={styles['text']}>Date: {purchase.date}</p>
                    <p className={styles['text']}>Total amount: {purchase.amount.toFixed(2)}</p>
                    <button
                        className={styles['delete-button']}
                        onClick={() => setDeleteModalIsOpen(true)}>
                        Delete
                    </button>
                </div>
                <h2 className={styles['table-header']}>Purchased products</h2>
                <div className={`table-responsive ${styles['table-wrapper']}`}>
                    <table className={styles['table-fill']}>
                        <thead>
                            <tr>
                                <th className={styles['text-left']}>Name</th>
                                <th className={styles['text-left']}>Description</th>
                                <th className={styles['text-left']}>Bought Quantity</th>
                                <th className={styles['text-left']}>Purchase Price</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody className={styles['table-hover']}>
                            {isLoading ? (
                                <tr>
                                    <td colSpan="4">
                                        <div className={styles['loading-container']}>
                                            <ReactLoading type="spin" color="#808080" />
                                        </div>
                                    </td>
                                </tr>
                            ) : (
                                purchasedProducts.map(product => (
                                    <TablePurchasedProduct
                                        key={product.id}
                                        product={product}
                                        deleteProductHandler={deletePurchasedProductHandler}
                                    />
                                ))
                            )}
                        </tbody>
                    </table>
                </div>
                <button
                    className={styles['close-button']}
                    onClick={() => closePurchaseDetails()}>
                    Close
                </button>
            </div>
            <div className={styles['backdrop']} />
            {deleteModalIsOpen && <DeletePurchaseModal
                clientId={clientId}
                purchaseId={purchase.id}
                closeModal={() => setDeleteModalIsOpen(false)}
                refreshClients={() => refreshClients()}
                closePurchaseDetails={() => closePurchaseDetails()} />}
            {deleteProductModalIsOpen && <DeletePurchasedProductModal
                clientId={clientId}
                purchaseId={purchase.id}
                purchasedProductId={selectedProductId}
                closeModal={() => setDeleteProductModalIsOpen(false)}
                refreshClients={() => refreshClients()} />}
        </div>
    );
}

export default PurchaseDetails;