import { Link } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import styles from './MainNavigation.module.css';
import { useState } from 'react';
import AccountDetails from '../account/accountDetails/AccountDetails';
import { useTranslation } from 'react-i18next';
import i18n from '../../i18n';

function MainNavigation() {
    const [accountModalIsOpen, setAccountModalIsOpen] = useState(false);
    const { isLoggedIn } = useAuth();
    const { t } = useTranslation();

    return (
        <div>
            <header className={styles.header}>
                <div className={styles.logo}>
                    <Link to='/'>STORE<span>management</span></Link>
                    <span
                        className={`fi fi-bg ${styles['bg-flag']}`}
                        onClick={() => i18n.changeLanguage('bg')}></span>
                    <span className={`fi fi-gb ${styles['gb-flag']}`}
                        onClick={() => i18n.changeLanguage('en')}></span>
                </div>
                <nav>
                    {isLoggedIn ? (
                        <ul className={styles['header-ul']}>
                            <li>
                                <Link to='/products'>{t('nav.logged.products')}</Link>
                            </li>
                            <li>
                                <Link to='/warehouses'>{t('nav.logged.warehouses')}</Link>
                            </li>
                            <li>
                                <Link to='/delayedpayments'>{t('nav.logged.dPayments')}</Link>
                            </li>
                            <li>
                                <Link to='/gmv'>{t('nav.logged.gmv')}</Link>
                            </li>
                            <li>
                                <button className={styles['acc-button']} onClick={() => setAccountModalIsOpen(true)}>{t('nav.logged.account')}</button>
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