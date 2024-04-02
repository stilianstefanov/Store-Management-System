import styles from './TableClient.module.css'

function TableClient ({ client }) {
    return (
        <tr>
            <td className={styles['text-left']}>{client.name}</td>
            <td className={styles['text-left']}>{client.surname}</td>
            <td className={styles['text-left']}>{client.lastName}</td>
            <td className={styles['text-left']}>{client.currentCredit}</td>
            <td className={styles['text-left']}>{client.creditLimit}</td>
        </tr>
    );
};

export default TableClient;