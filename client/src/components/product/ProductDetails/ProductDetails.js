import styles from './ProductDetails.module.css';
import { useState, useEffect, useCallback } from 'react';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../context/AuthContext';

function ProductDetails({ productId, closeProductDetails, refreshProducts }) {
    const [product, setProduct] = useState({});

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

                </div>
            </div>
            <div className={styles['backdrop']} />
        </div>
    );
};

export default ProductDetails;