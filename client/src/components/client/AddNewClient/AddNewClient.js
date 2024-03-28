import { useState } from "react";

function AddNewClient() {
    const [name, setName] = useState("");
    const [surname, setSurname] = useState("");
    const [lastName, setLastName] = useState("");
    const [currentCredit, setCurrentCredit] = useState(0);
    const [creditLimit, setCreditLimit] = useState(0);
    const [validationErrors, setValidationErrors] = useState({});

    const inputNameHandler = (event) => {

    };

    const inputSurnameHandler = (event) => {

    };

    const inputLastNameHandler = (event) => {

    };

    const inputCurrentCreditHandler = (event) => {

    };

    const inputCreditLimitHandler = (event) => {

    };

    return (
        <div className={styles["container"]}>
            <div className={styles["card"]}>
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
                        <button className={styles['button-cancel']}>
                            Cancel
                        </button>
                        <button type="submit" className={styles["button-confirm"]}>
                            Add
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default AddNewClient;