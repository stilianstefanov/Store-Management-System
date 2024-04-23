import styles from './UpdateProfileModal.module.css';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../../context/AuthContext';
import { useState } from 'react';

function UpdateProfileModal({ closeModal }) {
    const [userName, setUserName] = useState("");
    const [email, setEmail] = useState("");
    const [companyName, setCompanyName] = useState("");
    const [validationErrors, setValidationErrors] = useState({});
    const { logout } = useAuth();
    const navigate = useNavigate();

    return (
        <div className={styles["Auth-container"]}>
            <div className={styles["Auth-card"]}>
                <h1 className={styles["Auth-header"]}>Update Profile</h1>
                <form>
                    <input
                        placeholder="Email"
                        className={styles["Auth-input"]}
                        value={email}

                    />
                    {validationErrors.email && <p className={styles["Error-message"]}>{validationErrors.email}</p>}
                    <input
                        placeholder="Username"
                        className={styles["Auth-input"]}
                        value={userName}

                    />
                    {validationErrors.userName && <p className={styles["Error-message"]}>{validationErrors.userName}</p>}
                    <input
                        placeholder="Company name"
                        className={styles["Auth-input"]}
                        value={companyName}

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