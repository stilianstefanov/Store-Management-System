import { Route, Routes } from 'react-router-dom';
import { useAuth } from './context/AuthContext';
import { ToastContainer } from 'react-toastify';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'react-toastify/dist/ReactToastify.css';

import Layout from './components/layout/Layout';
import HomePage from './pages/home/Home';
import ProductsPage from './pages/products/Products';
import WarehousePage from './pages/warehouses/Warehouses';
import LoginPage from './pages/auth/login/Login';
import RegisterPage from './pages/auth/register/Register';
import DelayedPaymentsPage from './pages/delayedPayments/DelayedPayments';



function App() {
  const { isLoggedIn } = useAuth();

  return (
    <Layout>
      <ToastContainer position="top-right" autoClose={10000} hideProgressBar={false} newestOnTop={false} closeOnClick rtl={false} pauseOnFocusLoss draggable pauseOnHover style={{ width: '600px' }}/>
      <Routes>
        <Route path='/' element={<HomePage />} exact />
        {isLoggedIn ? (
          <>
            <Route path='/products' element={<ProductsPage />} />
            <Route path='/warehouses' element={<WarehousePage />} />
            <Route path='/delayedpayments' element={<DelayedPaymentsPage />} />
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
