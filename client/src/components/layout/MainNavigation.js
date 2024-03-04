import { Link } from 'react-router-dom';
import classes from './MainNavigation.module.css';

function MainNavigation() {

    function logoutHandler() {
        //ToDo 
    }

    return (
    <header className={classes.header}>
        <div className={classes.logo}>
            <Link to='/'>STORE<span>management</span></Link>
        </div>
        <nav>
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
        </nav>
    </header>
    );
}

export default MainNavigation;