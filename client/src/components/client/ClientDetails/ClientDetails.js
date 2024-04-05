import styles from './ClientDetails.module.css'

function ClientDetails({ client, closeClientDetails, refreshClients }) {
    return (
        <div>
            <div className={styles['modal']}>
                <div className={styles['flex-container']}>
                    <ul className={styles['list-info']}>
                        <li>
                            <p>Name: <span className={styles['list-info-span']}>{client.name}</span></p>
                        </li>
                        <li>
                            <p>Surname: <span className={styles['list-info-span']}>{client.surname ? client.surname : 'N/A'}</span></p>
                        </li>
                        <li>
                            <p>Lastname: <span className={styles['list-info-span']}>{client.lastName}</span></p>
                        </li>
                        <li>
                            <p>Current credit: <span className={styles['list-info-span']}>{client.currentCredit}</span></p>
                        </li>
                        <li>
                            <p>Credit limit: <span className={styles['list-info-span']}>{client.creditLimit}</span></p>
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