import styles from './ClientDetails.module.css'

function ClientDetails({ client, closeClientDetails, refreshClients }) {
    return (
        <div>
            <div className={styles['modal']}>
                {client.name}
            </div>
            <div className={styles['backdrop']} />
        </div>
    );
};

export default ClientDetails;