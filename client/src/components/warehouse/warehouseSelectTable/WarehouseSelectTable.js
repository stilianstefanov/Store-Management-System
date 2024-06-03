import styles from './WarehouseSelectTable.module.css';
import { useState, useEffect, useCallback } from 'react';
import ReactLoading from 'react-loading';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../context/AuthContext';
import { toast } from 'react-toastify';
import WarehouseRow from './WarehouseRow/WarehouseRow';
import * as WarehouseService from '../../../services/warehouseService';
import { useTranslation } from 'react-i18next';

function WarehouseSelectTable({ selectedWarehouseId, selectWarehouseHandler }) {
    const [warehouses, setWarehouses] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
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
        } catch (error) {
            handleError(error);
        } finally {
            setIsLoading(false);
        }
    }, [handleError, searchTerm]);

    useEffect(() => {
        getWarehouses();
    }, [getWarehouses]);

    return (
        <div className={styles['main-container']}>
            <h3 className={styles['header']}>{t('selectWarehouseTable.header')}</h3>
            <input
                type='text'
                value={searchTerm}
                placeholder={t('selectWarehouseTable.searchInput')}
                className={`form-control ${styles.input}`}
                onChange={(e) => setSearchTerm(e.target.value)}
            />
            <div className={`table-responsive ${styles['table-wrapper']}`}>
                <table className={styles['tableCustom']}>
                    <thead className={styles['tableHeader']}>
                        <tr>
                            <th>{t('selectWarehouseTable.name')}</th>
                            <th>{t('selectWarehouseTable.type')}</th>
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
        </div>
    );
};

export default WarehouseSelectTable;