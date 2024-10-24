import { Button, Container, Input, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";
import React, { useEffect, useState } from "react";

const DeskDisplay = (auth) => {
    const [desks, setDesks] = useState([]);
    const [nameFilter, setNewNameFilter] = useState('');
    const [filteredDesks, setFilteredDesks] = useState(desks);

    useEffect(() => {
        handleShowDesks();
    }, [])


    interface Desk {
        LocationId: number;
        Location: Location;
        IsAvailable: boolean;
        Id: number;
    }

    interface Location {
        Id: number;
        Name: string;
    }

    async function handleShowDesks(): Promise<Desk[]> {
        const headerAuth = auth.auth.replaceAll('"', '');
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
        return res;
    };

    const handleFilterDesks = (nameFilter: string) => {
        setNewNameFilter(nameFilter);
        const filtered = desks.filter((desk) => (
            desk.Location.Name.toLowerCase().includes(nameFilter.toLowerCase())
        ));
        setFilteredDesks(filtered);
    };

    const deskList = filteredDesks.map((desk: Desk) => (

        <TableCell key={desk.Id} sx={{ textalign: "center" }}>
            <h2>{desk.LocationId}</h2>
            <p>{desk.Location.Name}</p>
        </TableCell>
    ));


    return (
        <Paper sx={{ padding: "2%", backgroundColor: 'rgba(204, 200, 198, 60%)', width: "80vw" }}>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell sx={{ width: "60vw" }}>
                            <Input
                                type="text"
                                value={nameFilter}
                                onChange={(e) => handleFilterDesks(e.target.value)}
                                placeholder="Filter desk by location"
                            />
                        </TableCell>
                        <TableCell sx={{ width: "20vw", display: "flex", justifyContent: "center" }}>
                            <Button onClick={handleShowDesks}>Refresh</Button>
                        </TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    <TableRow>
                        {deskList}
                    </TableRow>
                </TableBody>
            </Table>
        </Paper >
    );
};

export default DeskDisplay;   