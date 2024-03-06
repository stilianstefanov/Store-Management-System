import { Route, Routes } from 'react-router-dom';
import { useAuth } from './context/AuthContext';

import Layout from './components/layout/Layout';
import HomePage from './pages/home/Home';
import ProductsPage from './pages/products/Products';
import WarehousePage from './pages/warehouses/Warehouses';
import BorrowersPage from './pages/borrowers/Borrowers';
import LoginPage from './pages/auth/login/Login';
import RegisterPage from './pages/auth/register/Register';


function App() {
  const { isLoggedIn } = useAuth();

  return (
    <Layout>
      <Routes>
        <Route path='/' element={<HomePage />} exact />
        {isLoggedIn ? (
          <>
            <Route path='/products' element={<ProductsPage />} />
            <Route path='/warehouses' element={<WarehousePage />} />
            <Route path='/borrowers' element={<BorrowersPage />} />
          </>
        ) : (
          <>
            <Route path='/login' element={<LoginPage />}></Route>
            <Route path='/register' element={<RegisterPage />}></Route>
          </>
        )}
      </Routes>
    </Layout>
  );
}

export default App;
