import styles from './AccountDetails.module.css';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../context/AuthContext';
import { useState, useEffect } from 'react';
import { jwtDecode } from "jwt-decode";
import ChangePasswordModal from './ChangePasswordModal/ChangePasswordModal';
import UpdateProfileModal from './UpdadeProfileModal/UpdateProfileModal';

function AccountDetails({ closeModal }) {
    const [claims, setClaims] = useState({ unique_name: "", email: "", companyName: "" });
    const [changePasswordModalIsOpen, setChangePasswordModalIsOpen] = useState(false);
    const [updateProfileModalIsOpen, setUpdateProfileModalIsOpen] = useState(false);
    const navigate = useNavigate();
    const { logout } = useAuth();

    useEffect(() => {
        const jwtToken = sessionStorage.getItem('token');

        if (jwtToken) {
            try {
                const decodedToken = jwtDecode(jwtToken);
                const { unique_name, email, CompanyName } = decodedToken;
                setClaims({ unique_name, email, companyName: CompanyName });
            } catch (error) {
                console.error('Error decoding JWT token:', error);
            }
        }
    }, []);

    const logoutHandler = () => {
        logout();
        navigate('/');
        closeModal();
    }

    return (
        <div>
            <div className={styles["container"]}>
                <div className={styles['flex-container']}>
                    <ul className={styles['list-info']}>
                        <li>
                            <p>Company: <span className={styles['list-info-span']}>{claims.companyName}</span></p>
                        </li>
                        <li>
                            <p>Username: <span className={styles['list-info-span']}>{claims.unique_name}</span></p>
                        </li>
                        <li>
                            <p>Email: <span className={styles['list-info-span']}>{claims.email}</span></p>
                        </li>
                    </ul>
                    <div className={styles['buttons-container']}>
                        <button
                            className={styles['update-button']}
                            onClick={() => setUpdateProfileModalIsOpen(true)}>
                            Update info
                        </button>
                        <button
                            className={styles['change-password-button']}
                            onClick={() => setChangePasswordModalIsOpen(true)}>
                            Change password
                        </button>
                        <button
                            className={styles['logout-button']}
                            onClick={logoutHandler}>
                            Logout
                        </button>
                    </div>
                    <button
                        className={styles['close-button']}
                        onClick={closeModal} >
                        Close
                    </button>
                </div>
            </div>
            {changePasswordModalIsOpen && <ChangePasswordModal
                closeModal={closeModal} />}
            {updateProfileModalIsOpen && <UpdateProfileModal
                claims={claims}
                closeModal={closeModal} />}
        </div>
    );
};

export default AccountDetails;