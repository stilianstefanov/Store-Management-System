import axios from "axios"

const baseUrl = 'http://acme.com/api/Warehouses';

export const GetAll = async (params) => {
    const url = new URL('http://acme.com/api/Warehouses');
    const config = { headers: { Authorization: `Bearer ${sessionStorage.getItem('token')}` } };

    if (params) Object.entries(params).forEach(([key, value]) => {
        if (value) url.searchParams.append(key, value);
    });

    const response = await axios.get(url, config);
    return response.data;
};