import styles from './PurchaseDetails.module.css';
import ReactLoading from 'react-loading';
import { useState, useEffect, useCallback } from 'react';
import { toast } from 'react-toastify'
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../context/AuthContext';
import TablePurchasedProduct from '../../purchasedProduct/TablePurchasedProduct';

function PurchaseDetails({ purchase, refreshClients, closeModal }) {
    const [purchasedProducts, setPurchasedProducts] = useState([]);
    const [isLoading, setIsLoading] = useState(false);

    return (
        <div>
            <div className={styles["container"]}>
                <div className={styles['header-container']}>
                    <p>Date: {purchase.date}</p>
                    <p>Amount: {purchase.amount.toFixed(2)}</p>
                    <button
                        className={styles['delete-button']}>
                        Delete
                    </button>
                </div>
                <h2>Purchased products</h2>
                <div className={`table-responsive ${styles['table-wrapper']}`}>
                    <table className={styles['table-fill']}>
                        <thead>
                            <tr>
                                <th className={styles['text-left']}>Name</th>
                                <th className={styles['text-left']}>Description</th>
                                <th className={styles['text-left']}>Bought Quantity</th>
                                <th className={styles['text-left']}>Purchase Price</th>
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
                                    />
                                ))
                            )}
                        </tbody>
                    </table>
                </div>
            </div>
            <div className={styles['backdrop']} />
        </div>
    );
}

export default PurchaseDetails;