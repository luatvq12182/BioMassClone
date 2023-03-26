import axios from "axios";

const accessToken = localStorage.getItem("accessToken");

const httpClient = axios.create({
    baseURL: `${import.meta.env.VITE_SERVICE}/api`,
    headers: {
        Authorization: accessToken ? `Bearer ${JSON.parse(accessToken)}` : "Unset",
    },
});

export { httpClient };
