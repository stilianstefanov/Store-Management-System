import styles from './TableWarehouse.module.css';

function TableWarehouse ({ warehouse, openWarehouseDetails }) {
    return (
        <tr onClick={() => openWarehouseDetails(warehouse.id)}>
            <td className={styles['text-left']}>{warehouse.name}</td>
            <td className={styles['text-left']}>{warehouse.type}</td>
            <td className={styles['text-left']}>{warehouse.productsCount}</td>
        </tr>
    );
};

export default TableWarehouse;