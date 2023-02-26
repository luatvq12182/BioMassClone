import { ChangeEvent, useState } from "react";
import { Navigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import { Box, Checkbox, FormControlLabel, FormGroup } from "@mui/material";

import useAuth from "@/hooks/useAuth";
import useLoading from "@/hooks/useLoading";
import * as UserService from "@/services/user";
import { Button, Card, Input } from "@/components/common";

type FormProps = {
    username: string;
    password: string;
};

const Login = () => {
    const user = useAuth();

    const [isShowPassword, setIsShowPassword] = useState<boolean>(false);
    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<FormProps>();
    const { showLoading, hideLoading } = useLoading();

    const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
        setIsShowPassword(e.target.checked);
    };

    const submitHandler = async (data: FormProps) => {
        showLoading();

        try {
            const res = await UserService.signin(data);

            console.log(res);
        } catch (error) {
            console.log("ERROR:: ", error);
        }

        setTimeout(() => {
            hideLoading();
        }, 1500);
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
                        error={!!errors["username"]}
                        label='Username'
                        sx={{ mb: 2 }}
                        {...register("username", {
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

export default Login;
