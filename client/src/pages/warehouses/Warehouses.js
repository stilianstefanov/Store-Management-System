import styles from './Warehouses.module.css';
import { useState, useEffect, useCallback } from 'react';
import ReactLoading from 'react-loading';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import TableWarehouse from '../../components/warehouse/TableWarehouse/TableWarehouse';
import WarehouseForm from '../../components/warehouse/WarehouseForm/WarehouseForm';
import WarehouseDetails from '../../components/warehouse/WarehouseDetails/WarehouseDetails';
import * as WarehouseService from '../../services/warehouseService';
import { useTranslation } from 'react-i18next';

function WarehousesPage() {
    const [warehouses, setWarehouses] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(0);
    const [warehousesPerPage, setWarehousesPerPage] = useState(10);
    const [searchTerm, setSearchTerm] = useState("");
    const [sorting, setSorting] = useState(0);
    const [isLoading, setIsLoading] = useState(false);
    const [warehouseFormIsOpen, setWarehouseFormIsOpen] = useState(false);
    const [warehouseDetailsIsOpen, setWarehouseDetailsIsOpen] = useState(false);
    const [selectedWarehouseId, setSelectedWarehouseId] = useState("");
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

    const getWarehouses = useCallback(async () => {
        setIsLoading(true);
        try {
            const request = {
                currentPage,
                warehousesPerPage,
                searchTerm,
                sorting
            };
            const response = await WarehouseService.GetAll(request);
            setWarehouses(response.warehouses);
            setTotalPages(response.totalPages);
        } catch (error) {
            handleError(error);
        } finally {
            setIsLoading(false);
        }
    }, [handleError, currentPage, warehousesPerPage, sorting, searchTerm]);

    useEffect(() => {
        getWarehouses();
    }, [getWarehouses]);

    const openWarehouseDetailsHandler = (warehouseId) => {
        setSelectedWarehouseId(warehouseId);
        setWarehouseDetailsIsOpen(true);
    }

    const PAGE_BUTTONS_DISPLAY_LIMIT = 5;
    const startPage = Math.max(currentPage - Math.floor(PAGE_BUTTONS_DISPLAY_LIMIT / 2), 1);
    const endPage = Math.min(startPage + PAGE_BUTTONS_DISPLAY_LIMIT - 1, totalPages);

    const pageNumbers = Array.from({ length: (endPage - startPage) + 1 }, (_, index) => startPage + index);

    return (
        <div className={`container ${styles['table-container']}`}>
            <div className={styles['header-container']}>
                <div className={styles['spacer']}></div>
                <h1 className={`text-center ${styles['title']}`}>{t('warehouses.header')}</h1>
                <button
                    className={styles['add-warehouse-button']}
                    onClick={() => setWarehouseFormIsOpen(true)}
                >
                    {t('warehouses.add')}
                </button>
            </div>
            <div className="d-flex justify-content-center flex-wrap">
                <div className={styles['input-group']}>
                    <label htmlFor="search-input">{t('warehouses.searchLabel')}</label>
                    <input
                        id="search-input"
                        type="text"
                        value={searchTerm}
                        placeholder={t('warehouses.searchInput')}
                        className={`form-control ${styles['input-field']}`}
                        onChange={(e) => {
                            setCurrentPage(1);
                            setSearchTerm(e.target.value);
                        }} />
                </div>
                <div className={styles['input-group']}>
                    <label htmlFor="order-select">{t('warehouses.sortLabel')}</label>
                    <select
                        id="order-select"
                        className={`form-control ${styles['input-field']}`}
                        onChange={(e) => {
                            setCurrentPage(1);
                            setSorting(e.target.value);
                        }} >
                        <option value="0">{t('warehouses.sortInput.nameAsc')}</option>
                        <option value="1">{t('warehouses.sortInput.nameDesc')}</option>
                        <option value="2">{t('warehouses.sortInput.productCntAsc')}</option>
                        <option value="3">{t('warehouses.sortInput.productCntDesc')}</option>
                    </select>
                </div>
                <div className={styles['input-group']}>
                    <label htmlFor="order-select">{t('warehouses.perPageLabel')}</label>
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
                            <th className={styles['text-left']}>{t('warehouses.table.name')}</th>
                            <th className={styles['text-left']}>{t('warehouses.table.type')}</th>
                            <th className={styles['text-left']}>{t('warehouses.table.count')}</th>
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
                                    openWarehouseDetails={openWarehouseDetailsHandler}
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
                        {t('warehouses.prevButton')}
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
                        {t('warehouses.nextButton')}
                    </button>
                )}
            </div>
            {warehouseFormIsOpen && <WarehouseForm
                closeForm={() => setWarehouseFormIsOpen(false)}
                refreshWarehouses={() => getWarehouses()} />}
            {warehouseDetailsIsOpen && <WarehouseDetails
                warehouse={warehouses.find(w => w.id === selectedWarehouseId)}
                closeWarehouseDetails={() => setWarehouseDetailsIsOpen(false)}
                refreshWarehouses={() => getWarehouses()} />}
        </div>
    );
}

export default WarehousesPage;