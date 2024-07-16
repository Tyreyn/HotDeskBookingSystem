import { useContext, createContext, useState } from "react";
import { useNavigate } from "react-router-dom";

const AuthContext = createContext();

const AuthProvider = ({ children }) => {
    const [token, setToken] = useState(localStorage.getItem("site") || "");
    const navigate = useNavigate();
    const loginAction = async (data) => {
        try {
            const response = await fetch(`https://localhost:7147/AnonymousUser/Login?email=${data.email}&password=${data.password}`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(data),
            });
            const res = await response.json();
            if (res.message.includes('Basic')) {
                setToken(res.message.split(" ")[1]);
                localStorage.setItem("site", res.token);
                navigate("/dashboard");
                return;
            } else
            {
                alert("Please provide a valid input");
            }
            throw new Error(res.message);
        } catch (err) {
            console.error(err);
        }
    };

    const createAccountAction = async (data) => {
        try {
            const response = await fetch(`https://localhost:7147/AnonymousUser/CreateAccount?email=${data.email}&password=${data.password}`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(data),
            });
            const res = await response.json();
            if (res.success === true) {
                alert(res.message);
                return;
            } else {
                alert(res.message);
            }
            throw new Error(res.message);
        } catch (err) {
            console.error(err);
        }
    };

    const logOut = () => {
        setToken("");
        localStorage.removeItem("site");
        navigate("/login");
    };

    return (
        <AuthContext.Provider value={{ token, createAccountAction, loginAction, logOut }}>
            {children}
        </AuthContext.Provider>
    );

};

export default AuthProvider;

export const useAuth = () => {
    return useContext(AuthContext);
};