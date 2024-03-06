import { useState } from "react";
import { useNavigate } from "react-router-dom";
import * as UserService from '../../../services/UserService'
import styles from "./Login.module.css";

function LoginPage() {
    const navigate = useNavigate();
    const [emailOrUserName, setEmailOrUserName] = useState("");
    const [password, setPassword] = useState("");

    async function handleLogin(event) {
        event.preventDefault();

        if (emailOrUserName && password) {
            const loginRequest = {
                emailOrUserName,
                password
            };

            const data = await UserService.Login(loginRequest);

            sessionStorage.setItem('token', data);
            navigate('/');
            window.location.reload();
        }
    }

    return (
        <div className={styles["Auth-container"]}>
            <div className={styles["Auth-card"]}>
                <h1 className={styles["Auth-header"]}>Login</h1>
                <form>
                    <input
                        placeholder="Email or Username"
                        className={styles["Auth-input"]}
                        value={emailOrUserName}
                        onChange={(e) => setEmailOrUserName(e.target.value)}
                    />
                    <input
                        type="password"
                        placeholder="Password"
                        className={styles["Auth-input"]}
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />
                    <button type="button" className={styles["Auth-button"]} onClick={handleLogin}>
                        Login
                    </button>
                </form>
            </div>
        </div>
    );
}

export default LoginPage;