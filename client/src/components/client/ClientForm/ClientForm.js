import styles from './ClientForm.module.css'
import { useState } from "react";
import { toast } from 'react-toastify'
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../context/AuthContext';
import { clientValidationRules, commonValidationRules } from "../../../validationRules";
import * as ClientService from "../../../services/clientService"

function ClientForm(props) {
    const [name, setName] = useState(`${props.client ? props.client.name : ""}`);
    const [surname, setSurname] = useState(`${props.client ? (props.client.surname ? props.client.surname : "") : ""}`);
    const [lastName, setLastName] = useState(`${props.client ? props.client.lastName : ""}`);;
    const [currentCredit, setCurrentCredit] = useState(`${props.client ? props.client.currentCredit.toFixed(2) : ""}`);
    const [creditLimit, setCreditLimit] = useState(`${props.client ? props.client.creditLimit.toFixed(2) : ""}`);
    const [validationErrors, setValidationErrors] = useState({});
    const { logout } = useAuth();
    const navigate = useNavigate();

    const submitHandler = async (event) => {
        event.preventDefault();

        if (Object.entries(validationErrors).length === 0) {
            try {
                const request = {
                    name,
                    surname,
                    lastName,
                    currentCredit,
                    creditLimit
                };
                props.client ? await ClientService.Update(props.client.id, request) : await ClientService.Create(request);
                props.closeAddNewClient();
                props.refreshClients();
                toast.success(`${props.client ? "Client updated successfully!" : "Client added successfully!"}`);
            } catch (error) {
                handleError(error);
            }
        }
    };

    const inputNameHandler = (event) => {
        const inputName = event.target.value;
        setName(inputName);
        validateNameInput(inputName);
    };

    const inputSurnameHandler = (event) => {
        const inputSurname = event.target.value;
        setSurname(inputSurname);
        validateSurnameInput(inputSurname);
    };

    const inputLastNameHandler = (event) => {
        const inputLastName = event.target.value;
        setLastName(inputLastName);
        validateLastNameInput(inputLastName);
    };

    const inputCurrentCreditHandler = (event) => {
        const inputCurrentCredit = event.target.value;
        setCurrentCredit(inputCurrentCredit);
        validateCurrentCreditInput(inputCurrentCredit);
    };

    const inputCreditLimitHandler = (event) => {
        const inputCreditLimit = event.target.value;
        setCreditLimit(inputCreditLimit);
        validateCreditLimitInput(inputCreditLimit);
    };

    const validateNameInput = (input) => {
        const errors = { ...validationErrors };
        const minLength = clientValidationRules.name.minLength;
        const maxLength = clientValidationRules.name.maxLength;
        if (!input) {
            errors.name = commonValidationRules.required('Name').message;
            setValidationErrors(errors);
        } else if (input.length < minLength || input.length > maxLength) {
            errors.name = commonValidationRules.length('Name', minLength, maxLength).message;
            setValidationErrors(errors);
        } else {
            delete errors.name;
            setValidationErrors(errors);
        }
    }

    const validateSurnameInput = (input) => {
        const errors = { ...validationErrors };
        const minLength = clientValidationRules.surname.minLength;
        const maxLength = clientValidationRules.surname.maxLength;
        if (input.length > maxLength) {
            errors.surname = commonValidationRules.length('Surname', minLength, maxLength).message;
            setValidationErrors(errors);
        } else {
            delete errors.surname;
            setValidationErrors(errors);
        }
    }

    const validateLastNameInput = (input) => {
        const errors = { ...validationErrors };
        const minLength = clientValidationRules.lastName.minLength;
        const maxLength = clientValidationRules.lastName.maxLength;
        if (!input) {
            errors.lastName = commonValidationRules.required('Lastname').message;
            setValidationErrors(errors);
        } else if (input.length < minLength || input.length > maxLength) {
            errors.lastName = commonValidationRules.length('Lastname', minLength, maxLength).message;
            setValidationErrors(errors);
        } else {
            delete errors.lastName;
            setValidationErrors(errors);
        }
    }

    const validateCurrentCreditInput = (input) => {
        const errors = { ...validationErrors };
        const minValue = clientValidationRules.currentCredit.minValue;
        const maxValue = clientValidationRules.currentCredit.maxValue;
        if (!input) {
            errors.currentCredit = commonValidationRules.required('Current credit').message;
            setValidationErrors(errors);
        } else if (input < minValue || input > maxValue) {
            errors.currentCredit = commonValidationRules.range('Current credit', minValue, maxValue).message;
            setValidationErrors(errors);
        } else {
            delete errors.currentCredit;
            setValidationErrors(errors);
        }
    }

    const validateCreditLimitInput = (input) => {
        const errors = { ...validationErrors };
        const minValue = clientValidationRules.creditLimit.minValue;
        const maxValue = clientValidationRules.creditLimit.maxValue;
        if (!input) {
            errors.creditLimit = commonValidationRules.required('Credit limit').message;
            setValidationErrors(errors);
        } else if (input < minValue || input > maxValue) {
            errors.creditLimit = commonValidationRules.range('Credit limit', minValue, maxValue).message;
            setValidationErrors(errors);
        } else {
            delete errors.creditLimit;
            setValidationErrors(errors);
        }
    }

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
                <h1 className={styles["header"]}>{`${props.client ? "Update Client" : "Add New Client"}`}</h1>
                <form onSubmit={submitHandler}>
                    <div className={styles['input-group']}>
                        <label htmlFor="name-input">Name:</label>
                        <input
                            id="name-input"
                            placeholder="Enter name"
                            className={styles["input"]}
                            value={name}
                            onChange={inputNameHandler}
                        />
                        {validationErrors.name && <p className={styles["error-message"]}>{validationErrors.name}</p>}
                    </div>
                    <div className={styles['input-group']}>
                        <label htmlFor="surname-input">Surname:</label>
                        <input
                            id="surname-input"
                            placeholder="Enter surname"
                            className={styles["input"]}
                            value={surname}
                            onChange={inputSurnameHandler}
                        />
                        {validationErrors.surname && <p className={styles["error-message"]}>{validationErrors.surname}</p>}
                    </div>
                    <div className={styles['input-group']}>
                        <label htmlFor="lastname-input">Lastname:</label>
                        <input
                            id="lastname-input"
                            placeholder="Enter lastname"
                            className={styles["input"]}
                            value={lastName}
                            onChange={inputLastNameHandler}
                        />
                        {validationErrors.lastName && <p className={styles["error-message"]}>{validationErrors.lastName}</p>}
                    </div>
                    <div className={styles['input-group']}>
                        <label htmlFor="current-credit-input">Current credit:</label>
                        <input
                            id="current-credit-input"
                            type="number"
                            placeholder="Enter current credit"
                            className={styles["input"]}
                            value={currentCredit}
                            onChange={inputCurrentCreditHandler}
                        />
                        {validationErrors.currentCredit && <p className={styles["error-message"]}>{validationErrors.currentCredit}</p>}
                    </div>
                    <div className={styles['input-group']}>
                        <label htmlFor="credit-limit-input">Credit limit:</label>
                        <input
                            id="credit-limit-input"
                            type="number"
                            placeholder="Enter credit limit"
                            className={styles["input"]}
                            value={creditLimit}
                            onChange={inputCreditLimitHandler}
                        />
                    </div>
                    {validationErrors.creditLimit && <p className={styles["error-message"]}>{validationErrors.creditLimit}</p>}
                    <div className={styles['buttons-container']}>
                        <button className={styles['button-cancel']} onClick={props.closeAddNewClient}>
                            Cancel
                        </button>
                        <button type="submit" className={styles["button-confirm"]}>
                            {`${props.client ? "Update" : "Add"}`}
                        </button>
                    </div>
                </form>
            </div>
            <div className={styles['backdrop']} />
        </div>
    );
};

export default ClientForm;