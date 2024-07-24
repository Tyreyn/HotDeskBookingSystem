import React, { useState } from 'react';
import { Container, TableCell, TableRow, Button, TextField, FormGroup, InputLabel } from '../../node_modules/@mui/material/index';

const LocationList = (auth) => {
    const [locationToDelete, setLocationToDelete] = useState('');
    const [newLocation, setNewLocation] = useState('');

    const handleAddLocation = async () => {
        let requestParam = '?locationName=New Room';
        if (newLocation) {
            requestParam = `?locationName=${newLocation}`;
        }
        const headerAuth = auth.auth.auth.replaceAll('"', '');
        const response = await fetch(`https://localhost:7147/Admin/AddNewLocation${requestParam}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Authorization: headerAuth,
            },
        });
        if (response.status === 200) {
            alert("New location added correctly");
            setNewLocation("");
        }
        else {
            alert("Something went wrong");
        }
    };

    const handleDeleteLocation = async () => {
        const headerAuth = auth.auth.auth.replaceAll('"', '');
        const response = await fetch(`https://localhost:7147/Admin/DeleteLocation?locationId=${locationToDelete}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Authorization: headerAuth,
            },
        });
        const res = await response.json();
        if (response.status === 200) {
            setLocationToDelete("");
        }
        alert(res.message);
    };

    return (
        <TableCell className="desk-list">
            <h2>Locations</h2>
            <Container className="desk-list">
                <FormGroup className="add-location" class="col-md-6 col-md-offset-3 text-center">
                    <InputLabel>Add new location</InputLabel>
                    <form onSubmit={handleAddLocation}>
                        <TableRow>
                            <TableCell>
                                <TextField
                                    type="text"
                                    value={newLocation}
                                    onChange={(e) => setNewLocation(e.target.value)}
                                    placeholder="(Optional) set location name."
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
                <FormGroup className="delete-location" class="col-md-6 col-md-offset-3 text-center">
                    <InputLabel>Delete location</InputLabel>
                    <form onSubmit={handleDeleteLocation}>
                        <TableRow>
                            <TableCell>
                                <TextField
                                    type="text"
                                    value={locationToDelete}
                                    onChange={(e) => setLocationToDelete(e.target.value)}
                                    placeholder="Id of location to be deleted."
                                />
                            </TableCell>
                            <TableCell>
                                <Button variant="outlined" color="secondary" type="submit">Delete</Button>
                            </TableCell>
                        </TableRow>
                    </form>
                </FormGroup>
            </Container>

        </TableCell>
    );
};

export default LocationList;