import styles from './TableClient.module.css'

function TableClient ({ client, openClientDetails }) {
    return (
        <tr onClick={() => openClientDetails(client.id)}>
            <td className={styles['text-left']}>{client.name}</td>
            <td className={styles['text-left']}>{client.surname}</td>
            <td className={styles['text-left']}>{client.lastName}</td>
            <td className={styles['text-left']}>{client.currentCredit.toFixed(2)}</td>
            <td className={styles['text-left']}>{client.creditLimit.toFixed(2)}</td>
        </tr>
    );
};

export default TableClient;