import httpClient from "../httpClient";

const signin = (payload: { username: string; password: string }) => {
    return httpClient.post("/login", payload);
};

export { signin };
