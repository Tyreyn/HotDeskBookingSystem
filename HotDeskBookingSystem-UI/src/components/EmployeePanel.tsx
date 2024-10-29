import { Button, FormGroup, InputLabel, Paper, TableCell, TableRow, TextField, Typography } from "@mui/material";
import { useState } from "react";

const EmployeePanel = (auth: string) => {
    const [makeReservationInputs, setMakeReservationInputs] = useState({
        deskId: "",
        dateStart: Date,
        dateEnd: Date,
    });

    const handleChangeMakeReservation = (event: { target: { name: any; value: any; }; }) => {
        const name = event.target.name;
        const value = event.target.value;
        setMakeReservationInputs(values => ({ ...values, [name]: value }))
    }

    const handleMakeReservation = async (event: { preventDefault: () => void; }) => {
        event.preventDefault();
        const headerAuth = auth.auth.replaceAll('"', '');
        const formattedStartDate = convertDateFormat(makeReservationInputs.dateStart);
        const formattedEndDate = convertDateFormat(makeReservationInputs.dateEnd);
        const response = await fetch(`https://localhost:7147/Employee/MakeReservation?deskId=${makeReservationInputs.deskId}&dateStart=${formattedStartDate}&dateEnd=${formattedEndDate}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Authorization: headerAuth,
            },
        });
        const res = await response.json();
        console.log(res);
        if (res.length >>> 0) {
            //setDesks(res);
            //setFilteredDesks(res);
        }
        alert(res.message);
    };

    const handleChangeDeskInReservation = async (event: { preventDefault: () => void; }) => {
        event.preventDefault();
        const headerAuth = auth.replaceAll('"', '');
        const response = await fetch(`https://localhost:7147/Employee/ChangeReservationDesk?reservationId=${makeReservationInputs.reservationId}&newDeskId=${makeReservationInputs.newDeskId}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Authorization: headerAuth,
            },
        });
        const res = await response.json();
        console.log(res);
        if (res.length >>> 0) {
            //setDesks(res);
            //setFilteredDesks(res);
        }
        alert(res.message);
    };

    const convertDateFormat = (dateString: { split: (arg0: string) => [any, any, any]; }) => {
        const [year, month, day] = dateString.split('-');
        return `${month}/${day}/${year}`;
    };

    return (
        <Paper sx={{ padding: "2%", backgroundColor: 'rgba(204, 200, 198, 60%)', width: "inherit"}}>
            <Typography sx={{ flexGrow: "1", color: '#5E738C', fontWeight: "800" }}>Reservations</Typography>
            <Paper sx={{ padding: "2%", backgroundColor: 'rgba(204, 200, 198, 60%)', margin: "2%", display: "flex", justifyContent: "center" }}>
                <form onSubmit={handleMakeReservation}>
                    <TableRow sx={{ display: "flex", justifyContent: "center" }}>
                        <InputLabel>Make new reservation</InputLabel>
                    </TableRow>
                    <TableRow sx={{ borderBottom: "0px" }}>
                        <TableCell>
                            <TextField
                                label="Enter desk id to be reserved"
                                type="text"
                                name="deskId"
                                value={makeReservationInputs.deskId || ""}
                                onChange={handleChangeMakeReservation}
                            />
                        </TableCell>
                    </TableRow>
                    <TableRow>
                        <TableCell>
                            <TextField
                                helperText='Enter start date of reservation:'
                                name="dateStart"
                                type="date"
                                value={makeReservationInputs.dateStart || ""}
                                onChange={handleChangeMakeReservation}
                            />
                        </TableCell>
                    </TableRow>
                    <TableRow>
                        <TableCell>
                            <TextField
                                helperText="Enter end date of reservation:"
                                type="date"
                                name="dateEnd"
                                value={makeReservationInputs.dateEnd || ""}
                                onChange={handleChangeMakeReservation}
                            />
                        </TableCell>
                    </TableRow>
                    <TableRow sx={{ display: "flex", justifyContent: "center" }}>
                        <TableCell>
                            <Button variant="outlined" color="secondary" type="submit">Submit</Button>
                        </TableCell>
                    </TableRow>
                </form>
            </Paper>
            <Paper sx={{ padding: "2%", backgroundColor: 'rgba(204, 200, 198, 60%)', margin: "2%", display: "flex", justifyContent: "center" }}>
                <form onSubmit={handleChangeDeskInReservation}>
                    <TableRow sx={{ display: "flex", justifyContent: "center" }}>
                        <InputLabel>Change desk in reservation</InputLabel>
                    </TableRow>
                    <TableRow>
                        <TableCell>
                            <TextField
                                label="Enter new desk id to be changed"
                                type="text"
                                name="newDeskId"
                                value={makeReservationInputs.newDeskId || ""}
                                onChange={handleChangeMakeReservation}
                            />
                        </TableCell>
                    </TableRow>
                    <TableRow>
                        <TableCell>
                            <TextField
                                label="Enter reservation id to be changed"
                                type="text"
                                name="reservationId"
                                value={makeReservationInputs.reservationId || ""}
                                onChange={handleChangeMakeReservation}
                            />
                        </TableCell>
                    </TableRow>
                    <TableRow sx={{ display: "flex", justifyContent: "center" }}>
                        <TableCell>
                            <Button variant="outlined" color="secondary" type="submit">Submit</Button>
                        </TableCell>
                    </TableRow>
                </form>
            </Paper>
        </Paper>
    );
};


export default EmployeePanel;