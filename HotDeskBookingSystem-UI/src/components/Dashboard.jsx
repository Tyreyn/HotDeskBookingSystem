import { AppBar, Container, IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableRow, Toolbar, Slide, useScrollTrigger, Typography } from "@mui/material";
import { useNavigate } from "react-router-dom";
import AdminPanel from "./AdminPanel";
import DeskDisplay from "./DeskDisplay";
import EmployeePanel from "./EmployeePanel";
import PropTypes from 'prop-types';
import React from "react";
import CssBaseline from '@mui/material/CssBaseline';

const AdminDashboard = ({ auth }) => (
    <Table sx={{ justifyContent: "center", display: "flex", marginTop: "10vh" }}>
        <TableBody sx={{ display: "flex", justifyContent: "space-around", alignContent: "space-between", flexWrap: "wrap" }}>
            <TableRow sx={{ width: "80vw", display: "block", justifyItems: "center" }}>
                <TableCell sx={{ width:"35vw" }}><AdminPanel auth={auth}></AdminPanel></TableCell>
                <TableCell sx={{ width: "35vw" }}><EmployeePanel auth={auth}></EmployeePanel></TableCell>
            </TableRow >
            <TableRow sx={{ width: "80vw", display: "flex"}}><DeskDisplay auth={auth}></DeskDisplay></TableRow>
        </TableBody>
    </Table>
);

const UserDashboard = ({ auth }) => (
    <Table sx={{ justifyContent: "center", display: "flex", marginTop: "10vh" }}>
        <TableBody sx={{ display: "contents", justifyContent: "space-around", alignContent: "space-between", flexWrap: "wrap", flexDirection: "column" }}>
            <TableRow sx={{ display: "block" }}>
                <TableRow sx={{ width: "80vw"} }><EmployeePanel auth={auth}></EmployeePanel></TableRow>
                <TableRow><DeskDisplay auth={auth}></DeskDisplay></TableRow>
            </TableRow>
        </TableBody>
    </Table>
);

function ElevationScroll(props) {
    const { children, window } = props;
    const trigger = useScrollTrigger({
        disableHysteresis: true,
        threshold: 0,
        target: window ? window() : undefined,
    });

    return React.cloneElement(children, {
        elevation: trigger ? 4 : 0,
    });
}

ElevationScroll.propTypes = {
    children: PropTypes.element.isRequired,
    window: PropTypes.func,
};


const Dashboard = (props) => {
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
            <ElevationScroll {...props}>
                <AppBar position="fixed" sx={{ backgroundColor: 'rgba(204, 200, 198, 100%)', opacity: "1" }}>
                    <Toolbar>
                        <Typography sx={{ flexGrow: "1", color: '#5E738C', fontWeight: "800" }}>Hot desk booking system</Typography>
                        <IconButton onClick={logoutAuth} sx={{ color: '#5E738C' }}>Logout</IconButton>
                    </Toolbar>
                </AppBar>
            </ElevationScroll>
            {renderDashboard()}
        </Container>

    );
}

export default Dashboard;