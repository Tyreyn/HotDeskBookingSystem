import AdminPanel from "./AdminPanel";
import DeskDisplay from "./DeskDisplay";
import EmployeePanel from "./EmployeePanel";
import { useNavigate } from "react-router-dom";
import { Container, Table, TableContainer, TableCell, TableRow, Paper, TableBody } from "../../node_modules/@mui/material/index";

const AdminDashboard = ({ auth }) => (
    <TableContainer component={Paper}>
        <Table>
            <TableBody>
                <TableRow>
                    <TableCell ><AdminPanel auth={auth}></AdminPanel></TableCell>
                    <TableCell ><EmployeePanel auth={auth}></EmployeePanel></TableCell>
                </TableRow>
                <TableRow><DeskDisplay auth={auth} style={{ maxWidth: "100%" }}></DeskDisplay></TableRow>
            </TableBody>
        </Table>
    </TableContainer>
);

const UserDashboard = ({ auth }) => (
    <Container>
        <Table>
            <TableBody>
                <TableRow>
                    <TableRow><EmployeePanel auth={auth}></EmployeePanel></TableRow>
                    <TableRow><DeskDisplay auth={auth}></DeskDisplay></TableRow>
                </TableRow>
            </TableBody>
        </Table>
    </Container>
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
        <Container maxWidth="xl">
            <button onClick={logoutAuth}>Logout</button>
            {renderDashboard()}
        </Container>
    );
}

export default Dashboard;