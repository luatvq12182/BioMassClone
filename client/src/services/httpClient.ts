import axios from "axios";

const httpClient = axios.create({
    baseURL: `${import.meta.env.VITE_SERVICE}`,
});

httpClient.interceptors.response.use((response) => {
    return new Promise((resolve) => {
        setTimeout(() => {
            return resolve(response);
        }, 1500);
    });
});

export default httpClient;
