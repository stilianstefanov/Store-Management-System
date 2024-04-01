import styles from './DelayedPayments.module.css'

function DelayedPaymentsPage() {
    return (
        <div className={`container ${styles['table-container']}`}>
        <h1 className={`text-center ${styles['title']}`}>Clients</h1>
        <div className="d-flex justify-content-center flex-wrap">
            <div className={styles['input-group']}>
                <label htmlFor="search-input">Search:</label>
                <input
                    id="search-input"
                    type="text"
                    placeholder="Search client"
                    className={`form-control ${styles['input-field']}`}
                />
            </div>
            <div className={styles['input-group']}>
                <label htmlFor="order-select">Sort by:</label>
                <select id="order-select" className={`form-control ${styles['input-field']}`}>
                    <option value="0">Name (Ascending)</option>
                    <option value="1">Name (Descending)</option>
                    <option value="2">Current credit (Descending)</option>
                    <option value="3">Current credit (Ascending)</option>
                    <option value="4">Credit limit (Descending)</option>
                    <option value="4">Credit limit (Ascending)</option>
                </select>
            </div>
            <div className={styles['input-group']}>
                <label htmlFor="order-select">Clients per Page :</label>
                <select id="order-select" className={`form-control ${styles['input-field']}`}>
                    <option value="0">10</option>
                    <option value="1">15</option>
                    <option value="2">20</option>
                </select>
            </div>
        </div>
    </div>
    );
}

export default DelayedPaymentsPage;