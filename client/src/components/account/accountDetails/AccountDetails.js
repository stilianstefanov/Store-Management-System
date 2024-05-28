import styles from './AccountDetails.module.css';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../context/AuthContext';
import { useState, useEffect } from 'react';
import { jwtDecode } from "jwt-decode";
import ChangePasswordModal from './ChangePasswordModal/ChangePasswordModal';
import UpdateProfileModal from './UpdadeProfileModal/UpdateProfileModal';
import { useTranslation } from 'react-i18next';

function AccountDetails({ closeModal }) {
    const [claims, setClaims] = useState({ unique_name: "", email: "", companyName: "" });
    const [changePasswordModalIsOpen, setChangePasswordModalIsOpen] = useState(false);
    const [updateProfileModalIsOpen, setUpdateProfileModalIsOpen] = useState(false);
    const navigate = useNavigate();
    const { logout } = useAuth();
    const { t } = useTranslation();

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
                            <p>{t('account.company')} <span className={styles['list-info-span']}>{claims.companyName}</span></p>
                        </li>
                        <li>
                            <p>{t('account.username')} <span className={styles['list-info-span']}>{claims.unique_name}</span></p>
                        </li>
                        <li>
                            <p>{t('account.email')} <span className={styles['list-info-span']}>{claims.email}</span></p>
                        </li>
                    </ul>
                    <div className={styles['buttons-container']}>
                        <button
                            className={styles['update-button']}
                            onClick={() => setUpdateProfileModalIsOpen(true)}>
                            {t('account.update')}
                        </button>
                        <button
                            className={styles['change-password-button']}
                            onClick={() => setChangePasswordModalIsOpen(true)}>
                            {t('account.changepass')}
                        </button>
                        <button
                            className={styles['logout-button']}
                            onClick={logoutHandler}>
                            {t('account.logout')}
                        </button>
                    </div>
                    <button
                        className={styles['close-button']}
                        onClick={closeModal} >
                        {t('account.close')}
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