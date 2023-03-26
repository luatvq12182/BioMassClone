import { httpClient } from "../httpClient";
import { ILoginModel, ILoginResponseModel } from "./models";

const login = (payload: ILoginModel) => {
    return httpClient.post<ILoginResponseModel>("/auth/login", payload);
};

export { login };
