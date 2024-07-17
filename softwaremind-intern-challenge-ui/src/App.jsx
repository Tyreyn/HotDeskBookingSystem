import "./App.css";
import { Routes, Route, BrowserRouter as Router } from "react-router-dom";
import ProtectedRoute from "./components/ProtectedRoute";
import Login from "./components/Login";
import AuthProvider from "./AuthProvider";
import Dashboard from "./components/Dashboard";
const App = () => {

    return (
        <div className="App">
            <header>
                <h1>Hot Desk Booking System</h1>
            </header>
            <Routes>
                <Route path="/login" element={<Login />} />
                <Route element={<ProtectedRoute />}>
                    <Route path="/dashboard" element={<Dashboard />} />
                </Route>
                <Route path="*" element={<Login />} />
            </Routes>
        </div>
    );
}

const AppWithRouter = () => (
    <Router>
        <AuthProvider>
            <App />
        </AuthProvider>
    </Router>
);

export default AppWithRouter;