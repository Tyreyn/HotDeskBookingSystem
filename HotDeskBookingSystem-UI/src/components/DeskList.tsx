import React, { useState } from 'react';
import { Container, TableCell, TableRow, Button, TextField, FormGroup, InputLabel, Paper, Table, Typography } from '@mui/material';
const DeskList = (auth) => {
    const [newDeskCode, setNewDeskCode] = useState('');
    const [newDeskLocation, setNewDeskLocationCode] = useState('');
    const [deskToChange, setDeskToChangeCode] = useState('');
    const [deskToDelete, setDeskToDelete] = useState('');

    const handleAddDesk = async () => {
        event.preventDefault();
        let requestParam = '?locationId=1';
        if (newDeskCode) {
            requestParam = `?locationId=${newDeskCode}`;
        }
        const headerAuth = auth.auth.replaceAll('"', '');
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

    const handleChangeDeskLocation = async () => {
        event.preventDefault();
        const headerAuth = auth.auth.auth.replaceAll('"', '');
        const response = await fetch(`https://localhost:7147/Admin/ChangeDeskLocation?locationId=${newDeskLocation}&deskId=${deskToChange}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Authorization: headerAuth,
            },
        });
        const res = await response.json();
        if (response.status === 200) {
            setDeskToChangeCode("");
            setNewDeskLocationCode("");
        }
        alert(res.message);
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
        <Paper sx={{ padding: "2%", backgroundColor: 'rgba(204, 200, 198, 60%)' }}>
            <Typography sx={{ flexGrow: "1", color: '#5E738C', fontWeight: "800" }}>Desks</Typography>
            <Table>
                <FormGroup>
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
            </Table>
            <Table>
                <FormGroup>
                    <InputLabel>Change desk location</InputLabel>
                    <form onSubmit={handleChangeDeskLocation}>
                        <TableRow>
                            <TableCell>
                                <TextField
                                    type="text"
                                    value={deskToChange}
                                    onChange={(e) => setDeskToChangeCode(e.target.value)}
                                    label="Desk to change"
                                />
                            </TableCell>
                            <TableCell>
                                <TextField
                                    type="text"
                                    value={newDeskLocation}
                                    onChange={(e) => setNewDeskLocationCode(e.target.value)}
                                    label="New desk location"
                                />
                            </TableCell>
                            <TableCell>
                                <Button variant="outlined" color="secondary" type="submit">Change</Button>
                            </TableCell>
                        </TableRow>
                    </form>
                </FormGroup>
            </Table>
            <Table className="desk-list">
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
            </Table>
        </Paper >
    );
};
export default DeskList;