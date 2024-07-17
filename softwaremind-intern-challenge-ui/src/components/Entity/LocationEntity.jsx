import React from 'react';
import { TableCell } from '../../../node_modules/@mui/material/index';
const LocationEnity = ({ locations }) => {
    return (
        <>
            {
                <TableCell key={locations.Id}>
                    <h3>Location Name: {locations.Name} </h3>
                </TableCell>
            }
        </>
    );
};

export default LocationEnity;