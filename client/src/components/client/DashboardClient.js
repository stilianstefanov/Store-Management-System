import styles from './DashboardClient.module.css'

function DashboardClient({ client, isSelected, select }) {
    return (
        <tr className={`${styles['row']} ${isSelected ? styles['selected-row'] : ''}`} onClick={() => select(client.id)}>
            <td>{client.name}</td>
            <td>{client.surname}</td>
            <td>{client.lastName}</td>
            <td>{client.currentCredit.toFixed(2)}</td>
            <td>{client.creditLimit.toFixed(2)}</td>
        </tr>
    );
}

export default DashboardClient;