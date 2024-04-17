import styles from './WarehouseDetails.module.css';
import ReactLoading from 'react-loading';
import { useState, useEffect, useCallback } from 'react';
import { toast } from 'react-toastify'
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../context/AuthContext';
import ProductRow from './ProductRow/ProductRow';
import WarehouseForm from '../WarehouseForm/WarehouseForm';

function WarehouseDetails({ warehouse, closeWarehouseDetails, refreshWarehouses }) {
    const products = [{ externalId: '1', name: 'some name', quantity: 10, minQuantity: 5, maxQuantity: 20, suggestedOrderQty: 10 },
    { externalId: '1', name: 'some name', quantity: 10, minQuantity: 5, maxQuantity: 20, suggestedOrderQty: 10 },
    { externalId: '1', name: 'some name', quantity: 10, minQuantity: 5, maxQuantity: 20, suggestedOrderQty: 10 },
    { externalId: '1', name: 'some name', quantity: 10, minQuantity: 5, maxQuantity: 20, suggestedOrderQty: 10 },
    { externalId: '1', name: 'some name', quantity: 10, minQuantity: 5, maxQuantity: 20, suggestedOrderQty: 10 },
    { externalId: '1', name: 'some name', quantity: 10, minQuantity: 5, maxQuantity: 20, suggestedOrderQty: 10 },
    { externalId: '1', name: 'some name', quantity: 10, minQuantity: 5, maxQuantity: 20, suggestedOrderQty: 10 },
    { externalId: '1', name: 'some name', quantity: 10, minQuantity: 5, maxQuantity: 20, suggestedOrderQty: 10 },
    { externalId: '1', name: 'some name', quantity: 10, minQuantity: 5, maxQuantity: 20, suggestedOrderQty: 10 },
    { externalId: '1', name: 'some name', quantity: 10, minQuantity: 5, maxQuantity: 20, suggestedOrderQty: 10 },
    { externalId: '1', name: 'some name', quantity: 10, minQuantity: 5, maxQuantity: 20, suggestedOrderQty: 10 },
    { externalId: '1', name: 'some name', quantity: 10, minQuantity: 5, maxQuantity: 20, suggestedOrderQty: 10 }];
    const [isLoading, setIsLoading] = useState(false);
    const [warehouseFormIsOpen, setWarehouseFormIsOpen] = useState(false);
    const navigate = useNavigate();
    const { logout } = useAuth();


    return (
        <div>
            <div className={styles['container']}>
                <div className={styles['header-container']}>
                    <p className={styles['text']}>Name: {warehouse.name}</p>
                    <p className={styles['text']}>Type: {warehouse.type}</p>
                    <p className={styles['text']}>Count of products: {warehouse.productsCount}</p>
                    <button
                        className={styles['update-button']}
                        onClick={() => setWarehouseFormIsOpen(true)}>
                        Update
                    </button>
                </div>
                <h2 className={styles['table-header']}>Products</h2>
                <div className={`table-responsive ${styles['table-wrapper']}`}>
                    <table className={styles['table-fill']}>
                        <thead>
                            <tr>
                                <th className={styles['text-left']}>Name</th>
                                <th className={styles['text-left']}>Quantity</th>
                                <th className={styles['text-left']}>Min Quantity</th>
                                <th className={styles['text-left']}>Max Quantity</th>
                                <th className={styles['text-left']}>Recommended Order Quantity</th>
                            </tr>
                        </thead>
                        <tbody className={styles['table-hover']}>
                            {isLoading ? (
                                <tr>
                                    <td colSpan="5">
                                        <div className={styles['loading-container']}>
                                            <ReactLoading type="spin" color="#808080" />
                                        </div>
                                    </td>
                                </tr>
                            ) : (
                                products.map(product => (
                                    <ProductRow
                                        key={product.externalId}
                                        product={product}
                                    />
                                ))
                            )}
                        </tbody>
                    </table>
                </div>
                <button
                    className={styles['close-button']}
                    onClick={() => closeWarehouseDetails()}>
                    Close
                </button>
            </div>
            <div className={styles['backdrop']} />
            {warehouseFormIsOpen && <WarehouseForm
                warehouse={warehouse}
                closeForm={() => setWarehouseFormIsOpen(false)}
                refreshWarehouses={refreshWarehouses} />}
        </div>
    );
};

export default WarehouseDetails;