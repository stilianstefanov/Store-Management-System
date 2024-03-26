import { useState } from "react";
import { useAuth } from "../../../context/AuthContext";
import { useNavigate } from "react-router-dom";
import * as UserService from "../../../services/userService";
import styles from "./Login.module.css";
import { toast } from "react-toastify";

function LoginPage() {
    const [userNameOrEmail, setuserNameOrEmail] = useState("");
    const [password, setPassword] = useState("");
    const [loginError, setLoginError] = useState("");
    const { login } = useAuth();
    const navigate = useNavigate();

    const loginHadler = async (event) => {
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
                handleError(error);
            }
        }
    }

    const handleError = (error) => {
        if (error.response && error.response.status === 400) {
            setLoginError("Invalid username or password!");
            toast.error("Invalid Credentials!");
        } else {
            toast.error("An unexpected error occurred. Please try again later.");
        }
        console.error(error);
    }

    return (
        <div className={styles["Auth-container"]}>
            <div className={styles["Auth-card"]}>
                <h1 className={styles["Auth-header"]}>Login</h1>
                {loginError && <p className={styles["Error-message"]}>{loginError}</p>}
                <form onSubmit={loginHadler}>
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
                    <button type="submit" className={styles["Auth-button"]}>
                        Login
                    </button>
                </form>
            </div>
        </div>
    );
}

export default LoginPage;