import styles from './Products.module.css';
import { useState, useEffect, useCallback } from 'react';
import ReactLoading from 'react-loading';
import TableProduct from '../../components/product/TableProduct/TableProduct'

function ProductsPage() {
    const [isLoading, setIsLoading] = useState(false);
    const products =
        [{ id: 1, name: 'cola', description: 'test', price: 1.20, quantity: 1 },
        { id: 1, name: 'cola', description: 'test', price: 1.20, quantity: 1 },
        { id: 1, name: 'cola', description: 'test', price: 1.20, quantity: 1 },
        { id: 1, name: 'cola', description: 'test', price: 1.20, quantity: 1 },
        { id: 1, name: 'cola', description: 'test', price: 1.20, quantity: 1 },
        { id: 1, name: 'cola', description: 'test', price: 1.20, quantity: 1 },
        { id: 1, name: 'cola', description: 'test', price: 1.20, quantity: 1 },
        { id: 1, name: 'cola', description: 'test', price: 1.20, quantity: 1 }]
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(0);
    const [productsPerPage, setProductsPerPage] = useState(10);
    const [searchTerm, setSearchTerm] = useState("");
    const [sorting, setSorting] = useState(0);


    const PAGE_BUTTONS_DISPLAY_LIMIT = 5;
    const startPage = Math.max(currentPage - Math.floor(PAGE_BUTTONS_DISPLAY_LIMIT / 2), 1);
    const endPage = Math.min(startPage + PAGE_BUTTONS_DISPLAY_LIMIT - 1, totalPages);

    const pageNumbers = Array.from({ length: (endPage - startPage) + 1 }, (_, index) => startPage + index);

    return (
        <div className={`container ${styles['table-container']}`}>
            <div className={styles['header-container']}>
                <div className={styles['spacer']}></div>
                <h1 className={`text-center ${styles['title']}`}>Products</h1>
                <button
                    className={styles['add-product-button']}
                >
                    Add New Product
                </button>
            </div>
            <div className="d-flex justify-content-center flex-wrap">
                <div className={styles['input-group']}>
                    <label htmlFor="search-input">Search:</label>
                    <input
                        id="search-input"
                        type="text"
                        // value={searchTerm}
                        placeholder="Search product"
                        className={`form-control ${styles['input-field']}`}
                    // onChange={(e) => {
                    //     setCurrentPage(1);
                    //     setSearchTerm(e.target.value);
                    // }}
                    />
                </div>
                <div className={styles['input-group']}>
                    <label htmlFor="order-select">Sort by:</label>
                    <select
                        id="order-select"
                        className={`form-control ${styles['input-field']}`}
                    // onChange={(e) => {
                    //     setCurrentPage(1);
                    //     setSorting(e.target.value);
                    // }}
                    >
                        <option value="0">Name (Ascending)</option>
                        <option value="1">Name (Descending)</option>
                        <option value="2">Price (Ascending)</option>
                        <option value="3">Price (Descending)</option>
                        <option value="4">Quantity (Ascending)</option>
                        <option value="5">Quantity (Descending)</option>
                        <option value="6">Delivery price (Ascending)</option>
                        <option value="7">Delivery price (Descending)</option>
                    </select>
                </div>
                <div className={styles['input-group']}>
                    <label htmlFor="order-select">Products per Page :</label>
                    <select
                        id="order-select"
                        className={`form-control ${styles['input-field']}`}
                    // onChange={(e) => {
                    //     setCurrentPage(1);
                    //     setClientsPerPage(e.target.value)
                    // }}
                    >
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
                            <th className={styles['text-left']}>Description</th>
                            <th className={styles['text-left']}>Price</th>
                            <th className={styles['text-left']}>Quantity</th>
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
                                //ToDO Implement product details
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
    );
}

export default ProductsPage;