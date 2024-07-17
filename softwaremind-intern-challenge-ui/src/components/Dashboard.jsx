import AdminPanel from "./AdminPanel";
import DeskDisplay from "./DeskDisplay";
import EmployeePanel from "./EmployeePanel";
import { useNavigate } from "react-router-dom";

const AdminDashboard = ({ auth }) => (
    <div>
        <table>
            <tbody>
                <tr>
                    <td><AdminPanel auth={auth}></AdminPanel></td>
                    <td>
                        <tr>
                            <tr><EmployeePanel auth={auth}></EmployeePanel></tr>
                            <tr><DeskDisplay auth={auth}></DeskDisplay></tr>
                        </tr>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
);

const UserDashboard = ({ auth }) => (
    <div>
        <table>
            <tbody>
                <tr>
                    <td><EmployeePanel auth={auth}></EmployeePanel></td>
                    <td><DeskDisplay auth={auth}></DeskDisplay></td>
                </tr>
            </tbody>
        </table>
    </div>
);


const Dashboard = () => {
    const role = localStorage.getItem("role");
    const auth = localStorage.getItem("auth");
    const navigate = useNavigate();

    const logoutAuth = () => {
        localStorage.removeItem("auth");
        localStorage.removeItem("role");
        navigate("/login");
    };

    const renderDashboard = () => {
        if (role === 'admin') {
            return <AdminDashboard auth={auth} />;
        } else {
            return <UserDashboard auth={auth} />;
        }
    };

    return (
        <div>
            <button onClick={logoutAuth}>Logout</button>
            {renderDashboard()}
        </div>
    );
}

export default Dashboard;