import { Link } from 'react-router-dom';
import { useState } from 'react';
import { useEffect } from 'react';
import classes from './MainNavigation.module.css';

function MainNavigation() {
    const [isActive, setIsActive] = useState(true);

    useEffect(() => {
        const token = sessionStorage.getItem('token');
        if (!token) {
            setIsActive(false);
        }
    }, []);

    function logoutHandler() {
        sessionStorage.removeItem('token');
        setIsActive(false);
    }

    return (
        <header className={classes.header}>
            <div className={classes.logo}>
                <Link to='/'>STORE<span>management</span></Link>
            </div>
            <nav>
                {isActive ? (
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