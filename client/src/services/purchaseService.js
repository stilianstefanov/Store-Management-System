import axios from "axios";

const baseUrl = 'http://acme.com/api/Purchases';

export const CreatePurchase = async (products, clientId) => {
    const config = { headers: { Authorization: `Bearer ${sessionStorage.getItem('token')}` }};

   
};