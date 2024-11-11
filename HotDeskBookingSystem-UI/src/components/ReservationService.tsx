const convertDateFormat = (dateString: { split: (arg0: string) => [any, any, any]; }) => {
    const [year, month, day] = dateString.split('-');
    return `${month}/${day}/${year}`;
};

interface MakeReservation {
    deskId: string;
    dateStart: Date;
    dateEnd: Date
};

interface ChangeReservation {
    newDeskId: string;
    reservationId: string;
};

export const handleMakeReservation = async (
    event,
    auth,
    makeReservationInputs: MakeReservation) => {
    event.preventDefault();
    console.log(auth);
    const headerAuth = auth.replace(new RegExp('"', 'g'), '');
    const formattedStartDate = convertDateFormat(makeReservationInputs.dateStart);
    const formattedEndDate = convertDateFormat(makeReservationInputs.dateEnd);
    const response = await fetch(`https://localhost:7147/Employee/MakeReservation?deskId=
    ${makeReservationInputs.deskId}&dateStart=${formattedStartDate}&dateEnd=${formattedEndDate}`, {
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

export const handleChangeDeskInReservation = async (
    event: { preventDefault: () => void; },
    auth,
    changeReservationInputs: ChangeReservation) => {
    event.preventDefault();
    const headerAuth = auth.replace(new RegExp('"', 'g'), '');
    const response = await fetch(`https://localhost:7147/Employee/ChangeReservationDesk?reservationId=
    ${changeReservationInputs.reservationId}&newDeskId=${changeReservationInputs.newDeskId}`, {
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