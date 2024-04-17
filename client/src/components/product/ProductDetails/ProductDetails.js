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

    const handleError = useCallback((error) => {
        if (error.response && error.response.status === 401) {
            logout();
            navigate('/login');
            toast.warning('Your session has expired. Please login again.');
        } else {
            toast.error(error.response ? error.response.data : "An error occurred");
        }
        console.error(error);
    }, [logout, navigate]);

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
                                <p>Barcode: <span className={styles['list-info-span']}>{product.barcode}</span></p>
                            </li>
                            <li>
                                <p>Name: <span className={styles['list-info-span']}>{product.name}</span></p>
                            </li>
                            <li>
                                <p>Description: <span className={styles['list-info-span']}>{product.description ? product.description : 'N/A'}</span></p>
                            </li>
                            <li>
                                <p>Price: <span className={styles['list-info-span']}>{product.price.toFixed(2)}</span></p>
                            </li>
                            <li>
                                <p>Delivery price: <span className={styles['list-info-span']}>{product.deliveryPrice.toFixed(2)}</span></p>
                            </li>
                        </ul>
                    </div>
                    <div className={styles['right-section']}>
                        <ul className={styles['list-info']}>
                            <li>
                                <p>Quantity: <span className={styles['list-info-span']}>{product.quantity}</span></p>
                            </li>
                            <li>
                                <p>Min Quantity: <span className={styles['list-info-span']}>{product.minQuantity}</span></p>
                            </li>
                            <li>
                                <p>Max Quantity: <span className={styles['list-info-span']}>{product.maxQuantity}</span></p>
                            </li>
                            <li>
                                <p>Warehouse Name: <span className={styles['list-info-span']}>{product.warehouse.name}</span></p>
                            </li>
                            <li>
                                <p>Warehouse Type: <span className={styles['list-info-span']}>{product.warehouse.type}</span></p>
                            </li>
                        </ul>
                    </div>
                </div>
                <div className={styles['buttons-container']}>
                    <button
                        className={styles['update-button']}
                        onClick={() => setProductFormIsOpen(true)}
                    >
                        Update
                    </button>
                    <button
                        className={styles['add-qty-button']}
                        onClick={() => setAddQuantityModalIsOpen(true)}
                    >
                        Add quantity
                    </button>
                    <button
                        className={styles['change-button']}
                        onClick={() => setChangeWarehouseModalIsOpen(true)}
                    >
                        Change warehouse
                    </button>
                    <button
                        className={styles['delete-button']}
                        onClick={() => setDeleteModalIsOpen(true)}
                    >
                        Delete
                    </button>
                </div>
                <button
                    className={styles['close-button']}
                    onClick={() => closeProductDetails()}>
                    Close
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