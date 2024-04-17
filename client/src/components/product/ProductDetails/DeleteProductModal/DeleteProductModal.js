import styles from './DeleteProductModal.module.css';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../../context/AuthContext';
import * as ProductService from '../../../../services/productService'

function DeleteProductModal({ productId, closeModal, refreshProducts, closeProductDetails }) {
    const navigate = useNavigate();
    const { logout } = useAuth();

    const confirmHandler = async () => {
        try {
            await ProductService.Delete(productId);
            closeProductDetails();
            refreshProducts();
            toast.success('Product deleted successfully!');
        } catch (error) {
            handleError(error);
        }
    };

    const handleError = (error) => {
        if (error.response && error.response.status === 401) {
            logout();
            navigate('/login');
            toast.warning('Your session has expired. Please login again.');
        } else {
            toast.error(error.response.data);
        }
        console.error(error);
    }

    return (
        <div>
            <div className={styles["container"]}>
                <h1 className={styles["header"]}>Are you sure?</h1>
                <div className={styles['buttons-container']}>
                    <button className={styles['button-cancel']} onClick={closeModal}>
                        Cancel
                    </button>
                    <button className={styles["button-confirm"]} onClick={confirmHandler}>
                        Confirm
                    </button>
                </div>
            </div>
            <div className={styles['backdrop']} />
        </div>
    );
};

export default DeleteProductModal;