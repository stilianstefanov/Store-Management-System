import axios from "axios";

const baseUrl = 'http://acme.com/api/clients';

export const CreatePurchase = async (products, clientId) => {
    const config = { headers: { Authorization: `Bearer ${sessionStorage.getItem('token')}` }};

    const response = await axios.post(`${baseUrl}/${clientId}/Purchases`, products, config);
    return response.data;
};