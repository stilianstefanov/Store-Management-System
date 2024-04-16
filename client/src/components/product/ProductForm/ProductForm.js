import styles from './ProductForm.module.css'
import { useState, useEffect, useCallback } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../context/AuthContext';
import { productValidationRules, commonValidationRules } from '../../../validationRules';
import * as ProductService from '../../../services/productService'

function ProductForm(props) {
    const isUpdate = props.product ? true : false;
    const [name, setName] = useState(`${props.product ? props.product.name : ""}`);
    const [description, setDescription] = useState(`${props.product ? props.product.description : ""}`);
    const [barcode, setBarcode] = useState(`${props.product ? props.product.barcode : ""}`);
    const [deliveryPrice, setDeliveryPrice] = useState(`${props.product ? props.product.deliveryPrice : ""}`);
    const [price, setPrice] = useState(`${props.product ? props.product.price : ""}`);
    const [quantity, setQuantity] = useState(`${props.product ? props.product.quantity : ""}`);
    const [minQuantity, setMinQuantity] = useState(`${props.product ? props.product.minQuantity : ""}`);
    const [maxQuantity, setMaxQuantity] = useState(`${props.product ? props.product.maxQuantity : ""}`);
    const [selectedWarehouseId, setSelectedWarehouseId] = useState("");
    const [validationErrors, setValidationErrors] = useState({});
    const { logout } = useAuth();
    const navigate = useNavigate();

    const handleError = useCallback((error) => {
        if (error.response && error.response.status === 401) {
            logout();
            navigate('/login');
            toast.warning('Your session has expired. Please login again.');
        } else {
            toast.error(error.response ? error.response.data : "An error occurred");
        }
        console.error(error);
    }, [logout, navigate]);
};

export default ProductForm;
