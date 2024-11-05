import { Button, Grid2, Input, Paper, Table, TableBody, TableCell, TableHead, TableRow, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import _ from "lodash";

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

    const deskList = () => {
        const groupedByLocation = filteredDesks.reduce((result, item) => {
            if (!result[item.LocationId]) {
                result[item.LocationId] = [];
            }
            result[item.LocationId].push(item);
            return result;
        }, {});

        return (
            <TableRow>
                {Object.keys(groupedByLocation).map(locationId => (
                    <Paper key={locationId} sx={{ margin: "10px", padding: "2%", backgroundColor: 'rgba(204, 200, 198, 60%)' }}>
                        <Typography sx={{ flexGrow: "1", color: '#5E738C', fontWeight: "800" }}>Location ID: {locationId}</Typography>
                        <Grid2 container spacing={3} sx={{ justifyContent: "space-evenly" }}>
                        {groupedByLocation[locationId].map(desk => (
                            <Paper key={desk.Id} sx={{ backgroundColor: desk.IsAvailable ? 'rgba(204, 200, 198, 80%)' : 'rgba(166, 0, 55, 80%)' }}>
                                <p>ID: {desk.Id}</p>
                                <p>Is Available: {desk.IsAvailable ? "Yes" : "No"}</p>
                            </Paper>
                        ))}
                        </Grid2>
                    </Paper>
                ))}
            </TableRow>
        );
    };


    return (
        <Paper sx={{ padding: "2%", backgroundColor: 'rgba(204, 200, 198, 60%)', width: "80vw" }}>
            <Table>
                <TableHead>
                    <TableRow sx={{ display: "flex", justifyContent: "space-around" }}>
                        <TableCell>
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
                <TableBody sx={{ justifyItems: "center"}}>
                    {deskList()}
                </TableBody>
            </Table>
        </Paper >
    );
};

export default DeskDisplay;   