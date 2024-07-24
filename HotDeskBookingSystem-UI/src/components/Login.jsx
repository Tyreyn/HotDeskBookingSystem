import { useState } from "react";
import { useAuth } from "../AuthProvider";

const Login = () => {
    const [input, setInput] = useState({
        email: "",
        password: "",
    });

    const auth = useAuth();

    const handlers = {
        submit1: handlesubmit_task1,
        submit2: handlesubmit_task2,
    }

    const submitHandler = (e) => {
        const { id } = e.nativeEvent.submitter; // <-- access submitter id
        handlers[id](e); // <--proxy event to proper callback handler
    };

    function handlesubmit_task1(e){
        e.preventDefault();
        if (input.email !== "" && input.password !== "") {
            auth.loginAction(input);
            return;
        }
        alert("Please provide a valid input");
    };

    function handlesubmit_task2(e){
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
        <form onSubmit={submitHandler}>
            <div className="form_control">
                <label htmlFor="user-email">Email:</label>
                <input
                    type="email"
                    id="user-email"
                    name="email"
                    placeholder="example@yahoo.com"
                    aria-describedby="user-email"
                    aria-invalid="false"
                    onChange={handleInput}
                />
                <div id="user-email" className="sr-only">
                    Please enter a valid email, test@test.com
                </div>
            </div>
            <div className="form_control">
                <label htmlFor="password">Password:</label>
                <input
                    type="password"
                    id="password"
                    name="password"
                    aria-describedby="user-password"
                    aria-invalid="false"
                    onChange={handleInput}
                />
                <div id="user-password" className="sr-only">
                    your password should be more than 6 character
                </div>
            </div>
            <button className="btn-submit" id="submit1" type="submit">Log in</button>
            <button className="btn-submit" id="submit2" type="submit">Create Account</button>
        </form>
    );
};

export default Login;