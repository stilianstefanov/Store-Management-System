import { useState } from "react";
import { useNavigate } from "react-router-dom";
import styles from "./Login.module.css"

function LoginPage() {
    const navigate = useNavigate();
    const [emailOrUserName, setEmailOrUserName] = useState("");
    const [password, setPassword] = useState("");

    function handleLogin() {

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