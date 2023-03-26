import axios from "axios";

const accessToken = JSON.parse(localStorage.getItem("accessToken") || "");

const httpClient = axios.create({
    baseURL: `${import.meta.env.VITE_SERVICE}/api`,
    headers: {
        Authorization: accessToken ? `Bearer ${accessToken}` : "Unset",
    },
});

export { httpClient };
