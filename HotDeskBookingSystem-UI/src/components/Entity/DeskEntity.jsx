import LocationEnity from "./LocationEntity";
import React from 'react';
import ReservationEntity from "./ReservationEntity";
import { Container, TableCell, TableRow } from "../../../node_modules/@mui/material/index";
const DeskEntity = ({ desks }) => {
    return (
        <>
            {
                desks.map((desk) => {
                    const { Id, IsAvailable, LocationId, Location, Reservations } = desk;

                    return (
                        <TableRow key={Id}>
                            {IsAvailable === true
                                ? (
                                    <Container className="reservation-list">
                                        <TableRow>
                                            <TableCell><h3 style={{ color: 'black' }}>Id: {Id}</h3></TableCell>
                                            <TableCell><h3 style={{ color: 'black' }}>LocationId: {LocationId}</h3></TableCell>
                                            <LocationEnity locations={Location} />
                                        </TableRow>
                                        {Reservations != null && (Reservations.length > 0 && <ReservationEntity reservations={Reservations} />)}
                                    </Container>
                                )
                                : (
                                    <Container className="reservation-list">
                                        <TableCell><h3 style={{ color: 'red' }}>Id: {Id}</h3></TableCell>
                                        <TableCell><h3 style={{ color: 'red' }}>LocationId: {LocationId}</h3></TableCell>
                                        <LocationEnity locations={Location} />
                                        {Reservations != null && (Reservations.length > 0 && <ReservationEntity reservations={Reservations} />)}
                                    </Container>
                                )}
                        </TableRow>
                    );
                })}
        </>
    );
};

export default DeskEntity;