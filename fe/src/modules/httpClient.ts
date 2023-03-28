import axios from "axios";

const accessToken = localStorage.getItem("accessToken");

const httpClient = axios.create({
    baseURL: `${import.meta.env.VITE_SERVICE}/api`,
    headers: {
        Authorization: accessToken
            ? `Bearer ${JSON.parse(accessToken)}`
            : "Unset",
    },
});

httpClient.interceptors.response.use(
    (response) => {
        return new Promise((resolve) => {
            setTimeout(() => {
                return resolve(response);
            }, 1000);
        });
    },
    (error) => {
        console.log("Error::", error);
        if (error.response.status === 401) {
            window.alert("Login session has expired!");
            localStorage.removeItem("accessToken");
            localStorage.removeItem("user");
            window.location.href = "/login";
        }
    }
);

export { httpClient };
