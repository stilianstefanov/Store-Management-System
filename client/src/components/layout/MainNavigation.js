import { Link } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import styles from './MainNavigation.module.css';
import { useState } from 'react';
import AccountDetails from '../account/accountDetails/AccountDetails';
import { useTranslation } from 'react-i18next';

function MainNavigation() {
    const [accountModalIsOpen, setAccountModalIsOpen] = useState(false);
    const { isLoggedIn } = useAuth();
    const { t } = useTranslation();

    return (
        <div>
            <header className={styles.header}>
                <div className={styles.logo}>
                    <Link to='/'>STORE<span>management</span></Link>
                </div>
                <nav>
                    {isLoggedIn ? (
                        <ul className={styles['header-ul']}>
                            <li>
                                <Link to='/products'>Products</Link>
                            </li>
                            <li>
                                <Link to='/warehouses'>Warehouses</Link>
                            </li>
                            <li>
                                <Link to='/delayedpayments'>Delayed Payments</Link>
                            </li>
                            <li>
                                <Link to='/gmv'>GMV</Link>
                            </li>
                            <li>
                                <button className={styles['acc-button']} onClick={() => setAccountModalIsOpen(true)}>Account</button>
                            </li>
                        </ul>
                    ) : (
                        <ul className={styles['header-ul']}>
                            <li>
                                <Link to='/login'>{t('nav.notLogged.login')}</Link>
                            </li>
                            <li>
                                <Link to='/register'>{t('nav.notLogged.register')}</Link>
                            </li>

                        </ul>
                    )
                    }
                </nav>
            </header>
            {accountModalIsOpen && <AccountDetails
                closeModal={() => setAccountModalIsOpen(false)} />}
        </div>
    );
}

export default MainNavigation;