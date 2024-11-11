import { useState } from "react";
import { useAuth } from "../Security/AuthProvider";
import Button from '@mui/material/Button';
import Box from '@mui/material/Box/Box';
import { FormControl, InputLabel, Input, Grid2, styled, AppBar, Toolbar, IconButton } from "@mui/material";

const Login = () => {
    const [input, setInput] = useState({
        email: "",
        password: "",
    });

    const isDisabled = input.email === "" || input.password === "";

    const auth = useAuth();

    const handlers = {
        submit1: handlesubmit_task1,
        submit2: handlesubmit_task2,
    }

    const submitHandler = (e) => {
        const { id } = e.nativeEvent.submitter; // <-- access submitter id
        handlers[id](e); // <--proxy event to proper callback handler
    };

    function handlesubmit_task1(e) {
        e.preventDefault();
        if (input.email !== "" && input.password !== "") {
            auth.loginAction(input);
            return;
        }
        alert("Please provide a valid input");
    };

    function handlesubmit_task2(e) {
        e.preventDefault();
        if (input.email !== "" && input.password !== "") {
            auth.createAccountAction(input);
            return;
        }
        alert("Please provide a valid input");
    };

    const handleInput = (e) => {
        const { name, value } = e.target;
        setInput((prev) => ({
            ...prev,
            [name]: value,
        }));
    };

    return (
            <Box sx={{
            width: "80vw",
            marginLeft: "auto",
            marginRight: "auto",
            textAlign: "center",
            boxShadow: "10",
            borderRadius: 1,
            backgroundColor: 'rgba(204, 200, 198, 60%)',
        }}>
            <AppBar position="static" sx={{ backgroundColor: 'rgba(204, 200, 198, 0%)', opacity: "0" } }>
            </AppBar>
                <form onSubmit={submitHandler}>
                    <Grid2 container spacing={3} justifyContent="center" margin="2%">
                        <Grid2 size={12}>
                            <FormControl sx={{
                                width: "0.8"
                            }}>
                                <InputLabel sx={{
                                    '&.MuiInputLabel-shrink': {
                                        color: '#5E738C'
                                    }
                                }}
                                    htmlFor="user-email">Email:</InputLabel>
                                <Input
                                    sx={{
                                        color: "#33455A",
                                        ':after': { borderBottomColor: '#5E738C' },
                                    }}
                                    name="email"
                                    placeholder="example@gmail.com"
                                    aria-placeholder="example@gmail.com"
                                    onChange={handleInput} />
                            </FormControl>
                        </Grid2>
                        <Grid2 size={12}>
                            <FormControl sx={{ width: "0.8" }}>
                                <InputLabel sx={{
                                    '&.MuiInputLabel-shrink': {
                                        color: '#5E738C'
                                    }
                                }} htmlFor="user-password">Password:</InputLabel>
                                <Input
                                    sx={{
                                        color: "#33455A",
                                        ':after': { borderBottomColor: '#5E738C' },
                                }}
                                    type="password"
                                    name="password"
                                    onChange={handleInput} />
                            </FormControl>
                        </Grid2>
                        <Grid2 size={6}>
                            <Button type="submit" disabled={isDisabled} id="submit1" variant="contained" sx={{
                                backgroundColor: 'rgba(204, 200, 198, 80%)', width: "0.9", color: "#33455A",
                            }}>
                                Log in
                            </Button>
                        </Grid2>
                        <Grid2 size={6}>
                            <Button type="submit" disabled={isDisabled} id="submit2" variant="contained" sx={{
                                backgroundColor: 'rgba(204, 200, 198, 80%)', width: "0.9", color: "#33455A",
                            }}>
                                Create Account
                            </Button>
                        </Grid2>
                    </Grid2>
                </form>
            </Box>
    );
};

export default Login;