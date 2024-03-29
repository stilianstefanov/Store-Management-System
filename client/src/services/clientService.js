import axios from "axios"

const baseUrl = 'http://acme.com/api/Clients';

export const GetAll = async (params) => {
    const url = new URL('http://acme.com/api/Clients');
    const config = { headers: { Authorization: `Bearer ${sessionStorage.getItem('token')}` } };

    if (params) Object.entries(params).forEach(([key, value]) => {
        if (value) url.searchParams.append(key, value);
    });

    const response = await axios.get(url, config);
    return response.data;
};

export const PartialUpdate = async (clientId, request) => {
    const config = { headers: { Authorization: `Bearer ${sessionStorage.getItem('token')}` } };

    const response = await axios.patch(`${baseUrl}/${clientId}`, request, config);
    return response.data;
};