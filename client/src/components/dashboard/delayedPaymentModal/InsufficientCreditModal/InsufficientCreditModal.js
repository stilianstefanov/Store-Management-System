import styles from './InsufficientCreditModal.module.css'

function InsufficientCreditModal() {
    return (
        <div className={styles["modal"]}>
                <h1 className={styles["header"]}>
                    It looks like the selected client does not have enough credit.
                    <br>Do you want to update the credit limit?</br>
                </h1>
                <form>
                    <input
                        type='number'
                        placeholder="New credit limit"
                        className={`form-control ${styles.input}`}
                        value={email}
                    />
                    <button className={styles['button-cancel']}>
                        Cancel
                    </button>
                    <button type="submit" className={styles["button-confirm"]}>
                        Confirm
                    </button>
                </form>
            <div className={styles['backdrop']} />
        </div>
    );
}

export default InsufficientCreditModal;