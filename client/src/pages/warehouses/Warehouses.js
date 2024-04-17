import styles from './Warehouses.module.css';
import { useState, useEffect, useCallback } from 'react';
import ReactLoading from 'react-loading';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import TableWarehouse from '../../components/warehouse/TableWarehouse/TableWarehouse';

function WarehousesPage() {
    const warehouses = [{ id: '1', name: 'Kaufland', type: 'grocery', productsCount: 20 },
    { id: '1', name: 'Kaufland', type: 'grocery', productsCount: 20 },
    { id: '1', name: 'Kaufland', type: 'grocery', productsCount: 20 },
    { id: '1', name: 'Kaufland', type: 'grocery', productsCount: 20 },
    { id: '1', name: 'Kaufland', type: 'grocery', productsCount: 20 },
    { id: '1', name: 'Kaufland', type: 'grocery', productsCount: 20 },
    { id: '1', name: 'Kaufland', type: 'grocery', productsCount: 20 },
    { id: '1', name: 'Kaufland', type: 'grocery', productsCount: 20 },
    { id: '1', name: 'Kaufland', type: 'grocery', productsCount: 20 },
    { id: '1', name: 'Kaufland', type: 'grocery', productsCount: 20 },
    { id: '1', name: 'Kaufland', type: 'grocery', productsCount: 20 },
    { id: '1', name: 'Kaufland', type: 'grocery', productsCount: 20 },
    { id: '1', name: 'Kaufland', type: 'grocery', productsCount: 20 }];
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(0);
    const [warehousesPerPage, setWarehousesPerPage] = useState(10);
    const [searchTerm, setSearchTerm] = useState("");
    const [sorting, setSorting] = useState(0);
    const [isLoading, setIsLoading] = useState(false);
    const navigate = useNavigate();
    const { logout } = useAuth();

    const PAGE_BUTTONS_DISPLAY_LIMIT = 5;
    const startPage = Math.max(currentPage - Math.floor(PAGE_BUTTONS_DISPLAY_LIMIT / 2), 1);
    const endPage = Math.min(startPage + PAGE_BUTTONS_DISPLAY_LIMIT - 1, totalPages);

    const pageNumbers = Array.from({ length: (endPage - startPage) + 1 }, (_, index) => startPage + index);

    return (
        <div className={`container ${styles['table-container']}`}>
            <div className={styles['header-container']}>
                <div className={styles['spacer']}></div>
                <h1 className={`text-center ${styles['title']}`}>Warehouses</h1>
                <button
                    className={styles['add-warehouse-button']}
                // onClick={() => setWarehouseFormIsopen(true)} 
                >
                    Add New Warehouse
                </button>
            </div>
            <div className="d-flex justify-content-center flex-wrap">
                <div className={styles['input-group']}>
                    <label htmlFor="search-input">Search:</label>
                    <input
                        id="search-input"
                        type="text"
                        value={searchTerm}
                        placeholder="Search product"
                        className={`form-control ${styles['input-field']}`}
                        onChange={(e) => {
                            setCurrentPage(1);
                            setSearchTerm(e.target.value);
                        }} />
                </div>
                <div className={styles['input-group']}>
                    <label htmlFor="order-select">Sort by:</label>
                    <select
                        id="order-select"
                        className={`form-control ${styles['input-field']}`}
                        onChange={(e) => {
                            setCurrentPage(1);
                            setSorting(e.target.value);
                        }} >
                        <option value="0">Name (Ascending)</option>
                        <option value="1">Name (Descending)</option>
                        <option value="2">Type (Ascending)</option>
                        <option value="3">Type (Descending)</option>
                        <option value="4">Products count (Ascending)</option>
                        <option value="5">Products count (Descending)</option>
                    </select>
                </div>
                <div className={styles['input-group']}>
                    <label htmlFor="order-select">Warehouses per Page :</label>
                    <select
                        id="order-select"
                        className={`form-control ${styles['input-field']}`}
                        onChange={(e) => {
                            setCurrentPage(1);
                            setWarehousesPerPage(e.target.value)
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
                            <th className={styles['text-left']}>Type</th>
                            <th className={styles['text-left']}>Count of products</th>
                        </tr>
                    </thead>
                    <tbody className={styles['table-hover']}>
                        {isLoading ? (
                            <tr>
                                <td colSpan="3">
                                    <div className={styles['loading-container']}>
                                        <ReactLoading type="spin" color="#808080" />
                                    </div>
                                </td>
                            </tr>
                        ) : (
                            warehouses.map(warehouse => (
                                <TableWarehouse
                                    key={warehouse.id}
                                    warehouse={warehouse}
                                //ToDo Implement warehouse details
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

export default WarehousesPage;