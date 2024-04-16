import styles from './WarehouseRow.module.css'

function WarehouseRow({ warehouse, isSelected, select }) {
    return (
        <tr className={`${styles['row']} ${isSelected ? styles['selected-row'] : ''}`} onClick={() => select(warehouse.id)}>
            <td>{warehouse.name}</td>
            <td>{warehouse.type}</td>
        </tr>
    );
}

export default WarehouseRow;