import styles from './WarehouseDetails.module.css';
import ReactLoading from 'react-loading';
import { useState, useEffect, useCallback } from 'react';
import { toast } from 'react-toastify'
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../context/AuthContext';
import ProductRow from './ProductRow/ProductRow';
import WarehouseForm from '../WarehouseForm/WarehouseForm';
import * as WarehouseService from '../../../services/warehouseService';
import { useTranslation } from 'react-i18next';

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
    const { t } = useTranslation();

    const handleError = useCallback((error) => {
        if (error.response && error.response.status === 401) {
            logout();
            navigate('/login');
            toast.warning(t('common.sessionExp'));
        } else {
            toast.error(error.response ? error.response.data : "An error occurred");
        }
        console.error(error);
    }, [logout, navigate, t]);

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
                    <p className={styles['text']}>{t('warehouseDetails.name')} {warehouse.name}</p>
                    <p className={styles['text']}>{t('warehouseDetails.type')} {warehouse.type}</p>
                    <p className={styles['text']}>{t('warehouseDetails.count')} {warehouse.productsCount}</p>
                    <button
                        className={styles['update-button']}
                        onClick={() => setWarehouseFormIsOpen(true)}>
                        {t('warehouseDetails.update')}
                    </button>
                </div>
                <div className={styles['table-header-wrapper']}>
                    <h2 className={styles['table-header']}>{t('warehouseDetails.header')}</h2>
                    <div className={styles["checkbox-wrapper-14"]}>
                        <input id="s1-14" type="checkbox" className={styles["switch"]}
                            onChange={() => setShowLowStocksOnly(!showLowStocksOnly)} />
                        <label for="s1-14">{t('warehouseDetails.checkbox')}</label>
                    </div>
                    {showLowStocksOnly &&
                        <button className={styles['export-button']}
                        //ToDo Implement Excel export
                        >
                            {t('warehouseDetails.export')}
                        </button>}
                </div>
                <div className={styles['input-fields-container']}>
                    <div className={styles['input-group']}>
                        <label htmlFor="search-input">{t('warehouseDetails.searchLabel')}</label>
                        <input
                            id="search-input"
                            placeholder={t('warehouseDetails.searchInput')}
                            className={`form-control ${styles['input-field']}`}
                            onChange={(e) => {
                                setCurrentPage(1);
                                setSearchTerm(e.target.value);
                            }}
                        />
                    </div>
                    <div className={styles['input-group']}>
                        <label htmlFor="order-select">{t('warehouseDetails.sortLabel')}</label>
                        <select
                            id="order-select"
                            className={`form-control ${styles['input-field']}`}
                            onChange={(e) => {
                                setCurrentPage(1);
                                setSorting(e.target.value);
                            }}
                        >
                            <option value="0">{t('warehouseDetails.sortInput.nameAsc')}</option>
                            <option value="1">{t('warehouseDetails.sortInput.nameDesc')}</option>
                            <option value="2">{t('warehouseDetails.sortInput.quantityAsc')}</option>
                            <option value="3">{t('warehouseDetails.sortInput.quantityDesc')}</option>
                            <option value="4">{t('warehouseDetails.sortInput.minQtyAsc')}</option>
                            <option value="5">{t('warehouseDetails.sortInput.minQtyDesc')}</option>
                            <option value="6">{t('warehouseDetails.sortInput.maxQtyAsc')}</option>
                            <option value="7">{t('warehouseDetails.sortInput.maxQtyDesc')}</option>
                            <option value="8">{t('warehouseDetails.sortInput.recQtyAsc')}</option>
                            <option value="9">{t('warehouseDetails.sortInput.recQtyDesc')}</option>
                        </select>
                    </div>
                    <div className={styles['input-group']}>
                        <label htmlFor="order-select">{t('warehouseDetails.perPageLabel')}</label>
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
                                <th className={styles['text-left']}>{t('warehouseDetails.table.name')}</th>
                                <th className={styles['text-left']}>{t('warehouseDetails.table.quantity')}</th>
                                <th className={styles['text-left']}>{t('warehouseDetails.table.minQty')}</th>
                                <th className={styles['text-left']}>{t('warehouseDetails.table.maxQty')}</th>
                                <th className={styles['text-left']}>{t('warehouseDetails.table.recQty')}</th>
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
                        {t('warehouseDetails.close')}
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