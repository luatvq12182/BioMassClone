import { FormEvent, SyntheticEvent, useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { InputText } from "primereact/inputtext";
import { Button } from "primereact/button";
import { Card } from "primereact/card";
import { Checkbox, CheckboxChangeEvent } from "primereact/checkbox";

import { ILoginModel, login } from "@/modules/auth";
import useLocalStorage from "@/hooks/useLocalStorage";

const Login = () => {
    const navigate = useNavigate();
    const [payload, setPayload] = useState<ILoginModel>({
        identify: "",
        password: "",
    });
    const [isShowPass, setIsShowPass] = useState<boolean>(false);
    const [isVerifying, setIsVerifying] = useState<boolean>(false);
    const [tokenLocal, setTokenLocal] = useLocalStorage("accessToken", "");

    useEffect(() => {
        document.title = "Login";

        if (tokenLocal) {
            navigate("/admin");
        }
    }, []);

    const handleChange =
        (field: keyof ILoginModel) => (e: FormEvent<HTMLInputElement>) => {
            setPayload({
                ...payload,
                [field]: e.currentTarget.value,
            });
        };

    const handleSubmit = async (e: SyntheticEvent) => {
        e.preventDefault();

        if (!payload.identify.trim() || !payload.password.trim()) {
            window.alert("Username or password is required!");
            return;
        }

        setIsVerifying(true);

        try {
            const res = await login(payload);

            setTokenLocal(res.data.token);

            window.location.href = "/admin";
        } catch (error: any) {
            console.log("Error when login:", error);
            if (+error.response.status === 401) {
                window.alert("Incorrect username or password!");
            }
        }

        setIsVerifying(false);
    };

    return (
        <Card
            title='Login'
            className='fixed w-[450px] h-[300px] top-[50%] left-[50%] translate-x-[-50%] translate-y-[-50%]'
        >
            <form onSubmit={handleSubmit}>
                <InputText
                    placeholder='Username'
                    className='w-full mb-4'
                    value={payload.identify}
                    onChange={handleChange("identify")}
                />

                <div className='mt-4'></div>

                <InputText
                    placeholder='Password'
                    className='w-full'
                    type={isShowPass ? "text" : "password"}
                    value={payload.password}
                    onChange={handleChange("password")}
                />

                <div className='mt-2 flex items-center'>
                    <Checkbox
                        inputId='show-password'
                        name='toggle-show-password'
                        value='IsShowPassword'
                        checked={isShowPass}
                        onChange={(e: CheckboxChangeEvent) => {
                            setIsShowPass(!!e.checked);
                        }}
                    />
                    <label
                        htmlFor='show-password'
                        className='ml-2'
                    >
                        Show password
                    </label>
                </div>

                <div className='mt-6'></div>

                <Button
                    loading={isVerifying}
                    label='Submit'
                />
            </form>
        </Card>
    );
};

export default Login;
