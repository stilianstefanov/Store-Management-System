import { Route, Routes } from 'react-router-dom';
import { useEffect } from 'react';

import Layout from './components/layout/Layout';
import HomePage from './pages/home/Home';
import ProductsPage from './pages/products/Products';
import WarehousePage from './pages/warehouses/Warehouses';
import BorrowersPage from './pages/borrowers/Borrowers';
import LoginPage from './pages/auth/login/Login';
import RegisterPage from './pages/auth/register/Register';


function App() {

  useEffect(() => {
    const token = sessionStorage.getItem('token');
    if (token) {
      const decodedToken = JSON.parse(atob(token.split('.')[1]));
      if (decodedToken.exp * 1000 < new Date().getTime()) sessionStorage.removeItem('token');
    }
  }, []);

  return (
    <Layout>
      {sessionStorage.getItem('token') ? (
      <Routes>
        <Route path='/' element={<HomePage />} exact />
        <Route path='/products' element={<ProductsPage />} />
        <Route path='/warehouses' element={<WarehousePage />} />
        <Route path='/borrowers' element={<BorrowersPage />} />
      </Routes>
      ) : (
        <Routes>
          <Route path='/' element={<HomePage />} exact />
          <Route path='/login' element={<LoginPage />}></Route>
          <Route path='/register' element={<RegisterPage />}></Route>
        </Routes>
      )}
    </Layout>
  );
}

export default App;
