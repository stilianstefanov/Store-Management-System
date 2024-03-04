import { Link } from 'react-router-dom';
import classes from './MainNavigation.module.css';

function MainNavigation() {
    return (
    <header className={classes.header}>
        <div className={classes.logo}>
            <Link to='/'>Store Management</Link>
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
            </ul>
        </nav>
    </header>
    );
}

export default MainNavigation;