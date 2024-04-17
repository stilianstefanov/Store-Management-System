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
    const config = { headers: { Authorization: `Bearer ${sessionStorage.getItem('token')}` } };

    const response = await axios.get(`${baseUrl}/barcode/${barcode}`, config);
    return response.data;
};

export const GetById = async (id) => {
    const config = { headers: { Authorization: `Bearer ${sessionStorage.getItem('token')}` } };

    const response = await axios.get(`${baseUrl}/${id}`, config);
    return response.data;
};

export const Create = async (request) => {
    const config = { headers: { Authorization: `Bearer ${sessionStorage.getItem('token')}` } };

    const response = await axios.post(baseUrl, request, config);
    return response.data;
};

export const Update = async (productId, request) => {
    const config = { headers: { Authorization: `Bearer ${sessionStorage.getItem('token')}` } };

    const response = await axios.put(`${baseUrl}/${productId}`, request, config);
    return response.data;
};

export const PartialUpdate = async (productId, request) => {
    const config = { headers: { Authorization: `Bearer ${sessionStorage.getItem('token')}` } };

    const response = await axios.patch(`${baseUrl}/${productId}`, request, config);
    return response.data;
};

export const Delete = async (productId) => {
    const config = { headers: { Authorization: `Bearer ${sessionStorage.getItem('token')}` } };

    const response = await axios.delete(`${baseUrl}/${productId}`, config);
    return response.data;
};

export const UpdateStocks = async (products) => {
    const config = { headers: { Authorization: `Bearer ${sessionStorage.getItem('token')}` } };

    const response = await axios.patch(`${baseUrl}/decrease-stocks`, products, config);
    return response.data;
};