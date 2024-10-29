import React, { useState } from 'react';
import { Container, TableCell, TableRow, Button, TextField, FormGroup, InputLabel, Paper, Table, Typography } from '@mui/material';
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
        <Paper sx={{ padding: "2%", backgroundColor: 'rgba(204, 200, 198, 60%)' }}>
            <Typography sx={{ flexGrow: "1", color: '#5E738C', fontWeight: "800" }}>Locations</Typography>
            < Table className="desk-list" >
                <FormGroup>
                    <InputLabel>Add new location </InputLabel>
                    < form onSubmit={handleAddLocation} >
                        <TableRow>
                            <TableCell>
                                <TextField
                                    type="text"
                                    value={newLocation}
                                    onChange={(e) => setNewLocation(e.target.value)}
                                    placeholder="(Optional) set location name."
                                />
                            </TableCell>
                            < TableCell >
                                <Button variant="outlined" color="secondary" type="submit" > Add </Button>
                            </TableCell>
                        </TableRow>
                    </form>
                </FormGroup>
            </Table>
            < Table className="desk-list" >
                <FormGroup>
                    <InputLabel>Delete location </InputLabel>
                    < form onSubmit={handleDeleteLocation} >
                        <TableRow>
                            <TableCell>
                                <TextField
                                    type="text"
                                    value={locationToDelete}
                                    onChange={(e) => setLocationToDelete(e.target.value)}
                                    placeholder="Id of location to be deleted."
                                />
                            </TableCell>
                            < TableCell >
                                <Button variant="outlined" color="secondary" type="submit" > Delete </Button>
                            </TableCell>
                        </TableRow>
                    </form>
                </FormGroup>
            </Table>
        </Paper>
    );
};
export default LocationList;