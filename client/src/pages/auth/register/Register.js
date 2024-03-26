import { useState } from "react";
import * as UserService from '../../../services/userService';
import { toast } from 'react-toastify';
import { useNavigate } from "react-router-dom";
import styles from './Register.module.css';
import { commonValidationRules, registerValidationRules } from "../../../validationRules";

function RegisterPage() {
    const [email, setEmail] = useState("");
    const [companyName, setCompanyName] = useState("");
    const [userName, setUserName] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [validationErrors, setValidationErrors] = useState({});
    const navigate = useNavigate();

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

        if (!password) {
            errors.password = commonValidationRules.required('Password').message;
        } else if (!registerValidationRules.password.pattern.test(password)) {
            errors.password = registerValidationRules.password.message;
        }

        if (password !== confirmPassword) {
            errors.confirmPassword = registerValidationRules.confirmPassword.message;
        }

        setValidationErrors(errors);
        return Object.keys(errors).length === 0;
    };

    const registerHandler = async (event) => {
        event.preventDefault();

        if (validate()) {
            const registerRequest = {
                email,
                companyName,
                userName,
                password,
                confirmPassword
            };

            try {
                const data = await UserService.Register(registerRequest);
                toast.success(data);
                navigate('/login');
            } catch (error) {
                toast.error(error.response.data)
                console.error(error);
            }
        }
    };

    return (
        <div className={styles["Auth-container"]}>
            <div className={styles["Auth-card"]}>
                <h1 className={styles["Auth-header"]}>Register</h1>
                <form onSubmit={registerHandler}>
                    <input
                        placeholder="Email"
                        className={styles["Auth-input"]}
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                    {validationErrors.email && <p className={styles["Error-message"]}>{validationErrors.email}</p>}
                    <input
                        placeholder="Company name"
                        className={styles["Auth-input"]}
                        value={companyName}
                        onChange={(e) => setCompanyName(e.target.value)}
                    />
                    {validationErrors.companyName && <p className={styles["Error-message"]}>{validationErrors.companyName}</p>}
                    <input
                        placeholder="Username"
                        className={styles["Auth-input"]}
                        value={userName}
                        onChange={(e) => setUserName(e.target.value)}
                    />
                    {validationErrors.userName && <p className={styles["Error-message"]}>{validationErrors.userName}</p>}
                    <input
                        type="password"
                        placeholder="Password"
                        className={styles["Auth-input"]}
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />
                    {validationErrors.password && <p className={styles["Error-message"]}>{validationErrors.password}</p>}
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
                </form>
            </div>
        </div>
    );
}

export default RegisterPage;