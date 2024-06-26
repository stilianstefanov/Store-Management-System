import styles from './InsufficientCreditModal.module.css'
import { toast } from 'react-toastify'
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../../context/AuthContext';
import { useState } from 'react';
import { clientValidationRules, commonValidationRules } from '../../../../validationRules';
import * as ClientService from '../../../../services/clientService';
import { useTranslation } from 'react-i18next';

function InsufficientCreditModal(props) {
    const [newCreditLimit, setNewCreditLimit] = useState("");
    const [validationError, setValidationError] = useState("");
    const { logout } = useAuth();
    const navigate = useNavigate();
    const { t } = useTranslation();

    const inputHandler = (event) => {
        const inputNewCreditLimit = event.target.value;
        setNewCreditLimit(inputNewCreditLimit);
        validateNewLimitInput(inputNewCreditLimit);
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

    const validateNewLimitInput = (input) => {
        if (!input) {
            setValidationError(commonValidationRules.required("New credit limit").message);
            return;
        }

        if (input < clientValidationRules.creditLimit.minValue || input > clientValidationRules.creditLimit.maxValue) {
            setValidationError(commonValidationRules.range(
                "New credit limit", clientValidationRules.creditLimit.minValue, clientValidationRules.creditLimit.maxValue).message);
            return;
        }
        setValidationError("");
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
                {t('insuffModal.header')}
                <p>{t('insuffModal.p')}</p>
            </h1>
            <ul>
                <li>
                    <p className={styles['p-curr-credit']}>{t('insuffModal.currCredit')} {props.client.currentCredit.toFixed(2)}</p>
                </li>
                <li>
                    <p className={styles['p-cred-limit']}>{t('insuffModal.limit')} {props.client.creditLimit.toFixed(2)}</p>
                </li>
                <li>
                    <p className={styles['p-total-cost']}>{t('insuffModal.total')} {props.totalCost.toFixed(2)}</p>
                </li>
                <li>
                    <p className={styles['p-insuff-amount']}>
                        {t('insuffModal.insuffAmount')} {(props.totalCost - (props.client.creditLimit - props.client.currentCredit)).toFixed(2)}
                    </p>
                </li>
            </ul>
            <form onSubmit={confirmHandler}>
                <input
                    type='number'
                    value={newCreditLimit}
                    placeholder={t('insuffModal.newLimit')}
                    className={`form-control ${styles.input}`}
                    onChange={inputHandler}
                />
                {validationError && <p className={styles["Error-message"]}>{validationError}</p>}
                <div className={styles['buttons-container']}>
                    <button className={styles['button-cancel']} onClick={props.closeCreditModal}>
                        {t('insuffModal.cancel')}
                    </button>
                    <button type="submit" className={styles["button-confirm"]}>
                        {t('insuffModal.confirm')}
                    </button>
                </div>
            </form>
        </div>
    );
}

export default InsufficientCreditModal;