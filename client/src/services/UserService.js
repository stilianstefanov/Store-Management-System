import axios from "axios"

const baseUrl = 'http://acme.com/api/Auth';

export const Login = async (loginRequest) => {
    const response = await axios.post(`${baseUrl}/login`, loginRequest);
    return response.data;
 }
