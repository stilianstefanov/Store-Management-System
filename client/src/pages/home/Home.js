import image from '../../images/home.jpg'
import styles from './Home.module.css'

function HomePage() {
    return (
    <div>
        <img className={styles.image} src={image} alt='homeimage'/>
        <h1 className={styles.inscription}>Manage your store easy and secure!</h1>
    </div>
    );
}

export default HomePage;