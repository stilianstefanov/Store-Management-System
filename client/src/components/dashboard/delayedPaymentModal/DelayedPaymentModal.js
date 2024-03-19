import styles from './DelayedPaymentModal.module.css'

function DelayedPaymentModal(props) {
    return (
        <div>
            <div className={styles['modal']}>
                <p>Are you sure?</p>
                <button onClick={props.onCancel} >
                    Cancel
                </button>
                <button >
                    Confirm
                </button>

            </div>
            <div className={styles['backdrop']} />
        </div>
    );
}

export default DelayedPaymentModal;