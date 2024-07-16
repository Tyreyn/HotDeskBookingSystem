import React from "react";
import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../AuthProvider";

const ProtectedRoute = () => {
    const auth = useAuth();
    if (auth.screen == "auth") return <Navigate to="/login" />;
    return <Outlet />;
};

export default ProtectedRoute;