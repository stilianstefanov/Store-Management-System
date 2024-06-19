import styles from './ChangeWarehouse.module.css'
import { useState, useEffect, useCallback } from 'react';
import ReactLoading from 'react-loading';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../../context/AuthContext';
import { toast } from 'react-toastify';
import WarehouseRow from '../../../warehouse/warehouseSelectTable/WarehouseRow/WarehouseRow';
import * as WarehouseService from '../../../../services/warehouseService';
import * as ProductService from '../../../../services/productService';
import { useTranslation } from 'react-i18next';

function ChangeWarehouse({ productId, updateProduct, closeModal }) {
    const [warehouses, setWarehouses] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [selectedWarehouseId, setSelectedWarehouseId] = useState("");
    const [isLoading, setIsLoading] = useState(false);
    const { logout } = useAuth();
    const navigate = useNavigate();
    const { t } = useTranslation();

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

    const getWarehouses = useCallback(async () => {
        setIsLoading(true);
        try {
            const request = { searchTerm };
            const response = await WarehouseService.GetAll(request);
            setWarehouses(response.warehouses);
            selectWarehouseHandler("");
        } catch (error) {
            handleError(error);
        } finally {
            setIsLoading(false);
        }
    }, [handleError, searchTerm]);

    useEffect(() => {
        getWarehouses();
    }, [getWarehouses]);

    const selectWarehouseHandler = (id) => {
        setSelectedWarehouseId(id);
    };

    const confirmHandler = async () => {
        if (!selectedWarehouseId) return;
        try {
            const request = {
                warehouseId: selectedWarehouseId
            };
            const response = await ProductService.PartialUpdate(productId, request);
            updateProduct(response);
            closeModal();
            toast.success('Warehouse changed successfully!');
        } catch (error) {
            handleError(error);
        }
    };

    return (
        <div>
            <div className={styles['main-container']}>
                <h3 className={styles['header']}>{t('changeWarehouse.header')}</h3>
                <input
                    type='text'
                    value={searchTerm}
                    placeholder={t('changeWarehouse.search')}
                    className={`form-control ${styles.input}`}
                    onChange={(e) => setSearchTerm(e.target.value)}
                />
                <div className={`table-responsive ${styles['table-wrapper']}`}>
                    <table className={styles['tableCustom']}>
                        <thead className={styles['tableHeader']}>
                            <tr>
                                <th>{t('changeWarehouse.table.name')}</th>
                                <th>{t('changeWarehouse.table.type')}</th>
                            </tr>
                        </thead>
                        <tbody>
                            {isLoading ? (
                                <tr>
                                    <td colSpan="2">
                                        <div className={styles['loading-container']}>
                                            <ReactLoading type="spin" color="#808080" />
                                        </div>
                                    </td>
                                </tr>
                            ) : (
                                warehouses.map(warehouse => (
                                    <WarehouseRow
                                        key={warehouse.id}
                                        warehouse={warehouse}
                                        isSelected={selectedWarehouseId === warehouse.id}
                                        select={selectWarehouseHandler}
                                    />
                                ))
                            )}
                        </tbody>
                    </table>
                </div>
                <div className={styles['buttons-container']}>
                    <button className={styles['button-cancel']} onClick={closeModal}>
                        {t('changeWarehouse.cancel')}
                    </button>
                    <button type="submit" className={styles["button-confirm"]} onClick={confirmHandler}>
                        {t('changeWarehouse.confirm')}
                    </button>
                </div>
            </div>
            <div className={styles['backdrop']} />
        </div>
    );
}

export default ChangeWarehouse;