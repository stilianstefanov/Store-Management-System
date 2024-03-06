import { Link } from 'react-router-dom';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import classes from './MainNavigation.module.css';

function MainNavigation() {
    const navigate = useNavigate();
    const { isLoggedIn, logout } = useAuth();

    function logoutHandler() {
        logout();
        navigate('/');
    }

    return (
        <header className={classes.header}>
            <div className={classes.logo}>
                <Link to='/'>STORE<span>management</span></Link>
            </div>
            <nav>
                {isLoggedIn ? (
                    <ul>
                        <li>
                            <Link to='/products'>Products</Link>
                        </li>
                        <li>
                            <Link to='/warehouses'>Warehouses</Link>
                        </li>
                        <li>
                            <Link to='/borrowers'>Borrowers</Link>
                        </li>
                        <li>
                            <button onClick={logoutHandler}>Logout</button>
                        </li>
                    </ul>
                ) : (
                    <ul>
                        <li>
                            <Link to='/login'>Login</Link>
                        </li>
                        <li>
                            <Link to='/register'>Register</Link>
                        </li>

                    </ul>
                )
                }
            </nav>
        </header>
    );
}

export default MainNavigation;