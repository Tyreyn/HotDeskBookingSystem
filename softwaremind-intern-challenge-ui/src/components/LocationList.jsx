import React, { useState } from 'react';
const LocationList = (auth) => {
    const [locationToDelete, setLocationToDelete] = useState('');
    const [newLocation, setNewLocation] = useState('');

    const handleAddLocation = async () => {
        let requestParam = '?locationName=New Room';
        if (newLocation) {
            requestParam = `?locationName=${newLocation}`;
        }
        const headerAuth = auth.auth.auth.replaceAll('"', '');
        const response = await fetch(`https://localhost:7147/Admin/AddNewLocation${requestParam}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Authorization: headerAuth,
            },
        });
        if (response.status === 200) {
            alert("New location added correctly");
            setNewLocation("");
        }
        else {
            alert("Something went wrong");
        }
    };

    const handleDeleteLocation = async () => {
        const headerAuth = auth.auth.auth.replaceAll('"', '');
        const response = await fetch(`https://localhost:7147/Admin/DeleteLocation?locationId=${locationToDelete}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Authorization: headerAuth,
            },
        });
        const res = await response.json();
        if (response.status === 200) {
            setLocationToDelete("");
        }
        alert(res.message);
    };

    return (
        <div className="location-list">
            <h2>Locations</h2>
            <input
                type="text"
                value={newLocation}
                onChange={(e) => setNewLocation(e.target.value)}
                placeholder="(Optional) set location name."
            />
            <button onClick={handleAddLocation}>Add</button>
            <input
                type="text"
                value={locationToDelete}
                onChange={(e) => setLocationToDelete(e.target.value)}
                placeholder="Id of location to be deleted."
            />
            <button onClick={handleDeleteLocation}>Delete</button>

        </div>
    );
};

export default LocationList;