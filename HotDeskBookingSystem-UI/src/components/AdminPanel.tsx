import { Container, TableRow } from "@mui/material";
import React from "react";

const AdminPanel = (auth: string) => {

    return (
        <Container className="admin-panel">
            <h1>{auth.auth}</h1>
            <TableRow>
            </TableRow>
            <TableRow>
            </TableRow >
        </Container>
    );
}

export default AdminPanel;