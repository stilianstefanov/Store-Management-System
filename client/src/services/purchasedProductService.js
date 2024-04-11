import axios from "axios";

const baseUrl = 'http://acme.com/api/clients';

export const GetAll = async (clientId, purchaseId) => {
    const config = { headers: { Authorization: `Bearer ${sessionStorage.getItem('token')}` } };

    const response = await axios.get(`${baseUrl}/${clientId}/purchases/${purchaseId}/PurchasedProducts`, config);
    return response.data;
};