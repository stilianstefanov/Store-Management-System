import image from '../../images/home.jpg'
import styles from './Home.module.css'
import DashBoard from '../../components/dashboard/Dashboard';
import { useAuth } from '../../context/AuthContext';

function HomePage() {
    const { isLoggedIn } = useAuth();

    return (
        <div>
            {isLoggedIn ? (
                <DashBoard />
            ) : (
                <div>
                    <img className={styles.image} src={image} alt='homeimage' />
                    <h1 className={styles.inscription}>Manage your store easy and secure!</h1>
                </div>
            )
            }
        </div>
    );
}

export default HomePage;