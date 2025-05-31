import axios from "axios";

const API_BASE_URL = import.meta.env.VITE_MOVIES_SERVICE;
console.log('API_BASE_URL:', API_BASE_URL);
const API_TIMEOUT = 5000; // 5 seconds
const axiosInstance = axios.create({ baseURL: API_BASE_URL, timeout: API_TIMEOUT, });

export async function GetMovieList() {
  try {
    const response = await axiosInstance.get('/Phim/danh-sach-phim');
    return response.data;
  } catch (error) {
    return error.response;
  }
}

export async function GetLoaiGhe() {
  try {
    const response = await axiosInstance.get(`/Phim/loai-ghe`);
    return response.data;
  } catch (error) {
    return error.response;
  }
}

export async function OrderTicket(data) {
  try {
    const response = await axiosInstance.post(`/Phim/dat-ve`, data, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem('token')}`
      }
    });
    return response.data;
  } catch (error) {
    return error.response;
  }
}

export async function GetOrderedSeat(maCaChieu) {
  try {
    const response = await axiosInstance.get(`/Phim/ca-chieu`, {
      headers: { Authorization: `Bearer ${localStorage.getItem('token')}` },
      params: { id: maCaChieu }
    });
    return response.data;
  } catch (error) {
    return error.response;
  }
}