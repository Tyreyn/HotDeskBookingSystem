import React, { useState, useEffect } from 'react';
import Desk from './Entity/DeskEntity';
import { TableHead, TableContainer, TableCell, TableRow, Input, Paper, TableBody, Button, Table } from "../../node_modules/@mui/material/index";


const DeskDisplay = ({ auth }) => {
    const [desks, setDesks] = useState([]);
    const [nameFilter, setNewNameFilter] = useState('');
    const [filteredDesks, setFilteredDesks] = useState(desks);

    useEffect(() => {
        handleShowDesks();
    }, [])

    const handleShowDesks = async () => {
        const headerAuth = auth.replaceAll('"', '');
        const response = await fetch('https://localhost:7147/Employee/GetAvailableDesks', {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                Authorization: headerAuth,
            },
        });
        const res = await response.json();
        console.log(res);
        if (res.length >>> 0) {
            setDesks(res);
            setFilteredDesks(res);
        }
    };

    const handleFilterDesks = (nameFilter) => {
        setNewNameFilter(nameFilter);
        const filtered = desks.filter((desk) => (
            desk.Location.Name.toLowerCase().includes(nameFilter.toLowerCase())
        ));
        setFilteredDesks(filtered);
    };

    return (
        <TableContainer component={Paper} style={{ height: "1000", innerWidth: "100%" }}>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>
                            <Input
                                type="text"
                                value={nameFilter}
                                onChange={(e) => handleFilterDesks(e.target.value)}
                                placeholder="Filter desk by location"
                            />
                        </TableCell>
                        <TableCell>
                            <div>
                                <Button onClick={handleShowDesks}>Refresh</Button>
                            </div>
                        </TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    <TableRow>
                        <TableCell style={{ textalign: "center" }}>
                            <div>
                                {(filteredDesks.length >= 1 ? <Desk desks={filteredDesks} /> : <div>No location with this name</div>)}
                            </div>
                        </TableCell>
                    </TableRow>
                </TableBody>
            </Table>
        </TableContainer >
    );
};

export default DeskDisplay;