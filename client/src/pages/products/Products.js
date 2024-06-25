import styles from './Products.module.css';
import { useState, useEffect, useCallback } from 'react';
import ReactLoading from 'react-loading';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext'
import TableProduct from '../../components/product/TableProduct/TableProduct';
import ProductForm from '../../components/product/ProductForm/ProductForm';
import ProductDetails from '../../components/product/ProductDetails/ProductDetails';
import * as ProductService from '../../services/productService';
import { useTranslation } from 'react-i18next';

function ProductsPage() {
    const [products, setProducts] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(0);
    const [productsPerPage, setProductsPerPage] = useState(10);
    const [searchTerm, setSearchTerm] = useState("");
    const [sorting, setSorting] = useState(0);
    const [isLoading, setIsLoading] = useState(false);
    const [productFormIsOpen, setProductFormIsopen] = useState(false);
    const [productDetailsIsOpen, setProductDetailsIsOpen] = useState(false);
    const [selectedProductId, setSelectedProductId] = useState("");
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
    }, [logout, navigate]);

    const getProducts = useCallback(async () => {
        setIsLoading(true);
        try {
            const request = {
                currentPage,
                productsPerPage,
                searchTerm,
                sorting
            };
            const response = await ProductService.GetAll(request);
            setProducts(response.products);
            setTotalPages(response.totalPages);
        } catch (error) {
            handleError(error);
        } finally {
            setIsLoading(false);
        }
    }, [handleError, currentPage, productsPerPage, sorting, searchTerm]);

    useEffect(() => {
        getProducts();
    }, [getProducts]);

    const openProductDetailsHandler = (productId) => {
        setSelectedProductId(productId);
        setProductDetailsIsOpen(true);
    };


    const PAGE_BUTTONS_DISPLAY_LIMIT = 5;
    const startPage = Math.max(currentPage - Math.floor(PAGE_BUTTONS_DISPLAY_LIMIT / 2), 1);
    const endPage = Math.min(startPage + PAGE_BUTTONS_DISPLAY_LIMIT - 1, totalPages);

    const pageNumbers = Array.from({ length: (endPage - startPage) + 1 }, (_, index) => startPage + index);

    return (
        <div className={`container ${styles['table-container']}`}>
            <div className={styles['header-container']}>
                <div className={styles['spacer']}></div>
                <h1 className={`text-center ${styles['title']}`}>{t('products.header')}</h1>
                <button
                    className={styles['add-product-button']}
                    onClick={() => setProductFormIsopen(true)} >
                    {t('products.add')}
                </button>
            </div>
            <div className="d-flex justify-content-center flex-wrap">
                <div className={styles['input-group']}>
                    <label htmlFor="search-input">{t('products.searchLabel')}</label>
                    <input
                        id="search-input"
                        type="text"
                        value={searchTerm}
                        placeholder={t('products.searchInput')}
                        className={`form-control ${styles['input-field']}`}
                        onChange={(e) => {
                            setCurrentPage(1);
                            setSearchTerm(e.target.value);
                        }} />
                </div>
                <div className={styles['input-group']}>
                    <label htmlFor="order-select">{t('products.sortLabel')}</label>
                    <select
                        id="order-select"
                        className={`form-control ${styles['input-field']}`}
                        onChange={(e) => {
                            setCurrentPage(1);
                            setSorting(e.target.value);
                        }} >
                        <option value="0">{t('products.sortInput.nameAsc')}</option>
                        <option value="1">{t('products.sortInput.nameDesc')}</option>
                        <option value="2">{t('products.sortInput.priceAsc')}</option>
                        <option value="3">{t('products.sortInput.priceDesc')}</option>
                        <option value="4">{t('products.sortInput.quantityAsc')}</option>
                        <option value="5">{t('products.sortInput.quantityDesc')}</option>
                        <option value="6">{t('products.sortInput.dPriceAsc')}</option>
                        <option value="7">{t('products.sortInput.dPriceDesc')}</option>
                    </select>
                </div>
                <div className={styles['input-group']}>
                    <label htmlFor="order-select">{t('products.perPageLabel')}</label>
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
                            <th className={styles['text-left']}>{t('products.table.name')}</th>
                            <th className={styles['text-left']}>{t('products.table.description')}</th>
                            <th className={styles['text-left']}>{t('products.table.price')}</th>
                            <th className={styles['text-left']}>{t('products.table.quantity')}</th>
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
                            products.map(product => (
                                <TableProduct
                                    key={product.id}
                                    product={product}
                                    openProductDetails={openProductDetailsHandler}
                                />
                            ))
                        )}
                    </tbody>
                </table>
            </div>
            <div className={styles['buttons-wrapper']}>
                {currentPage > 1 && (
                    <button
                        onClick={() => setCurrentPage(currentPage - 1)}
                        className={styles['page-control-button']}>
                        {t('products.prevButton')}
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
                        {t('products.nextButton')}
                    </button>
                )}
            </div>
            {productFormIsOpen && <ProductForm
                closeForm={() => setProductFormIsopen(false)}
                refreshProducts={() => getProducts()} />}
            {productDetailsIsOpen && <ProductDetails
                productId={selectedProductId}
                closeProductDetails={() => setProductDetailsIsOpen(false)}
                refreshProducts={() => getProducts()} />}
        </div>
    );
}

export default ProductsPage;