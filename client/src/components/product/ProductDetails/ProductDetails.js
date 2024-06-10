import styles from './ProductDetails.module.css';
import { useState, useEffect, useCallback } from 'react';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../context/AuthContext';
import ProductForm from '../ProductForm/ProductForm';
import ChangeWarehouse from './ChangeWarehouseModal/ChangeWarehouse';
import AddQuantityModal from './AddQuantityModal/AddQuantityModal';
import DeleteProductModal from './DeleteProductModal/DeleteProductModal';
import * as ProductService from '../../../services/productService';
import { useTranslation } from 'react-i18next';

function ProductDetails({ productId, closeProductDetails, refreshProducts }) {
    const [product, setProduct] = useState({
        barcode: '',
        name: '',
        description: '',
        price: 0,
        deliveryPrice: 0,
        quantity: 0,
        minQuantity: 0,
        maxQuantity: 0,
        warehouse: {
            name: '',
            type: ''
        }
    });
    const [productFormIsOpen, setProductFormIsOpen] = useState(false);
    const [changeWarehouseModalIsOpen, setChangeWarehouseModalIsOpen] = useState(false);
    const [addQuantityModalIsOpen, setAddQuantityModalIsOpen] = useState(false);
    const [deleteModalIsOpen, setDeleteModalIsOpen] = useState(false);
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

    const getProductDetails = useCallback(async () => {
        try {
            const response = await ProductService.GetById(productId);
            setProduct(response);
        } catch (error) {
            handleError(error);
        }
    }, [handleError, productId]);

    useEffect(() => {
        getProductDetails();
    }, [getProductDetails]);

    const updateProduct = (product) => {
        setProduct(product);
    }

    return (
        <div>
            <div className={styles['modal']}>
                <div className={styles['product-info-wrapper']}>
                    <div className={styles['left-section']}>
                        <ul className={styles['list-info']}>
                            <li>
                                <p>{t('productDetails.barcode')} <span className={styles['list-info-span']}>{product.barcode}</span></p>
                            </li>
                            <li>
                                <p>{t('productDetails.name')} <span className={styles['list-info-span']}>{product.name}</span></p>
                            </li>
                            <li>
                                <p>{t('productDetails.description')} <span className={styles['list-info-span']}>{product.description ? product.description : 'N/A'}</span></p>
                            </li>
                            <li>
                                <p>{t('productDetails.price')} <span className={styles['list-info-span']}>{product.price.toFixed(2)}</span></p>
                            </li>
                            <li>
                                <p>{t('productDetails.delPrice')} <span className={styles['list-info-span']}>{product.deliveryPrice.toFixed(2)}</span></p>
                            </li>
                        </ul>
                    </div>
                    <div className={styles['right-section']}>
                        <ul className={styles['list-info']}>
                            <li>
                                <p>{t('productDetails.qty')} <span className={styles['list-info-span']}>{product.quantity}</span></p>
                            </li>
                            <li>
                                <p>{t('productDetails.minQty')} <span className={styles['list-info-span']}>{product.minQuantity}</span></p>
                            </li>
                            <li>
                                <p>{t('productDetails.maxQty')} <span className={styles['list-info-span']}>{product.maxQuantity}</span></p>
                            </li>
                            <li>
                                <p>{t('productDetails.warehouseName')} <span className={styles['list-info-span']}>{product.warehouse.name}</span></p>
                            </li>
                            <li>
                                <p>{t('productDetails.warehouseType')} <span className={styles['list-info-span']}>{product.warehouse.type}</span></p>
                            </li>
                        </ul>
                    </div>
                </div>
                <div className={styles['buttons-container']}>
                    <button
                        className={styles['update-button']}
                        onClick={() => setProductFormIsOpen(true)}
                    >
                        {t('productDetails.update')}
                    </button>
                    <button
                        className={styles['add-qty-button']}
                        onClick={() => setAddQuantityModalIsOpen(true)}
                    >
                        {t('productDetails.addQty')}
                    </button>
                    <button
                        className={styles['change-button']}
                        onClick={() => setChangeWarehouseModalIsOpen(true)}
                    >
                        {t('productDetails.changeWarehouse')}
                    </button>
                    <button
                        className={styles['delete-button']}
                        onClick={() => setDeleteModalIsOpen(true)}
                    >
                        {t('productDetails.delete')}
                    </button>
                </div>
                <button
                    className={styles['close-button']}
                    onClick={() => closeProductDetails()}>
                    {t('productDetails.close')}
                </button>
            </div>
            <div className={styles['backdrop']} />
            {productFormIsOpen && <ProductForm
                product={product}
                updateProduct={updateProduct}
                closeForm={() => setProductFormIsOpen(false)}
                refreshProducts={refreshProducts} />}
            {changeWarehouseModalIsOpen && <ChangeWarehouse
                productId={product.id}
                updateProduct={updateProduct}
                closeModal={() => setChangeWarehouseModalIsOpen(false)}
            />}
            {addQuantityModalIsOpen && <AddQuantityModal
                product={product}
                updateProduct={updateProduct}
                refreshProducts={refreshProducts}
                closeModal={() => setAddQuantityModalIsOpen(false)} />}
            {deleteModalIsOpen && <DeleteProductModal
                productId={product.id}
                closeModal={() => setDeleteModalIsOpen(false)}
                closeProductDetails={() => closeProductDetails()}
                refreshProducts={refreshProducts} />}
        </div>
    );
};

export default ProductDetails;