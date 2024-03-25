import styles from './InsufficientCreditModal.module.css'
import { toast } from 'react-toastify'
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../../context/AuthContext';
import { useState } from 'react';
import * as ClientService from '../../../../services/clientService'

function InsufficientCreditModal(props) {
    const [newCreditLimit, setNewCreditLimit] = useState("");
    const [validationError, setValidationError] = useState("");
    const { logout } = useAuth();
    const navigate = useNavigate();

    const inputHandler = (event) => {
        const inputNewCreditLimit = event.target.value;
        setNewCreditLimit(inputNewCreditLimit);

        if (!inputNewCreditLimit || inputNewCreditLimit < 0 || inputNewCreditLimit > 99999) {
            setValidationError('The new credit limit should be between 0 and 99999!');
            return;
        }
        setValidationError("");
    };

    const confirmHandler = async (event) => {
        event.preventDefault();
        if (validationError) return;

        try {
            const updateLimitRequest = {
                creditLimit: Number(newCreditLimit)
            };
            await ClientService.PartialUpdate(props.client.id, updateLimitRequest);
            props.updateCreditLimit(props.client.id, newCreditLimit);
            props.closeCreditModal();
        } catch (error) {
            handleError(error);
        }
    };

    const handleError = (error) => {
        if (error.response && error.response.status === 401) {
            logout();
            navigate('/login');
            toast.warning('Your session has expired. Please login again.');
        } else {
            toast.error(error.response.data);
        }
        console.error(error);
    }

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
            <form onSubmit={confirmHandler}>
                <input
                    type='number'
                    value={newCreditLimit}
                    placeholder="New credit limit"
                    className={`form-control ${styles.input}`}
                    onChange={inputHandler}
                />
                {validationError && <p className={styles["Error-message"]}>{validationError}</p>}
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