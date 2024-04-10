import styles from './DecreaseCreditModal.module.css'

function DecreaseCreditModal({ clientId, closeModal, refreshClients }) {
    return (
        <div>
            <div className={styles["container"]}>
                <h1 className={styles["header"]}>Decrease current credit</h1>
                <form>
                    <div className={styles['input-group']}>
                        <label htmlFor="amount-input">Amount:</label>
                        <input
                            id="amount-input"
                            placeholder="Enter amount to decrease"
                            className={styles["input"]}

                        />
                    </div>
                    <div className={styles['buttons-container']}>
                        <button className={styles['button-cancel']} onClick={closeModal}>
                            Cancel
                        </button>
                        <button type="submit" className={styles["button-confirm"]}>
                            Confirm
                        </button>
                    </div>
                </form>
            </div>
            <div className={styles['backdrop']} />
        </div>
    );
}

export default DecreaseCreditModal;