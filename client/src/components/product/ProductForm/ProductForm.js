import styles from './ProductForm.module.css'
import { useState } from 'react';
import { toast } from 'react-toastify'
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../context/AuthContext';
import { productValidationRules, commonValidationRules } from '../../../validationRules';
import * as ProductService from '../../../services/productService'
import WarehouseSelectTable from '../../warehouse/warehouseSelectTable/WarehouseSelectTable';
import { useTranslation } from 'react-i18next';

function ProductForm(props) {
    const isUpdate = props.product ? true : false;
    const [name, setName] = useState(`${isUpdate ? props.product.name : ""}`);
    const [description, setDescription] = useState(`${isUpdate ? props.product.description : ""}`);
    const [barcode, setBarcode] = useState(`${isUpdate ? props.product.barcode : ""}`);
    const [deliveryPrice, setDeliveryPrice] = useState(`${isUpdate ? props.product.deliveryPrice : ""}`);
    const [price, setPrice] = useState(`${isUpdate ? props.product.price : ""}`);
    const [quantity, setQuantity] = useState(`${isUpdate ? props.product.quantity : ""}`);
    const [minQuantity, setMinQuantity] = useState(`${isUpdate ? props.product.minQuantity : ""}`);
    const [maxQuantity, setMaxQuantity] = useState(`${isUpdate ? props.product.maxQuantity : ""}`);
    const [selectedWarehouseId, setSelectedWarehouseId] = useState("");
    const [validationErrors, setValidationErrors] = useState({});
    const { logout } = useAuth();
    const navigate = useNavigate();
    const { t } = useTranslation();

    const submitHandler = async (event) => {
        event.preventDefault();

        if (!isUpdate) {
            if (!validateWarehouseId()) return;
        }

        if (Object.entries(validationErrors).length === 0) {
            try {
                const request = {
                    name,
                    description,
                    barcode,
                    deliveryPrice,
                    price,
                    quantity,
                    minQuantity,
                    maxQuantity,
                    warehouseId: selectedWarehouseId
                };
                isUpdate ? props.updateProduct(await ProductService.Update(props.product.id, request))
                    : await ProductService.Create(request);
                props.closeForm();
                props.refreshProducts();
                toast.success(`${isUpdate ? "Product updated successfully!" : "Product added successfully!"}`);
            } catch (error) {
                handleError(error);
            }
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

    const selectWarehouseHandler = (id) => {
        setSelectedWarehouseId(id);
    };

    const validateWarehouseId = () => {
        if (!selectedWarehouseId) {
            const errors = { ...validationErrors };
            errors.warehouse = commonValidationRules.required('Warehouse').message;
            setValidationErrors(errors);
            return false;
        }
        return true;
    };

    const inputBarcodeHandler = (event) => {
        const input = event.target.value;
        setBarcode(input);
        validateBarcodeInput(input);
    };

    const inputNameHandler = (event) => {
        const input = event.target.value;
        setName(input);
        validateNameInput(input);
    };

    const inputDescriptionHandler = (event) => {
        const input = event.target.value;
        setDescription(input);
        validateDescriptionInput(input);
    };

    const inputPriceHandler = (event) => {
        const input = event.target.value;
        setPrice(input);
        validatePriceInput(input);
    };

    const inputDeliveryPriceHandler = (event) => {
        const input = event.target.value;
        setDeliveryPrice(input);
        validateDeliveryPriceInput(input);
    };

    const inputQuantityHandler = (event) => {
        const input = event.target.value;
        setQuantity(input);
        validateQuantityInput(input);
    };

    const inputMinQuantityHandler = (event) => {
        const input = event.target.value;
        setMinQuantity(input);
        validateMinQuantityInput(input);
    };

    const inputMaxQuantityHandler = (event) => {
        const input = event.target.value;
        setMaxQuantity(input);
        validateMaxQuantityInput(input);
    };

    const validateBarcodeInput = (input) => {
        const errors = { ...validationErrors };
        const minLength = productValidationRules.barcode.minLength;
        const maxLength = productValidationRules.barcode.maxLength;
        if (!input) {
            errors.barcode = commonValidationRules.required('Barcode').message;
            setValidationErrors(errors);
        } else if (input.length < minLength || input.length > maxLength) {
            errors.barcode = commonValidationRules.length('Barcode', minLength, maxLength).message;
            setValidationErrors(errors);
        } else {
            delete errors.barcode;
            setValidationErrors(errors);
        }
    }

    const validateNameInput = (input) => {
        const errors = { ...validationErrors };
        const minLength = productValidationRules.name.minLength;
        const maxLength = productValidationRules.name.maxLength;
        if (!input) {
            errors.name = commonValidationRules.required('Name').message;
            setValidationErrors(errors);
        } else if (input.length < minLength || input.length > maxLength) {
            errors.name = commonValidationRules.length('Name', minLength, maxLength).message;
            setValidationErrors(errors);
        } else {
            delete errors.name;
            setValidationErrors(errors);
        }
    }

    const validateDescriptionInput = (input) => {
        const errors = { ...validationErrors };
        const minLength = productValidationRules.description.minLength;
        const maxLength = productValidationRules.description.maxLength;
        if (input.length > maxLength) {
            errors.description = commonValidationRules.length('Description', minLength, maxLength).message;
            setValidationErrors(errors);
        } else {
            delete errors.description;
            setValidationErrors(errors);
        }
    }

    const validatePriceInput = (input) => {
        const errors = { ...validationErrors };
        const minValue = productValidationRules.price.minValue;
        const maxValue = productValidationRules.price.maxValue;
        if (!input) {
            errors.price = commonValidationRules.required('Price').message;
            setValidationErrors(errors);
        } else if (input < minValue || input > maxValue) {
            errors.price = commonValidationRules.range('Price', minValue, maxValue).message;
            setValidationErrors(errors);
        } else {
            delete errors.price;
            setValidationErrors(errors);
        }
    }

    const validateDeliveryPriceInput = (input) => {
        const errors = { ...validationErrors };
        const minValue = productValidationRules.price.minValue;
        const maxValue = productValidationRules.price.maxValue;
        if (!input) {
            errors.deliveryPrice = commonValidationRules.required('Delivery Price').message;
            setValidationErrors(errors);
        } else if (input < minValue || input > maxValue) {
            errors.deliveryPrice = commonValidationRules.range('Delivery Price', minValue, maxValue).message;
            setValidationErrors(errors);
        } else {
            delete errors.deliveryPrice;
            setValidationErrors(errors);
        }
    }

    const validateQuantityInput = (input) => {
        const errors = { ...validationErrors };
        const minValue = productValidationRules.quantity.minValue;
        const maxValue = productValidationRules.quantity.maxValue;
        if (!input) {
            errors.quantity = commonValidationRules.required('Quantity').message;
            setValidationErrors(errors);
        } else if (input < minValue || input > maxValue) {
            errors.quantity = commonValidationRules.range('Quantity', minValue, maxValue).message;
            setValidationErrors(errors);
        } else {
            delete errors.quantity;
            setValidationErrors(errors);
        }
    }

    const validateMinQuantityInput = (input) => {
        const errors = { ...validationErrors };
        const minValue = productValidationRules.quantity.minValue;
        const maxValue = productValidationRules.quantity.maxValue;
        if (!input) {
            errors.minQuantity = commonValidationRules.required('Minimum Quantity').message;
            setValidationErrors(errors);
        } else if (input < minValue || input > maxValue) {
            errors.minQuantity = commonValidationRules.range('Minimum Quantity', minValue, maxValue).message;
            setValidationErrors(errors);
        } else {
            delete errors.minQuantity;
            setValidationErrors(errors);
        }
    }

    const validateMaxQuantityInput = (input) => {
        const errors = { ...validationErrors };
        const minValue = productValidationRules.quantity.minValue;
        const maxValue = productValidationRules.quantity.maxValue;
        if (!input) {
            errors.maxQuantity = commonValidationRules.required('Maximum Quantity').message;
            setValidationErrors(errors);
        } else if (input < minValue || input > maxValue) {
            errors.maxQuantity = commonValidationRules.range('Maximum Quantity', minValue, maxValue).message;
            setValidationErrors(errors);
        } else {
            delete errors.maxQuantity;
            setValidationErrors(errors);
        }
    }

    return (
        <div>
            <div className={isUpdate ? styles["update-container"] : styles["add-container"]}>
                <h1 className={styles["header"]}>{`${isUpdate ? t('productForm.headerUpdate') : t('productForm.headerAdd')}`}</h1>
                <form onSubmit={submitHandler}>
                    <div className={styles['wrapper']}>
                        <div className={styles['left-section']}>
                            <div className={styles['input-group']}>
                                <label htmlFor="barcode-input">{t('productForm.barcodeLabel')}</label>
                                <input
                                    id="barcode-input"
                                    placeholder={t('productForm.barcodeInput')}
                                    className={styles["input"]}
                                    value={barcode}
                                    onChange={inputBarcodeHandler}
                                />
                                {validationErrors.barcode && <p className={styles["error-message"]}>{validationErrors.barcode}</p>}
                            </div>
                            <div className={styles['input-group']}>
                                <label htmlFor="name-input">{t('productForm.nameLabel')}</label>
                                <input
                                    id="name-input"
                                    placeholder={t('productForm.nameInput')}
                                    className={styles["input"]}
                                    value={name}
                                    onChange={inputNameHandler}
                                />
                                {validationErrors.name && <p className={styles["error-message"]}>{validationErrors.name}</p>}
                            </div>
                            <div className={styles['input-group']}>
                                <label htmlFor="description-input">{t('productForm.descrLabel')}</label>
                                <input
                                    id="description-input"
                                    placeholder={t('productForm.descrInput')}
                                    className={styles["input"]}
                                    value={description}
                                    onChange={inputDescriptionHandler}
                                />
                                {validationErrors.description && <p className={styles["error-message"]}>{validationErrors.description}</p>}
                            </div>
                            <div className={styles['input-group']}>
                                <label htmlFor="price-input">{t('productForm.priceLabel')}</label>
                                <input
                                    id="price-input"
                                    type='number'
                                    placeholder={t('productForm.priceInput')}
                                    className={styles["input"]}
                                    value={price}
                                    onChange={inputPriceHandler}
                                />
                                {validationErrors.price && <p className={styles["error-message"]}>{validationErrors.price}</p>}
                            </div>
                        </div>
                        <div className={styles['middle-section']}>
                            <div className={styles['input-group']}>
                                <label htmlFor="delivery-price-input">{t('productForm.delPriceLabel')}</label>
                                <input
                                    id="delivery-price-input"
                                    type='number'
                                    placeholder={t('productForm.delPriceInput')}
                                    className={styles["input"]}
                                    value={deliveryPrice}
                                    onChange={inputDeliveryPriceHandler}
                                />
                                {validationErrors.deliveryPrice && <p className={styles["error-message"]}>{validationErrors.deliveryPrice}</p>}
                            </div>
                            <div className={styles['input-group']}>
                                <label htmlFor="quantity-input">{t('productForm.quantityLabel')}</label>
                                <input
                                    id="quantity-input"
                                    type='number'
                                    placeholder={t('productForm.quantityInput')}
                                    className={styles["input"]}
                                    value={quantity}
                                    onChange={inputQuantityHandler}
                                />
                                {validationErrors.quantity && <p className={styles["error-message"]}>{validationErrors.quantity}</p>}
                            </div>
                            <div className={styles['input-group']}>
                                <label htmlFor="quantity-min-input">{t('productForm.minQuantityLabel')}</label>
                                <input
                                    id="quantity-min-input"
                                    type='number'
                                    placeholder={t('productForm.minQuantityInput')}
                                    className={styles["input"]}
                                    value={minQuantity}
                                    onChange={inputMinQuantityHandler}
                                />
                                {validationErrors.minQuantity && <p className={styles["error-message"]}>{validationErrors.minQuantity}</p>}
                            </div>
                            <div className={styles['input-group']}>
                                <label htmlFor="quantity-max-input">{t('productForm.maxQuantityLabel')}</label>
                                <input
                                    id="quantity-max-input"
                                    type='number'
                                    placeholder={t('productForm.maxQuantityInput')}
                                    className={styles["input"]}
                                    value={maxQuantity}
                                    onChange={inputMaxQuantityHandler}
                                />
                                {validationErrors.maxQuantity && <p className={styles["error-message"]}>{validationErrors.maxQuantity}</p>}
                            </div>
                        </div>
                        <div className={styles['right-section']}>
                            {!isUpdate && <WarehouseSelectTable
                                selectedWarehouseId={selectedWarehouseId}
                                selectWarehouseHandler={selectWarehouseHandler}
                            />}
                            {validationErrors.warehouse && <p className={styles["error-message"]}>{validationErrors.warehouse}</p>}
                        </div>
                    </div>
                    <div className={styles['buttons-container']}>
                        <button className={styles['button-cancel']} onClick={props.closeForm}>
                            {t('productForm.cancel')}
                        </button>
                        <button type="submit" className={styles["button-confirm"]}>
                            {`${isUpdate ? t('productForm.update') : t('productForm.add')}`}
                        </button>
                    </div>
                </form>
            </div>
            <div className={styles['backdrop']} />
        </div>
    );
};

export default ProductForm;
