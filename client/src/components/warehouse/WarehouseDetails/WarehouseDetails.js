import styles from './WarehouseDetails.module.css';
import ReactLoading from 'react-loading';
import { useState, useEffect, useCallback } from 'react';
import { toast } from 'react-toastify'
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../context/AuthContext';
import ProductRow from './ProductRow/ProductRow';
import WarehouseForm from '../WarehouseForm/WarehouseForm';
import * as WarehouseService from '../../../services/warehouseService';

function WarehouseDetails({ warehouse, closeWarehouseDetails, refreshWarehouses }) {
    const [products, setProducts] = useState([]);
    const [isLoading, setIsLoading] = useState(false);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(0);
    const [productsPerPage, setProductsPerPage] = useState(10);
    const [searchTerm, setSearchTerm] = useState("");
    const [sorting, setSorting] = useState(0);
    const [showLowStocksOnly, setShowLowStocksOnly] = useState(false);
    const [warehouseFormIsOpen, setWarehouseFormIsOpen] = useState(false);
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

    const getProducts = useCallback(async () => {
        setIsLoading(true);
        try {
            const request = {
                currentPage,
                productsPerPage,
                searchTerm,
                sorting,
                belowMinQty: showLowStocksOnly
            };
            const response = await WarehouseService.GetProductsByWarehouse(warehouse.id, request);
            setProducts(response.products);
            setTotalPages(response.totalPages);
        } catch (error) {
            handleError(error);
        } finally {
            setIsLoading(false);
        }
    }, [handleError, warehouse, currentPage, productsPerPage, sorting, searchTerm, showLowStocksOnly]);

    useEffect(() => {
        getProducts();
    }, [getProducts]);

    const PAGE_BUTTONS_DISPLAY_LIMIT = 5;
    const startPage = Math.max(currentPage - Math.floor(PAGE_BUTTONS_DISPLAY_LIMIT / 2), 1);
    const endPage = Math.min(startPage + PAGE_BUTTONS_DISPLAY_LIMIT - 1, totalPages);

    const pageNumbers = Array.from({ length: (endPage - startPage) + 1 }, (_, index) => startPage + index);

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
                <div className={styles['table-header-wrapper']}>
                    <h2 className={styles['table-header']}>Products</h2>
                    <div className={styles["checkbox-wrapper-14"]}>
                        <input id="s1-14" type="checkbox" className={styles["switch"]}
                            onChange={() => setShowLowStocksOnly(!showLowStocksOnly)} />
                        <label for="s1-14">Show Low Stock Items</label>
                    </div>
                    {showLowStocksOnly &&
                        <button className={styles['export-button']}
                        //ToDo Implement Excel export
                        >
                            Export
                        </button>}
                </div>
                <div className={styles['input-fields-container']}>
                    <div className={styles['input-group']}>
                        <label htmlFor="search-input">Search:</label>
                        <input
                            id="search-input"
                            placeholder="Search"
                            className={`form-control ${styles['input-field']}`}
                            onChange={(e) => {
                                setCurrentPage(1);
                                setSearchTerm(e.target.value);
                            }}
                        />
                    </div>
                    <div className={styles['input-group']}>
                        <label htmlFor="order-select">Sort by:</label>
                        <select
                            id="order-select"
                            className={`form-control ${styles['input-field']}`}
                            onChange={(e) => {
                                setCurrentPage(1);
                                setSorting(e.target.value);
                            }}
                        >
                            <option value="0">Name (Ascending)</option>
                            <option value="1">Name (Descending)</option>
                            <option value="2">Quantity (Ascending)</option>
                            <option value="3">Quantity (Descending)</option>
                            <option value="4">Min Quantity (Ascending)</option>
                            <option value="5">Min Quantity (Descending)</option>
                            <option value="6">Max Quantity (Ascending)</option>
                            <option value="7">Max Quantity (Descending)</option>
                            <option value="8">Recommended Order Qty (Ascending)</option>
                            <option value="9">Recommended Order Qty (Descending)</option>
                        </select>
                    </div>
                    <div className={styles['input-group']}>
                        <label htmlFor="order-select">Products per Page:</label>
                        <select
                            id="order-select"
                            className={`form-control ${styles['input-field']}`}
                            onChange={(e) => {
                                setCurrentPage(1);
                                setProductsPerPage(e.target.value)
                            }} >
                            <option value="10">10</option>
                            <option value="15">15</option>
                            <option value="20">20</option>
                        </select>
                    </div>
                </div>
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
                <div className={styles['footer-container']}>
                    <button
                        className={styles['close-button']}
                        onClick={() => closeWarehouseDetails()}>
                        Close
                    </button>
                    <div className={styles['buttons-wrapper']}>
                        {currentPage > 1 && (
                            <button
                                onClick={() => setCurrentPage(currentPage - 1)}
                                className={styles['page-control-button']}>
                                Prev
                            </button>
                        )}
                        {pageNumbers.map(pageNumber => (
                            <button
                                className={`${styles['page-button']} ${currentPage === pageNumber ? styles['current-page'] : ''}`}
                                key={pageNumber}
                                onClick={() => setCurrentPage(pageNumber)}
                            >
                                {pageNumber}
                            </button>
                        ))}
                        {currentPage < totalPages && (
                            <button
                                onClick={() => setCurrentPage(currentPage + 1)}
                                className={styles['page-control-button']}>
                                Next
                            </button>
                        )}
                    </div>
                </div>
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