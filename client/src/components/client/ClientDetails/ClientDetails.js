import styles from './ClientDetails.module.css'

function ClientDetails({ client, closeClientDetails, refreshClients }) {
    return (
        <div>
            <div className={styles['modal']}>
                <div className={styles['flex-container']}>
                    <ul className={styles['list-info']}>
                        <li>
                            <p>Name: {client.name}</p>
                        </li>
                        <li>
                            <p>Surname: {client.surname}</p>
                        </li>
                        <li>
                            <p>Lastname: {client.lastName}</p>
                        </li>
                        <li>
                            <p>Current credit: {client.currentCredit}</p>
                        </li>
                        <li>
                            <p>Credit limit: {client.creditLimit}</p>
                        </li>
                    </ul>
                    <div className={styles['buttons-container']}>
                        <button className={styles['update-button']}>Update</button>
                        <button className={styles['decr-credit-button']}>Decrease credit</button>
                        <button className={styles['delete-button']}>Delete</button>
                    </div>
                </div>
            </div>
            <div className={styles['backdrop']} />
        </div>
    );
};

export default ClientDetails;