import styles from './ClientDetails.module.css'

function ClientDetails({ client, closeClientDetails, refreshClients }) {
    return (
        <div>
            <div className={styles['modal']}>
                {client.name}
            </div>
        </div>
    );
};

export default ClientDetails;