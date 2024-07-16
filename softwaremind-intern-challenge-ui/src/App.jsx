import "./App.css";
import { Routes, Route, BrowserRouter as Router } from "react-router-dom";
import ProtectedRoute from "./components/ProtectedRoute";
import Login from "./components/Login";
import AuthProvider from "./AuthProvider";
import View from "./components/View";
function App() {
    return (
        <div className="App">
            <Router>
                <AuthProvider>
                    <Routes>
                        <Route path="/login" element={<Login />} />
                        <Route element={<ProtectedRoute />}>
                            <Route path="/view" element={<View />} />
                        </Route>
                    </Routes>
                </AuthProvider>
            </Router>
        </div>
    );
}
export default App;