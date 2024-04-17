import { useState } from 'react';
import styles from './WarehouseForm.module.css';
import { toast } from 'react-toastify'
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../context/AuthContext';
import { warehouseValidationRules, commonValidationRules } from '../../../validationRules';
import * as WarehouseService from '../../../services/warehouseService';

function WarehouseForm(props) {
    const isUpdate = props.warehouse ? true : false;
    const [name, setName] = useState(`${isUpdate ? props.warehouse.name : ""}`);
    const [type, setType] = useState(`${isUpdate ? props.warehouse.type : ""}`);
    const [validationErrors, setValidationErrors] = useState({});
    const { logout } = useAuth();
    const navigate = useNavigate();

    const submitHandler = async (event) => {
        event.preventDefault();

        if (Object.entries(validationErrors).length === 0) {
            try {
                const request = {
                    name,
                    type
                };
                isUpdate ? await WarehouseService.Update(props.warehouse.id, request) : await WarehouseService.Create(request);
                props.closeForm();
                props.refreshWarehouses();
                toast.success(`${isUpdate? "Warehouse updated successfully!" : "Warehouse added successfully!"}`);
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

    const inputTypeHandler = (event) => {
        const inputType = event.target.value;
        setType(inputType);
        validateTypeInput(inputType);
    };

    const validateNameInput = (input) => {
        const errors = { ...validationErrors };
        const minLength = warehouseValidationRules.name.minLength;
        const maxLength = warehouseValidationRules.name.maxLength;
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

    const validateTypeInput = (input) => {
        const errors = { ...validationErrors };
        const minLength = warehouseValidationRules.type.minLength;
        const maxLength = warehouseValidationRules.type.maxLength;
        if (!input) {
            errors.type = commonValidationRules.required('Type').message;
            setValidationErrors(errors);
        } else if (input.length < minLength || input.length > maxLength) {
            errors.type = commonValidationRules.length('Type', minLength, maxLength).message;
            setValidationErrors(errors);
        } else {
            delete errors.type;
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
                <h1 className={styles["header"]}>{`${isUpdate ? "Update Warehouse" : "Add New Warehouse"}`}</h1>
                <form onSubmit={submitHandler}>
                    <div className={styles['input-group']}>
                        <label htmlFor="name-input">Name:</label>
                        <input
                            id="name-input"
                            placeholder="Enter warehouse name"
                            className={styles["input"]}
                            value={name}
                            onChange={inputNameHandler}
                        />
                        {validationErrors.name && <p className={styles["error-message"]}>{validationErrors.name}</p>}
                    </div>
                    <div className={styles['input-group']}>
                        <label htmlFor="type-input">Type:</label>
                        <input
                            id="type-input"
                            placeholder="Enter warehouse type"
                            className={styles["input"]}
                            value={type}
                            onChange={inputTypeHandler}
                        />
                        {validationErrors.type && <p className={styles["error-message"]}>{validationErrors.type}</p>}
                    </div>
                    <div className={styles['buttons-container']}>
                        <button className={styles['button-cancel']} onClick={props.closeForm}>
                            Cancel
                        </button>
                        <button type="submit" className={styles["button-confirm"]}>
                            {`${isUpdate ? "Update" : "Add"}`}
                        </button>
                    </div>
                </form>
            </div>
            <div className={styles['backdrop']} />
        </div>
    );
};

export default WarehouseForm;