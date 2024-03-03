import {Route, Routes} from 'react-router-dom';

import HomePage from './pages/Home';
import ProductsPage from './pages/Products';
import WarehousePage from './pages/Warehouses';
import BorrowersPage from './pages/Borrowers';


function App() {
  return (
    <div>
      <Routes>
        <Route path='/' element={<HomePage />} exact />
        <Route path='/products' element={<ProductsPage />} />
        <Route path='/warehouses' element={<WarehousePage />} />
        <Route path='/borrowers' element={<BorrowersPage />} />
      </Routes>
    </div>
  );
}

export default App;
