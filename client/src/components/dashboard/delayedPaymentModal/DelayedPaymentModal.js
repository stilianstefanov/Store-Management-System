import styles from './DelayedPaymentModal.module.css'
import ReactLoading from 'react-loading'
import { useState } from 'react';
import DashboardClient from '../../client/DashboardClient';

function DelayedPaymentModal(props) {
    const [clients, setClients] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [isLoading, setIsLoading] = useState(false);

    return (
        <div>
            <div className={styles['modal']}>
                <h1 className={styles['header']}>Select client</h1>
                <input
                    type='text'
                    value={searchTerm}
                    placeholder='Search'
                    className={`form-control ${styles.input}`}
                />
                <div className={`table-responsive ${styles['table-wrapper']}`}>
                    <table className={`table table-striped ${styles.tableCustom}`}>
                        <thead className={styles.tableHeader}>
                            <tr>
                                <th>Name</th>
                                <th>Surname</th>
                                <th>Lastname</th>
                                <th>Credit</th>
                                <th>Limit</th>
                            </tr>
                        </thead>
                        <tbody>
                            {isLoading ? (
                                <tr>
                                    <td colSpan="3">
                                        <div className={styles['loading-container']}>
                                            <ReactLoading type="spin" color="#808080" />
                                        </div>
                                    </td>
                                </tr>
                            ) : (
                                clients.map(client => (
                                    <DashboardClient
                                        key={client.id}
                                        client={client}
                                    />
                                ))
                            )}
                        </tbody>
                    </table>
                </div>
                <div className={styles['buttons-container']}>
                    <button className={styles['button-cancel']} onClick={props.onCancel} >
                        Cancel
                    </button>
                    <button className={styles['button-confirm']}>
                        Confirm
                    </button>
                </div>
            </div>
            <div className={styles['backdrop']} />
        </div>
    );
}

export default DelayedPaymentModal;