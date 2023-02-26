import axios from "axios";

const httpClient = axios.create({
    baseURL: `${import.meta.env.VITE_SERVICE}/api`,
});

httpClient.interceptors.response.use((response) => {
    return new Promise((resolve) => {
        setTimeout(() => {
            return resolve(response);
        }, 1000);
    });
});

const setAuthorizationHeader = (token: string) => {
    httpClient.defaults.headers.common["Authorization"] = "Bearer " + token;
};

export default httpClient;
export { setAuthorizationHeader };
