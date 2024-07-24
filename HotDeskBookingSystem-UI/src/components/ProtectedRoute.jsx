import { useEffect, React } from "react";
import { useNavigate, Outlet } from "react-router-dom";
import { useAuth } from "../AuthProvider";

const ProtectedRoute = () => {
    const { auth } = useAuth();
    const navigate = useNavigate();

    useEffect(() => {
        if (!auth) {
            return navigate("/login", { replace: true });
        } else {
            return navigate("/dashboard", { replace: true });
        }
    }, [auth, navigate]);

    return (
        <Outlet />
    );
};

export default ProtectedRoute;