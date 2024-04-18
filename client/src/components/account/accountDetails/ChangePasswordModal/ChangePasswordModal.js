import styles from './ChangePasswordModal.module.css';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../../context/AuthContext';
import { toast } from 'react-toastify';
import { commonValidationRules, registerValidationRules } from "../../../../validationRules";
import * as UserService from '../../../../services/userService';

function ChangePasswordModal({ closeModal }) {
    const [currentPassword, setCurrentPassword] = useState("");
    const [newPassword, setNewPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [validationErrors, setValidationErrors] = useState({});
    const { logout } = useAuth();
    const navigate = useNavigate();

    const submitHandler = async (event) => {
        event.preventDefault();

        if (validate()) {
            const request = {
                currentPassword,
                newPassword,
                confirmPassword
            };
            try {
                await UserService.ChangePassword(request);
                closeModal();
                toast.success("Password updated successfully");
            } catch (error) {
                handleError(error);
            }
        }
    };

    const validate = () => {
        const errors = {};

        if (!newPassword) {
            errors.newPassword = commonValidationRules.required('New Password').message;
        } else if (!registerValidationRules.password.pattern.test(newPassword)) {
            errors.newPassword = registerValidationRules.password.message;
        }

        if (newPassword !== confirmPassword) {
            errors.confirmPassword = registerValidationRules.confirmPassword.message;
        }

        setValidationErrors(errors);
        return Object.keys(errors).length === 0;
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
        <div className={styles["Auth-container"]}>
            <div className={styles["Auth-card"]}>
                <h1 className={styles["Auth-header"]}>Change Password</h1>
                <form onSubmit={submitHandler}>
                    <input
                        type="password"
                        placeholder="Current password"
                        className={styles["Auth-input"]}
                        value={currentPassword}
                        onChange={(e) => setCurrentPassword(e.target.value)}
                    />
                    {validationErrors.currentPassword && <p className={styles["Error-message"]}>{validationErrors.currentPassword}</p>}
                    <input
                        type="password"
                        placeholder="New password"
                        className={styles["Auth-input"]}
                        value={newPassword}
                        onChange={(e) => setNewPassword(e.target.value)}
                    />
                    {validationErrors.newPassword && <p className={styles["Error-message"]}>{validationErrors.newPassword}</p>}
                    <input
                        type="password"
                        placeholder="Confirm password"
                        className={styles["Auth-input"]}
                        value={confirmPassword}
                        onChange={(e) => setConfirmPassword(e.target.value)}
                    />
                    {validationErrors.confirmPassword && <p className={styles["Error-message"]}>{validationErrors.confirmPassword}</p>}
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
};

export default ChangePasswordModal;