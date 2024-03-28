import styles from './AddNewClient.module.css'
import { useState } from "react";
import { clientValidationRules, commonValidationRules } from "../../../validationRules";

function AddNewClient(props) {
    const [name, setName] = useState("");
    const [surname, setSurname] = useState("");
    const [lastName, setLastName] = useState("");
    const [currentCredit, setCurrentCredit] = useState("");
    const [creditLimit, setCreditLimit] = useState("");
    const [validationErrors, setValidationErrors] = useState({});

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

    return (
        <div className={styles["container"]}>
            <h1 className={styles["header"]}>Add New Client</h1>
            <form>
                <input
                    placeholder="Name"
                    className={styles["input"]}
                    value={name}
                    onChange={inputNameHandler}
                />
                {validationErrors.name && <p className={styles["error-message"]}>{validationErrors.name}</p>}
                <input
                    placeholder="Surname"
                    className={styles["input"]}
                    value={surname}
                    onChange={inputSurnameHandler}
                />
                {validationErrors.surname && <p className={styles["error-message"]}>{validationErrors.surname}</p>}
                <input
                    placeholder="Lastname"
                    className={styles["input"]}
                    value={lastName}
                    onChange={inputLastNameHandler}
                />
                {validationErrors.lastName && <p className={styles["error-message"]}>{validationErrors.lastName}</p>}
                <input
                    type="number"
                    placeholder="Current credit"
                    className={styles["input"]}
                    value={currentCredit}
                    onChange={inputCurrentCreditHandler}
                />
                {validationErrors.currentCredit && <p className={styles["error-message"]}>{validationErrors.currentCredit}</p>}
                <input
                    type="number"
                    placeholder="Credit limit"
                    className={styles["input"]}
                    value={creditLimit}
                    onChange={inputCreditLimitHandler}
                />
                {validationErrors.creditLimit && <p className={styles["error-message"]}>{validationErrors.creditLimit}</p>}
                <div className={styles['buttons-container']}>
                    <button className={styles['button-cancel']} onClick={props.closeAddNewClient}>
                        Cancel
                    </button>
                    <button type="submit" className={styles["button-confirm"]}>
                        Add
                    </button>
                </div>
            </form>
        </div>
    );
};

export default AddNewClient;