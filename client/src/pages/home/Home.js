import image from '../../images/home.jpg'
import styles from './Home.module.css'
import DashBoard from '../../components/dashboard/Dashboard';
import { useAuth } from '../../context/AuthContext';
import { useTranslation } from 'react-i18next';

function HomePage() {
    const { isLoggedIn } = useAuth();
    const { t } = useTranslation();

    return (
        <div>
            {isLoggedIn ? (
                <DashBoard />
            ) : (
                <div>
                    <img className={styles.image} src={image} alt='homeimage' />
                    <h1 className={styles.inscription}>{t('homeHeader')}</h1>
                </div>
            )
            }
        </div>
    );
}

export default HomePage;