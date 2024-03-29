import axios from "axios"

const baseUrl = 'http://acme.com/api/Products';

export const GetByBarcode = async (barcode) => {
    const config = { headers: { Authorization: `Bearer ${sessionStorage.getItem('token')}` }};

    const response = await axios.get(`${baseUrl}/barcode/${barcode}`, config);
    return response.data;
};

export const UpdateStocks = async (products) => {
    const config = { headers: { Authorization: `Bearer ${sessionStorage.getItem('token')}` }};

    const response = await axios.patch(`${baseUrl}/decrease-stocks`, products, config);
    return response.data;
};