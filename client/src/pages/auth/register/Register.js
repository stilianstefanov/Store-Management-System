import { useState } from "react";
import * as UserService from '../../../services/userService';
import { toast } from 'react-toastify';
import { useNavigate } from "react-router-dom";

function RegisterPage() {
    const [email, setEmail] = useState("");
    const [companyName, setCompanyName] = useState("");
    const [userName, setUserName] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
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
                toast.error(error);
                console.error(error);
            }
        }
    };

    return (
        <div className={styles["Auth-container"]}>
        <div className={styles["Auth-card"]}>
            <h1 className={styles["Auth-header"]}>Register</h1>
            <form>
                <input
                    placeholder="Email"
                    className={styles["Auth-input"]}
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                />
                 <input
                    placeholder="Company name"
                    className={styles["Auth-input"]}
                    value={companyName}
                    onChange={(e) => setCompanyName(e.target.value)}
                />
                  <input
                    placeholder="Username"
                    className={styles["Auth-input"]}
                    value={userName}
                    onChange={(e) => setUserName(e.target.value)}
                />
                <input
                    type="password"
                    placeholder="Password"
                    className={styles["Auth-input"]}
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                />
                 <input
                    type="password"
                    placeholder="Confirm password"
                    className={styles["Auth-input"]}
                    value={confirmPassword}
                    onChange={(e) => setConfirmPassword(e.target.value)}
                />
                <button type="submit" className={styles["Auth-button"]} onClick={registerHandler}>
                    Login
                </button>
            </form>
        </div>
    </div>
    );
}

export default RegisterPage;