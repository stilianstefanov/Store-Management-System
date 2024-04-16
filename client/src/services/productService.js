import axios from "axios"

const baseUrl = 'http://acme.com/api/Products';

export const GetAll = async (params) => {
    const url = new URL('http://acme.com/api/Products');
    const config = { headers: { Authorization: `Bearer ${sessionStorage.getItem('token')}` } };

    if (params) Object.entries(params).forEach(([key, value]) => {
        if (value) url.searchParams.append(key, value);
    });

    const response = await axios.get(url, config);
    return response.data;
};

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