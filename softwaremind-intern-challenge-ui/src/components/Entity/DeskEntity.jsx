import LocationEnity from "./LocationEntity";
import React from 'react';
import ReservationEntity from "./ReservationEntity";
const DeskEntity = ({ desks }) => {
    return (
        <>
            {
                desks.map((desk) => {
                    const { Id, IsAvailable, LocationId, Location, Reservations } = desk;

                    return (
                        <tr key={Id}>
                            {IsAvailable === true
                                ? (
                                    <div >
                                        <td><h5 style={{ color: 'black' }}>Id: {Id}</h5></td>
                                        <td><h5 style={{ color: 'black' }}>LocationId: {LocationId}</h5></td>
                                        <LocationEnity locations={Location} />
                                        {Reservations != null && (Reservations.length > 0 && <ReservationEntity reservations={Reservations} />)}
                                    </div>
                                )
                                : (
                                    <div key={Id}>
                                        <td><h5 style={{ color: 'red' }}>Id: {Id}</h5></td>
                                        <td><h5 style={{ color: 'red' }}>LocationId: {LocationId}</h5></td>
                                        <LocationEnity locations={Location} />
                                        {Reservations != null && (Reservations.length > 0 && <ReservationEntity reservations={Reservations} />)}
                                    </div>
                                )}
                        </tr>
                    );
                })}
        </>
    );
};

export default DeskEntity;