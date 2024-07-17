import EmployeeEntity from "./EmployeeEntity";
const ReservationEntity = ({ reservations }) => {
    return (
        <>
            {
                <div key={reservations[0].Id} style={{ textAlign: "center" }}>
                    <td><h5>Reservation ID: {reservations[0].Id}</h5></td>
                    <tr>
                        <EmployeeEntity employees={reservations[0].Employee} />
                    </tr>
                    <td><h5>DateStart: {reservations[0].DateStart}</h5></td>
                    <td><h5>DateEnd: {reservations[0].DateEnd}</h5></td>
                </div>
            }
        </>
    );
};
export default ReservationEntity;