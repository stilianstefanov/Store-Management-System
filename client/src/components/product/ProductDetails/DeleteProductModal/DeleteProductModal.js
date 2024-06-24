import styles from './DeleteProductModal.module.css';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../../context/AuthContext';
import * as ProductService from '../../../../services/productService';
import { useTranslation } from 'react-i18next';

function DeleteProductModal({ productId, closeModal, refreshProducts, closeProductDetails }) {
    const navigate = useNavigate();
    const { logout } = useAuth();
    const { t } = useTranslation();

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
            toast.warning(t('common.sessionExp'));
        } else {
            toast.error(error.response.data);
        }
        console.error(error);
    }

    return (
        <div>
            <div className={styles["container"]}>
                <h1 className={styles["header"]}>{t('common.deleteModal.header')}</h1>
                <div className={styles['buttons-container']}>
                    <button className={styles['button-cancel']} onClick={closeModal}>
                        {t('common.deleteModal.cancel')}
                    </button>
                    <button className={styles["button-confirm"]} onClick={confirmHandler}>
                        {t('common.deleteModal.confirm')}
                    </button>
                </div>
            </div>
            <div className={styles['backdrop']} />
        </div>
    );
};

export default DeleteProductModal;