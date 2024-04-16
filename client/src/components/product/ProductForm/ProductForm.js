import styles from './ProductForm.module.css'
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../context/AuthContext';
import { productValidationRules, commonValidationRules } from '../../../validationRules';
import * as ProductService from '../../../services/productService'
import WarehouseSelectTable from '../../warehouse/warehouseSelectTable/WarehouseSelectTable';

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

    const selectWarehouseHandler = (id) => {
        setSelectedWarehouseId(id);
    }

    return (
        <div>
            <div className={styles["container"]}>
                <h1 className={styles["header"]}>{`${isUpdate ? "Update Product" : "Add New Product"}`}</h1>
                <form>
                    <div className={styles['input-group']}>
                        <label htmlFor="barcode-input">Barcode:</label>
                        <input
                            id="barcode-input"
                            placeholder="Enter barcode"
                            className={styles["input"]}
                            value={barcode}
                        // onChange={inputBarcodeHandler}
                        />
                        {validationErrors.barcode && <p className={styles["error-message"]}>{validationErrors.barcode}</p>}
                    </div>
                    <div className={styles['input-group']}>
                        <label htmlFor="name-input">Name:</label>
                        <input
                            id="name-input"
                            placeholder="Enter name"
                            className={styles["input"]}
                            value={name}
                        // onChange={inputNameHandler}
                        />
                        {validationErrors.name && <p className={styles["error-message"]}>{validationErrors.name}</p>}
                    </div>
                    <div className={styles['input-group']}>
                        <label htmlFor="description-input">Description:</label>
                        <input
                            id="description-input"
                            placeholder="Enter description (optional)"
                            className={styles["input"]}
                            value={description}
                        // onChange={inputDescriptionHandler}
                        />
                        {validationErrors.description && <p className={styles["error-message"]}>{validationErrors.description}</p>}
                    </div>
                    <div className={styles['input-group']}>
                        <label htmlFor="delivery-price-input">Delivery price:</label>
                        <input
                            id="delivery-price-input"
                            placeholder="Enter delivery price"
                            className={styles["input"]}
                            value={deliveryPrice}
                        // onChange={inputDeliveryPriceHandler}
                        />
                        {validationErrors.deliveryPrice && <p className={styles["error-message"]}>{validationErrors.deliveryPrice}</p>}
                    </div>
                    <div className={styles['input-group']}>
                        <label htmlFor="price-input">Price:</label>
                        <input
                            id="price-input"
                            placeholder="Enter price"
                            className={styles["input"]}
                            value={price}
                        // onChange={inputPriceHandler}
                        />
                        {validationErrors.price && <p className={styles["error-message"]}>{validationErrors.price}</p>}
                    </div>
                    <div className={styles['input-group']}>
                        <label htmlFor="quantity-input">Quantity:</label>
                        <input
                            id="quantity-input"
                            placeholder="Enter quantity"
                            className={styles["input"]}
                            value={quantity}
                        // onChange={inputQuantityHandler}
                        />
                        {validationErrors.quantity && <p className={styles["error-message"]}>{validationErrors.quantity}</p>}
                    </div>
                    <div className={styles['input-group']}>
                        <label htmlFor="quantity-min-input">Min Quantity:</label>
                        <input
                            id="quantity-min-input"
                            placeholder="Enter minimum quantity"
                            className={styles["input"]}
                            value={minQuantity}
                        // onChange={inputMinQuantityHandler}
                        />
                        {validationErrors.minQuantity && <p className={styles["error-message"]}>{validationErrors.minQuantity}</p>}
                    </div>
                    <div className={styles['input-group']}>
                        <label htmlFor="quantity-max-input">Max Quantity:</label>
                        <input
                            id="quantity-max-input"
                            placeholder="Enter maximum quantity"
                            className={styles["input"]}
                            value={maxQuantity}
                        // onChange={inputMaxQuantityHandler}
                        />
                        {validationErrors.maxQuantity && <p className={styles["error-message"]}>{validationErrors.maxQuantity}</p>}
                    </div>
                    {!isUpdate && <WarehouseSelectTable
                        selectedWarehouseId={selectedWarehouseId}
                        selectWarehouseHandler={selectWarehouseHandler}
                    />}
                    <div className={styles['buttons-container']}>
                        <button className={styles['button-cancel']} onClick={props.closeForm}>
                            Cancel
                        </button>
                        <button type="submit" className={styles["button-confirm"]}>
                            {`${isUpdate ? "Update" : "Add"}`}
                        </button>
                    </div>
                </form>
            </div>
            <div className={styles['backdrop']} />
        </div>
    );
};

export default ProductForm;
