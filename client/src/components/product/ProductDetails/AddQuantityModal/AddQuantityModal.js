import { useState } from 'react';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../../context/AuthContext';
import styles from './AddQuantityModal.module.css';
import { productValidationRules, commonValidationRules } from '../../../../validationRules';
import * as ProductService from '../../../../services/productService';
import { useTranslation } from 'react-i18next';

function AddQuantityModal({ product, updateProduct, refreshProducts, closeModal }) {
    const [quantityToAdd, setQuantityToAdd] = useState(0);
    const [validationError, setValidationError] = useState("");
    const navigate = useNavigate();
    const { logout } = useAuth();
    const { t } = useTranslation();

    const submitHandler = async (event) => {
        event.preventDefault();
        if (quantityToAdd === 0 || validationError) return;

        try {
            const updatedQuantity = Number(product.quantity) + Number(quantityToAdd);
            const request = {
                quantity: updatedQuantity
            };
            const response = await ProductService.PartialUpdate(product.id, request);
            updateProduct(response);
            refreshProducts();
            closeModal();
            toast.success('Quantity added successfully!');
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

    const inputQuantityHandler = (event) => {
        const input = event.target.value;
        setQuantityToAdd(input);
        validateInput(input);
    };

    const validateInput = (input) => {
        const minValue = productValidationRules.quantity.minValue;
        const maxValue = productValidationRules.quantity.maxValue;

        if (!input) {
            setValidationError(commonValidationRules.required('Quantity').message);
        } else if (input < minValue || input > maxValue) {
            setValidationError(commonValidationRules.range('Quantity', minValue, maxValue).message);
        } else {
            setValidationError("");
        }
    };

    return (
        <div>
            <div className={styles['main-container']}>
                <h3 className={styles['header']}>{t('addQtyModal.header')}</h3>
                <form onSubmit={submitHandler}>
                    <input
                        type='number'
                        value={quantityToAdd}
                        placeholder='Enter quantity to add'
                        className={`form-control ${styles.input}`}
                        onChange={inputQuantityHandler}
                    />
                    {validationError && <p className={styles["error-message"]}>{validationError}</p>}
                    <div className={styles['buttons-container']}>
                        <button className={styles['button-cancel']} onClick={closeModal}>
                            {t('addQtyModal.cancel')}
                        </button>
                        <button type="submit" className={styles["button-confirm"]}>
                            {t('addQtyModal.confirm')}
                        </button>
                    </div>
                </form>
            </div>
            <div className={styles['backdrop']} />
        </div>
    );
}

export default AddQuantityModal;