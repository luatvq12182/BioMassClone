import axios from "axios";

const httpClient = axios.create({
    baseURL: `${import.meta.env.VITE_SERVICE}/api`,
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
        if (
            error.response.status === 401 &&
            error.response.data === "jwt expired"
        ) {
            window.alert("Login session has expired!");
            localStorage.removeItem("accessToken");
            localStorage.removeItem("user");
            window.location.href = "/sign-in";
        }
    }
);

const setAuthorizationHeader = (token: string) => {
    httpClient.defaults.headers.common["Authorization"] = "Bearer " + token;
};

export default httpClient;
export { setAuthorizationHeader };
