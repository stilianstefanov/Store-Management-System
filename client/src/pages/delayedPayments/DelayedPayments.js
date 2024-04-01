import styles from './DelayedPayments.module.css'

function DelayedPaymentsPage() {
    return (
        <div className={`container ${styles['table-container']}`}>
            <h1 className={`text-center ${styles['title']}`}>Clients</h1>
        </div>
    );
}

export default DelayedPaymentsPage;