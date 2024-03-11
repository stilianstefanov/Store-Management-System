import { useState } from "react";
import * as UserService from '../../../services/userService';
import { toast } from 'react-toastify';
import { useNavigate } from "react-router-dom";
import styles from './Register.module.css';

function RegisterPage() {
    const [email, setEmail] = useState("");
    const [companyName, setCompanyName] = useState("");
    const [userName, setUserName] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [validationErrors, setValidationErrors] = useState({});
    const navigate = useNavigate();

    const registerHandler = async (event) => {
        event.preventDefault();

        if (email && companyName && userName && password && confirmPassword) {
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
                if (error.response && error.response.data.errors) {
                    setValidationErrors(error.response.data.errors);
                    
                } else {
                    toast.error(error.response.data);
                }
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
                 {validationErrors.Email && <p className={styles["Error-message"]}>{validationErrors.Email.join(", ")}</p>}
                 <input
                    placeholder="Company name"
                    className={styles["Auth-input"]}
                    value={companyName}
                    onChange={(e) => setCompanyName(e.target.value)}
                />
                  {validationErrors.CompanyName && <p className={styles["Error-message"]}>{validationErrors.CompanyName.join(", ")}</p>}
                  <input
                    placeholder="Username"
                    className={styles["Auth-input"]}
                    value={userName}
                    onChange={(e) => setUserName(e.target.value)}
                />
                 {validationErrors.UserName && <p className={styles["Error-message"]}>{validationErrors.UserName.join(", ")}</p>}
                <input
                    type="password"
                    placeholder="Password"
                    className={styles["Auth-input"]}
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                />
                {validationErrors.Password && <p className={styles["Error-message"]}>{validationErrors.Password.join(", ")}</p>}
                 <input
                    type="password"
                    placeholder="Confirm password"
                    className={styles["Auth-input"]}
                    value={confirmPassword}
                    onChange={(e) => setConfirmPassword(e.target.value)}
                />
                 {validationErrors.ConfirmPassword && <p className={styles["Error-message"]}>{validationErrors.ConfirmPassword.join(", ")}</p>}
                <button type="submit" className={styles["Auth-button"]}>
                    Confirm
                </button>
            </form>
        </div>
    </div>
    );
}

export default RegisterPage;