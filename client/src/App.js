import {Route, Routes} from 'react-router-dom';

import Layout from './components/layout/Layout';
import HomePage from './pages/home/Home';
import ProductsPage from './pages/products/Products';
import WarehousePage from './pages/warehouses/Warehouses';
import BorrowersPage from './pages/borrowers/Borrowers';


function App() {
  return (
    <Layout>
      <Routes>
        <Route path='/' element={<HomePage />} exact />
        <Route path='/products' element={<ProductsPage />} />
        <Route path='/warehouses' element={<WarehousePage />} />
        <Route path='/borrowers' element={<BorrowersPage />} />
      </Routes>
    </Layout>
  );
}

export default App;
