import React, { useState } from 'react';
import { Input, Container } from '../../node_modules/@mui/material/index';
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
            <Container className="make-reservation">
                <form onSubmit={handleMakeReservation}>
                    <Container class="row mb-3">
                        <label>Enter desk id to be reserved:
                            <Input
                                type="text"
                                name="deskId"
                                value={makeReservationInputs.deskId || ""}
                                onChange={handleChangeMakeReservation}
                            />
                        </label>
                    </Container>
                    <Container class="row mb-3">
                        <label>Enter start date of reservation:
                            <Input
                                type="date"
                                name="dateStart"
                                value={makeReservationInputs.dateStart || ""}
                                onChange={handleChangeMakeReservation}
                            />
                        </label>
                    </Container>
                    <Container class="row mb-3">
                        <label>Enter end date of reservation:
                            <Input
                                type="date"
                                name="dateEnd"
                                value={makeReservationInputs.dateEnd || ""}
                                onChange={handleChangeMakeReservation}
                            />
                        </label>
                    </Container>
                    <Container class="row mb-3">
                        <input type="submit" />
                    </Container>
                </form>
            </Container>

            <Container className="change-desk">
                <form onSubmit={handleChangeDeskInReservation}>
                    <Container class="row mb-3">
                        <label>Enter new desk ID:
                            <Input
                                type="text"
                                name="newDeskId"
                                value={makeReservationInputs.newDeskId || ""}
                                onChange={handleChangeMakeReservation}
                            />
                        </label>
                    </Container>
                    <Container class="row mb-3">
                        <label>Enter reservation ID:
                            <Input
                                type="text"
                                name="reservationId"
                                value={makeReservationInputs.reservationId || ""}
                                onChange={handleChangeMakeReservation}
                            />
                        </label>
                    </Container>
                    <Container class="row mb-3">
                        <input type="submit" />
                    </Container>
                </form>
            </Container>
        </Container>
    );
};

export default EmployeePanel;