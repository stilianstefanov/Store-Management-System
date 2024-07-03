import styles from './DecreaseCreditModal.module.css';
import { useState } from 'react';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../../context/AuthContext';
import { clientValidationRules, commonValidationRules } from '../../../../validationRules';
import * as ClientService from '../../../../services/clientService';
import { useTranslation } from 'react-i18next';

function DecreaseCreditModal({ client, closeModal, refreshClients }) {
    const [decreaseAmount, setDecreaseAmount] = useState("");
    const [validationError, setValidationError] = useState("");
    const navigate = useNavigate();
    const { logout } = useAuth();
    const { t } = useTranslation();

    const submitHandler = async (event) => {
        event.preventDefault();
        if (validationError) return;

        try {
            const request = { amount: decreaseAmount };
            await ClientService.DecreaseCredit(client.id, request);
            closeModal();
            refreshClients();
            toast.success("Credit decreased successfully!");
        } catch (error) {
            handleError(error);
        }
    };

    const inputAmountHandler = (event) => {
        const inputAmount = event.target.value;
        setDecreaseAmount(inputAmount);
        validateAmountInput(inputAmount);
    };

    const validateAmountInput = (input) => {
        const minValue = clientValidationRules.currentCredit.minValue;
        const maxValue = client.currentCredit;
        if (!input) {
            setValidationError(commonValidationRules.required("Amount").message);
        } else if (input < minValue || input > maxValue) {
            setValidationError(commonValidationRules.range('Amount', minValue, maxValue).message);
        } else {
            setValidationError("");
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
        <div>
            <div className={styles["container"]}>
                <h1 className={styles["header"]}>{t('decreaseCredit.header')}</h1>
                <form onSubmit={submitHandler}>
                    <div className={styles['input-group']}>
                        <label htmlFor="amount-input">{t('decreaseCredit.amountLabel')}</label>
                        <input
                            id="amount-input"
                            placeholder={t('decreaseCredit.amountInput')}
                            className={styles["input"]}
                            value={decreaseAmount}
                            onChange={inputAmountHandler}
                        />
                        {validationError && <p className={styles["error-message"]}>{validationError}</p>}
                    </div>
                    <div className={styles['buttons-container']}>
                        <button className={styles['button-cancel']} onClick={closeModal}>
                            {t('decreaseCredit.cancel')}
                        </button>
                        <button type="submit" className={styles["button-confirm"]}>
                            {t('decreaseCredit.confirm')}
                        </button>
                    </div>
                </form>
            </div>
            <div className={styles['backdrop']} />
        </div>
    );
}

export default DecreaseCreditModal;