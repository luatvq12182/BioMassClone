export interface ILoginModel {
    identify: string;
    password: string;
}

export interface ILoginResponseModel {
    email: string;
    token: string;
    username: string;
}