import { ChangeEvent, useState } from "react";
import { Navigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import { Box, Checkbox, FormControlLabel, FormGroup } from "@mui/material";

import useAuth from "@/hooks/useAuth";
import * as UserService from "@/services/user";
import { Button, Card, Input } from "@/components/common";
import { setAuthorizationHeader } from "@/services/httpClient";

type FormProps = {
    identify: string;
    password: string;
};

const SignIn = () => {
    const { user, signIn } = useAuth();
    const [isSubmiting, setIsSubmiting] = useState<boolean>(false);

    const [isShowPassword, setIsShowPassword] = useState<boolean>(false);
    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<FormProps>();

    const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
        setIsShowPassword(e.target.checked);
    };

    const submitHandler = async (data: FormProps) => {
        setIsSubmiting(true);
        try {
            const res = await UserService.signin(data);
            console.log(res.data);

            setAuthorizationHeader(res.data.token);

            signIn(res.data.userName, () => {
                localStorage.setItem("accessToken", res.data.token);
                localStorage.setItem("user", JSON.stringify(res.data.userName));
            });
        } catch (error: any) {
            console.log("ERROR:: ", error);
            window.alert(error.response);
        }
        setIsSubmiting(false);
    };

    if (user) {
        return <Navigate to='/admin/dashboard' />;
    }

    return (
        <Box
            bgcolor='#F1F5F9'
            sx={{
                minHeight: "100vh",
                position: "relative",
            }}
        >
            <Card
                title='Sign in'
                sx={{
                    width: "400px",
                    height: "auto",
                    position: "absolute",
                    top: "50%",
                    left: "50%",
                    transform: "translate(-50%, -50%)",
                    p: 2,
                }}
            >
                <Box
                    component='form'
                    onSubmit={handleSubmit(submitHandler)}
                >
                    <Input
                        error={!!errors["identify"]}
                        label='Email'
                        sx={{ mb: 2 }}
                        {...register("identify", {
                            required: true,
                        })}
                    />
                    <Input
                        error={!!errors["password"]}
                        label='Password'
                        type={isShowPassword ? "text" : "password"}
                        {...register("password", {
                            required: true,
                        })}
                    />

                    <FormGroup sx={{ mb: 3 }}>
                        <FormControlLabel
                            control={
                                <Checkbox
                                    onChange={handleChange}
                                    checked={isShowPassword}
                                />
                            }
                            label='Show password'
                        />
                    </FormGroup>

                    <Button
                        fullWidth
                        loading={isSubmiting}
                        size='large'
                        type='submit'
                    >
                        Sign in
                    </Button>
                </Box>
            </Card>
        </Box>
    );
};

export default SignIn;
