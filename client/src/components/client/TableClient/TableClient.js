import styles from './TableClient.module.css'

function TableClient ({ client, openClientDetails }) {
    return (
        <tr onClick={() => openClientDetails(client.id)}>
            <td className={styles['text-left']}>{client.name}</td>
            <td className={styles['text-left']}>{client.surname}</td>
            <td className={styles['text-left']}>{client.lastName}</td>
            <td className={styles['text-left']}>{client.currentCredit}</td>
            <td className={styles['text-left']}>{client.creditLimit}</td>
        </tr>
    );
};

export default TableClient;