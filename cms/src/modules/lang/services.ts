import { httpClient } from "../httpClient";
import { ILang } from "./models";

const getLangs = () => {
    return httpClient.get<ILang[]>("/languages");
};

export {
    getLangs
}