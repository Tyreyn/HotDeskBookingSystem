import EmployeeEntity from "./EmployeeEntity";
import { Container, TableCell, TableRow } from "../../../node_modules/@mui/material/index";
const ReservationEntity = ({ reservations }) => {
    return (
        <>
            {
                <Container className="reservation-list">
                    <Container key={reservations[0].Id} style={{ textAlign: "center" }}>
                        <TableCell><h5>Reservation ID: {reservations[0].Id}</h5></TableCell>
                        <TableRow>
                            <EmployeeEntity employees={reservations[0].Employee} />
                        </TableRow>
                        <TableCell><h5>DateStart: {reservations[0].DateStart}</h5></TableCell>
                        <TableCell><h5>DateEnd: {reservations[0].DateEnd}</h5></TableCell>
                    </Container>
                </Container >
            }
        </>
    );
};
export default ReservationEntity;