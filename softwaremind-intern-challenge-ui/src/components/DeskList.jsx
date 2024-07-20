import React, { useState } from 'react';
import { Container, TableCell, TableRow, Button, TextField, FormGroup, InputLabel } from '../../node_modules/@mui/material/index';
const DeskList = (auth) => {
    const [newDeskCode, setNewDeskCode] = useState('');
    const [deskToDelete, setDeskToDelete] = useState('');

    const handleAddDesk = async () => {
        event.preventDefault();
        let requestParam = '?locationId=1';
        if (newDeskCode) {
            requestParam = `?locationId=${newDeskCode}`;
        }
        const headerAuth = auth.auth.auth.replaceAll('"', '');
        const response = await fetch(`https://localhost:7147/Admin/AddNewDesk${requestParam}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Authorization: headerAuth,
            },
        });
        if (response.status === 200) {
            alert("New desk added correctly");
            setNewDeskCode("");
        }
        else {
            alert("Something went wrong");
        }
    };

    const handleDeleteDesk = async () => {
        event.preventDefault();
        const headerAuth = auth.auth.auth.replaceAll('"', '');
        const response = await fetch(`https://localhost:7147/Admin/DeleteDesk?deskId=${deskToDelete}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Authorization: headerAuth,
            },
        });
        const res = await response.json();
        if (response.status === 200) {
            setDeskToDelete("");
        }
        alert(res.message);
    };

    return (
        <TableCell className="desk-list">
            <h2>Desks</h2>
            <Container className="desk-list">
                <FormGroup className="add-desk" class="col-md-6 col-md-offset-3 text-center">
                    <InputLabel>Add new desk</InputLabel>
                    <form onSubmit={handleAddDesk}>
                        <TableRow>
                            <TableCell>
                                <TextField
                                    type="text"
                                    value={newDeskCode}
                                    onChange={(e) => setNewDeskCode(e.target.value)}
                                    label="(Optional) Location for new desk"
                                />
                            </TableCell>
                            <TableCell>
                                <Button variant="outlined" color="secondary" type="submit">Add</Button>
                            </TableCell>
                        </TableRow>
                    </form>
                </FormGroup>
            </Container>

            <Container className="desk-list">
                <FormGroup className="delete-desk" class="col-md-6 col-md-offset-3 text-center">
                    <InputLabel>Delete desk</InputLabel>
                    <form onSubmit={handleDeleteDesk}>
                        <TableRow>
                            <TableCell>
                                <TextField
                                    type="text"
                                    value={deskToDelete}
                                    onChange={(e) => setDeskToDelete(e.target.value)}
                                    label="Id of desk to be deleted."
                                />
                            </TableCell>
                            <TableCell>
                                <Button variant="outlined" color="secondary" type="submit">Delete</Button>
                            </TableCell>
                        </TableRow>
                    </form>
                </FormGroup>
            </Container>
        </TableCell >
    );
};

export default DeskList;