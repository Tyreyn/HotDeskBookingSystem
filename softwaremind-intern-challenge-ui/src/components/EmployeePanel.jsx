import React, { useState } from 'react';

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
        <div className="reservation-forms">
            <div className="make-reservation">
                <form onSubmit={handleMakeReservation}>
                    <div class="row mb-3">
                        <label>Enter desk id to be reserved:
                            <input
                                type="text"
                                name="deskId"
                                value={makeReservationInputs.deskId || ""}
                                onChange={handleChangeMakeReservation}
                            />
                        </label>
                    </div>
                    <div class="row mb-3">
                        <label>Enter start date of reservation:
                            <input
                                type="date"
                                name="dateStart"
                                value={makeReservationInputs.dateStart || ""}
                                onChange={handleChangeMakeReservation}
                            />
                        </label>
                    </div>
                    <div class="row mb-3">
                        <label>Enter end date of reservation:
                            <input
                                type="date"
                                name="dateEnd"
                                value={makeReservationInputs.dateEnd || ""}
                                onChange={handleChangeMakeReservation}
                            />
                        </label>
                    </div>
                    <div class="row mb-3">
                        <input type="submit" />
                    </div>
                </form>
            </div>

            <div className="change-desk">
                <form onSubmit={handleChangeDeskInReservation}>
                    <div class="row mb-3">
                        <label>Enter new desk ID:
                            <input
                                type="text"
                                name="newDeskId"
                                value={makeReservationInputs.newDeskId || ""}
                                onChange={handleChangeMakeReservation}
                            />
                        </label>
                    </div>
                    <div class="row mb-3">
                        <label>Enter reservation ID:
                            <input
                                type="text"
                                name="reservationId"
                                value={makeReservationInputs.reservationId || ""}
                                onChange={handleChangeMakeReservation}
                            />
                        </label>
                    </div>
                    <div class="row mb-3">
                        <input type="submit" />
                    </div>
                </form>
            </div>
        </div>
    );
};

export default EmployeePanel;