import React, { useState } from 'react';
import { Input, Container, FormGroup, TableCell, FormControl, TextField, InputLabel, Button, FormLabel, TableRow } from '../../node_modules/@mui/material/index';
const EmployeePanel = ({ auth }) => {
    const [makeReservationInputs, setMakeReservationInputs] = useState({});

    const handleChangeMakeReservation = (event) => {
        const name = event.target.name;
        const value = event.target.value;
        setMakeReservationInputs(values => ({ ...values, [name]: value }))
    }

    const handleMakeReservation = async (event) => {
        event.preventDefault();
        const headerAuth = auth.replaceAll('"', '');
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
            setDesks(res);
            setFilteredDesks(res);
        }
        alert(res.message);
    };

    const handleChangeDeskInReservation = async (event) => {
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
            setDesks(res);
            setFilteredDesks(res);
        }
        alert(res.message);
    };

    const convertDateFormat = (dateString) => {
        const [year, month, day] = dateString.split('-');
        return `${month}/${day}/${year}`;
    };

    return (
        <Container className="reservation-list">
            <h2>Reservations</h2>
            <Container className="reservation-list">
                <FormGroup className="make-reservation" class="col-md-6 col-md-offset-3 text-center">
                    <InputLabel>Make new reservation</InputLabel>
                    <form onSubmit={handleMakeReservation}>
                        <TableRow>
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
                                    shrink
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
                        <TableRow>
                            <TableCell>
                                <Button variant="outlined" color="secondary" type="submit">Submit</Button>
                            </TableCell>
                        </TableRow>
                    </form>
                </FormGroup>
            </Container>
            <Container className="reservation-list">
                <FormGroup className="change-desk">
                    <InputLabel>Change desk in reservation</InputLabel>
                    <form onSubmit={handleChangeDeskInReservation}>
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
                        <TableRow>
                            <TableCell>
                                <Button variant="outlined" color="secondary" type="submit">Submit</Button>
                            </TableCell>
                        </TableRow>
                    </form>
                </FormGroup>
            </Container>
        </Container>
    );
};

export default EmployeePanel;