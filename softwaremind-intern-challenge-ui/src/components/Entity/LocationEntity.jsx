import React from 'react';
const LocationEnity = ({ locations }) => {
    return (
        <>
            {
                <td key={locations.Id}>
                    <td><h5>Location Name: {locations.Name} </h5></td>
                </td>
            }
        </>
    );
};

export default LocationEnity;