import styles from './DeletePurchasedProductModal.module.css';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../context/AuthContext';
import * as PurchasedProductService from '../../../services/purchasedProductService';
import { useTranslation } from 'react-i18next';

function DeletePurchasedProductModal({ clientId, purchaseId, purchasedProductId, closeModal, refreshClients }) {
    const navigate = useNavigate();
    const { logout } = useAuth();
    const { t } = useTranslation();

    const confirmHandler = async () => {
        try {
            await PurchasedProductService.Delete(clientId, purchaseId, purchasedProductId);
            closeModal();
            refreshClients();
            toast.success('Purchased product deleted successfully!');
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
    };

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
}

export default DeletePurchasedProductModal;