import { Button, InputLabel, Paper, TableCell, TableRow, TextField, Typography } from "@mui/material";
import { useState } from "react";
import { handleChangeDeskInReservation, handleMakeReservation } from "./ReservationService";

const EmployeePanel = ({ auth, Id, ReservationId }) => {
    const [makeReservationInputs, setMakeReservationInputs] = useState({
        deskId: Id,
        dateStart: Date,
        dateEnd: Date,
        newDeskId: Number,
        reservationId: ReservationId,
    });

    const handleChangeMakeReservation = (event: { target: { name: any; value: any; }; }) => {
        const name = event.target.name;
        const value = event.target.value;
        setMakeReservationInputs(values => ({ ...values, [name]: value }))
    }

    return (
        <Paper sx={{ padding: "2%", backgroundColor: 'rgba(204, 200, 198, 60%)', width: "inherit" }}>
            <Typography sx={{ flexGrow: "1", color: '#5E738C', fontWeight: "800" }}>Reservations</Typography>
            <Paper sx={{ padding: "2%", backgroundColor: 'rgba(204, 200, 198, 60%)', margin: "2%", display: "flex", justifyContent: "center" }}>
                <form onSubmit={function (e) { handleMakeReservation(e, auth, makeReservationInputs); }}>
                    <TableRow sx={{ display: "flex", justifyContent: "center" }}>
                        <InputLabel>Make new reservation</InputLabel>
                    </TableRow>
                    {Id === undefined ?
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
                        : <div></div>
                    }
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
            {ReservationId !== undefined || (localStorage.getItem('role') === "user")
                ?
            <Paper sx={{ padding: "2%", backgroundColor: 'rgba(204, 200, 198, 60%)', margin: "2%", display: "flex", justifyContent: "center" }}>
                <form onSubmit={function (e) { handleChangeDeskInReservation(e, auth, makeReservationInputs); }}>
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
                    {ReservationId === undefined
                        ?
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
                        : <div></div>
                    }
                    <TableRow sx={{ display: "flex", justifyContent: "center" }}>
                        <TableCell>
                            <Button variant="outlined" color="secondary" type="submit">Submit</Button>
                        </TableCell>
                    </TableRow>
                </form>
                </Paper>
                : <div></div>
            }
        </Paper >
    );
};


export default EmployeePanel;