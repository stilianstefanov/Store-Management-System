import axios from "axios"

export const GetAll = async (params) => {
    const baseUrl = new URL('http://acme.com/api/Clients');
    const config = { headers: { Authorization: `Bearer ${sessionStorage.getItem('token')}` } };

    if (params) Object.entries(params).forEach(([key, value]) => {
        if (value) baseUrl.searchParams.append(key, value);
    });

    const response = await axios.get(baseUrl, config);
    return response.data;
};