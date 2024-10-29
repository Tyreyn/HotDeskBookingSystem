import "./App.css"
import { Route, BrowserRouter as Router, Routes } from 'react-router-dom';
import ProtectedRoute from './Routes/ProtectedRoute';
import AuthProvider from './Security/AuthProvider';
import { Container } from '@mui/material';
import Dashboard from "./components/Dashboard";
import Login from "./components/Login";

function App() {
    return (
        <Container className="App" sx={{
            width: "100vw",
            maxWidth: "100%",
            marginLeft: "auto",
            marginRight: "auto",
            display: "flex",
            alignContent: "center",
            alignItems: "baseline",
            justifyContent: "center"
        }}>
            <div className="bg"></div>
            <div className="bg bg2"></div>
            <div className="bg bg3"></div>
            <div className="bg bg4"></div>
            <Routes>
                <Route path="/login" element={<Login />} />
                <Route element={<ProtectedRoute />}>
                    <Route path="/dashboard" element={<Dashboard />} />
                </Route>
                <Route path="*" element={<Login />} />
            </Routes>
        </Container>
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