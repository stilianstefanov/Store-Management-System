import { useState } from "react";
import { useAuth } from "../../../context/AuthContext";
import { useNavigate } from "react-router-dom";
import * as UserService from '../../../services/UserService'
import styles from "./Login.module.css";

function LoginPage() {
    const navigate = useNavigate();
    const { login } = useAuth();
    const [userNameOrEmail, setuserNameOrEmail] = useState("");
    const [password, setPassword] = useState("");
    const [loginError, setLoginError] = useState("");

    async function handleLogin(event) {
        event.preventDefault();

        if (userNameOrEmail && password) {
            const loginRequest = {
                userNameOrEmail,
                password
            };

            try {
                const data = await UserService.Login(loginRequest);
                login(data);
                navigate('/');
            } catch (error) {
                if (error.response && error.response.status === 400) {
                    setLoginError("Incorrect username or password.");
                    return;
                }
                console.log(error);
            }
        }
    }

    return (
        <div className={styles["Auth-container"]}>
            <div className={styles["Auth-card"]}>
                <h1 className={styles["Auth-header"]}>Login</h1>
                {loginError && <p className={styles["Error-message"]}>{loginError}</p>}
                <form>
                    <input
                        placeholder="Email or Username"
                        className={styles["Auth-input"]}
                        value={userNameOrEmail}
                        onChange={(e) => setuserNameOrEmail(e.target.value)}
                    />
                    <input
                        type="password"
                        placeholder="Password"
                        className={styles["Auth-input"]}
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />
                    <button type="submit" className={styles["Auth-button"]} onClick={handleLogin}>
                        Login
                    </button>
                </form>
            </div>
        </div>
    );
}

export default LoginPage;