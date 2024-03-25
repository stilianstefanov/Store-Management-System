import styles from './InsufficientCreditModal.module.css'

function InsufficientCreditModal(props) {
    return (
        <div className={styles["modal"]}>
            <h1 className={styles["header"]}>
                It looks like the selected client does not have enough credit.
                <p>Do you want to update the credit limit?</p>
            </h1>
            <ul>
                <li>
                    <p className={styles['p-curr-credit']}>Current credit: {props.client.currentCredit.toFixed(2)}</p>
                </li>
                <li>
                    <p className={styles['p-cred-limit']}>Credit limit: {props.client.creditLimit.toFixed(2)}</p>
                </li>
                <li>
                    <p className={styles['p-total-cost']}>Total cost: {props.totalCost.toFixed(2)}</p>
                </li>
                <li>
                    <p className={styles['p-insuff-amount']}>
                        Insufficient amount: {(props.totalCost - (props.client.creditLimit - props.client.currentCredit)).toFixed(2)}
                    </p>
                </li>
            </ul>
            <form>
                <input
                    type='number'
                    placeholder="New credit limit"
                    className={`form-control ${styles.input}`}

                />
                <div className={styles['buttons-container']}>
                    <button className={styles['button-cancel']} onClick={props.closeCreditModal}>
                        Cancel
                    </button>
                    <button type="submit" className={styles["button-confirm"]}>
                        Confirm
                    </button>
                </div>
            </form>
        </div>
    );
}

export default InsufficientCreditModal;