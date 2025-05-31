import axios from 'axios';

const API_BASE_URL = import.meta.env.VITE_AUTH_SERVICE;
console.log('API_BASE_URL:', API_BASE_URL);
const API_TIMEOUT = 5000; // 5 seconds
const axiosInstance = axios.create({
  baseURL: API_BASE_URL,
  timeout: API_TIMEOUT,
});

export async function RegisterAccount({ hoTen, email, matKhau }) {
  try {
    const response = await axiosInstance.post('/Auth/Register', { hoTen, email, matKhau });
    return response.data;
  } catch (error) {
    return error.response;
  }
}

export async function LoginAccount({ email, matKhau }) {
  try {
    const response = await axiosInstance.post('/Auth/Login', { email, matKhau });
    return response.data;
  } catch (error) {
    return error.response;
  }
}