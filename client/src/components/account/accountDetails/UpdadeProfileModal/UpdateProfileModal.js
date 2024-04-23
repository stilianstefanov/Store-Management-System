import styles from './UpdateProfileModal.module.css';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../../context/AuthContext';
import { useState } from 'react';
import { commonValidationRules, registerValidationRules } from '../../../../validationRules';
import * as UserService from '../../../../services/userService';

function UpdateProfileModal({ closeModal, claims }) {
    const [userName, setUserName] = useState(claims.unique_name);
    const [email, setEmail] = useState(claims.email);
    const [companyName, setCompanyName] = useState(claims.companyName);
    const [validationErrors, setValidationErrors] = useState({});
    const { logout, login } = useAuth();
    const navigate = useNavigate();

    const submitHandler = async (event) => {
        event.preventDefault();

        if (validate()) {
            const request = {
                email,
                userName,
                companyName
            };

            try {
                const data = await UserService.UpdateProfile(request);
                login(data);
                closeModal();
                toast.success('Your profile info was updated successfully!');
            } catch (error) {
                handleError(error);
            }
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

    const validate = () => {
        const errors = {};

        if (!email) {
            errors.email = commonValidationRules.required('Email').message;
        } else if (!registerValidationRules.email.pattern.test(email)) {
            errors.email = registerValidationRules.email.message;
        }

        if (!userName) {
            errors.userName = commonValidationRules.required('Username').message;
        } else if (userName.length < registerValidationRules.userName.minLength
            || userName.length > registerValidationRules.userName.maxLength) {

            errors.userName = commonValidationRules.length(
                'Username', registerValidationRules.userName.minLength, registerValidationRules.userName.maxLength).message;
        }

        if (!companyName) {
            errors.companyName = commonValidationRules.required("Company name").message;
        } else if (companyName < registerValidationRules.companyName.minLength
            || companyName > registerValidationRules.companyName.maxLength) {

            errors.companyName = commonValidationRules.length(
                'Company name', registerValidationRules.companyName.minLength, registerValidationRules.companyName.maxLength).message;
        }

        setValidationErrors(errors);
        return Object.keys(errors).length === 0;
    };

    return (
        <div className={styles["Auth-container"]}>
            <div className={styles["Auth-card"]}>
                <h1 className={styles["Auth-header"]}>Update Profile</h1>
                <form onSubmit={submitHandler}>
                    <input
                        placeholder="New email"
                        className={styles["Auth-input"]}
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                    {validationErrors.email && <p className={styles["Error-message"]}>{validationErrors.email}</p>}
                    <input
                        placeholder="New username"
                        className={styles["Auth-input"]}
                        value={userName}
                        onChange={(e) => setUserName(e.target.value)}
                    />
                    {validationErrors.userName && <p className={styles["Error-message"]}>{validationErrors.userName}</p>}
                    <input
                        placeholder="New company name"
                        className={styles["Auth-input"]}
                        value={companyName}
                        onChange={(e) => setCompanyName(e.target.value)}
                    />
                    {validationErrors.companyName && <p className={styles["Error-message"]}>{validationErrors.companyName}</p>}
                    <button type="submit" className={styles["Auth-button"]}>
                        Confirm
                    </button>
                    <button
                        className={styles['close-button']}
                        onClick={closeModal} >
                        Close
                    </button>
                </form>
            </div>
        </div>
    );
}

export default UpdateProfileModal;