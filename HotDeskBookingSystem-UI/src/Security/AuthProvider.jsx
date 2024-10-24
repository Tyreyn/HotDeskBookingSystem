import { useContext, createContext, useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

const AuthContext = createContext();

const AuthProvider = ({ children }) => {
    const [auth, setAuth] = useState(localStorage.getItem("auth") || "");
    const [role, setRole] = useState(localStorage.getItem("role") || "");
    const navigate = useNavigate();

    useEffect(() => {
        const authString = localStorage.getItem('auth');
        const roleString = localStorage.getItem('role');
        if (authString && roleString && authString != 'undefined')
        {
            setAuth(authString);
            setRole(roleString);
            navigate("/dashboard");
        }
        else
        {
            setAuth("");
            setRole("");
            localStorage.removeItem("auth");
            localStorage.removeItem("role");
            navigate("/login");
        }
    }, []);

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
            if (res.message.includes('Basic'))
            {
                setAuth(res.message);
                const dummyRequest = await fetch('https://localhost:7147/Admin/CheckIfAdmin', {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                        Authorization: res.message,
                    },
                });

                if (dummyRequest.status === 403)
                {
                    setRole("user");
                    localStorage.setItem("role", "user");
                }
                else
                {
                    setRole("admin");
                    localStorage.setItem("role", "admin");
                }

                localStorage.setItem("auth", res.message);
                navigate("/dashboard");
                return;
            }
            else
            {
                alert(res.message);
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

    return (
        <AuthContext.Provider value={{ auth, role, createAccountAction, loginAction }}>
            {children}
        </AuthContext.Provider>
    );

};

export default AuthProvider;

export const useAuth = () => {
    return useContext(AuthContext);
};