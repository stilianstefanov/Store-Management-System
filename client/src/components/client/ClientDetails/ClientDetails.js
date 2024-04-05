import styles from './ClientDetails.module.css'

function ClientDetails({ client, closeClientDetails, refreshClients }) {
    const testPurchases = [
        { date: '19-12-2022', amount: 20.54 },
        { date: '19-12-2022', amount: 20.54 },
        { date: '19-12-2022', amount: 20.54 },
        { date: '19-12-2022', amount: 20.54 },
        { date: '19-12-2022', amount: 20.54 },
        { date: '19-12-2022', amount: 20.54 },
        { date: '19-12-2022', amount: 20.54 },
        { date: '19-12-2022', amount: 20.54 },
        { date: '19-12-2022', amount: 20.54 },
        { date: '19-12-2022', amount: 20.54 }
    ]

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
                <h2 className={styles['list-info-span']}>Purchases</h2>
                <div className="d-flex justify-content-center flex-wrap">
                    <div className={styles['input-group']}>
                        <label htmlFor="select-date">Date:</label>
                        <input
                            id="select-date"
                            type="date"
                            placeholder="Select date"
                            className={`form-control ${styles['input-field']}`}
                        />
                    </div>
                    <div className={styles['input-group']}>
                        <label htmlFor="order-select">Sort by:</label>
                        <select
                            id="order-select"
                            className={`form-control ${styles['input-field']}`}
                        >
                            <option value="0">Date (Latest)</option>
                            <option value="1">Date (Oldest)</option>
                            <option value="2">Amount (Descending)</option>
                            <option value="3">Amount (Ascending)</option>
                        </select>
                    </div>
                </div>
                <div className={`table-responsive ${styles['table-wrapper']}`}>
                    <table className={styles['table-fill']}>
                        <thead>
                            <tr>
                                <th className={styles['text-left']}>Date</th>
                                <th className={styles['text-left']}>Amount</th>
                            </tr>
                        </thead>
                        <tbody className={styles['table-hover']}>
                            {testPurchases.map(p => {
                                return (
                                    <tr>
                                        <td>{p.date}</td>
                                        <td>{p.amount}</td>
                                    </tr>
                                )
                            })}
                        </tbody>
                    </table>
                </div>
            </div>
            <div className={styles['backdrop']} />
        </div>
    );
};

export default ClientDetails;