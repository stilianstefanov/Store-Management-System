import axios from "axios";

const baseUrl = 'http://acme.com/api/clients';

export const GetAll = async (clientId, params) => {
    const url = new URL(`http://acme.com/api/clients/${clientId}/Purchases`);
    const config = { headers: { Authorization: `Bearer ${sessionStorage.getItem('token')}` } };

    if (params) Object.entries(params).forEach(([key, value]) => {
        if (value) url.searchParams.append(key, value);
    });

    const response = await axios.get(url, config);
    return response.data;
};

export const CreatePurchase = async (products, clientId) => {
    const config = { headers: { Authorization: `Bearer ${sessionStorage.getItem('token')}` }};

    const response = await axios.post(`${baseUrl}/${clientId}/Purchases`, products, config);
    return response.data;
};

export const Delete = async (clientId, purchaseId) => {
    const config = { headers: { Authorization: `Bearer ${sessionStorage.getItem('token')}` }};

    const response = await axios.delete(`${baseUrl}/${clientId}/Purchases/${purchaseId}`, config);
    return response.data;
};