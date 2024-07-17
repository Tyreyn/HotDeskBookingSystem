import React, { useState } from 'react';

const DeskList = (auth) => {
    const [newDeskCode, setNewDeskCode] = useState('');
    const [deskToDelete, setDeskToDelete] = useState('');

    const handleAddDesk = async () => {
        let requestParam = '?locationId=1';
        if (newDeskCode) {
            requestParam = `?locationId=${newDeskCode}`;
        }
        const headerAuth = auth.auth.auth.replaceAll('"', '');
        const response = await fetch(`https://localhost:7147/Admin/AddNewDesk${requestParam}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Authorization: headerAuth,
            },
        });
        if (response.status === 200) {
            alert("New desk added correctly");
            setNewDeskCode("");
        }
        else {
            alert("Something went wrong");
        }
    };

    const handleDeleteDesk = async () => {
        const headerAuth = auth.auth.auth.replaceAll('"', '');
        const response = await fetch(`https://localhost:7147/Admin/DeleteDesk?deskId=${deskToDelete}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Authorization: headerAuth,
            },
        });
        const res = await response.json();
        if (response.status === 200) {
            setDeskToDelete("");
        }
        alert(res.message);
    };

    return (
        <div className="desk-list">
            <h2>Desks</h2>
            <input
                type="text"
                value={newDeskCode}
                onChange={(e) => setNewDeskCode(e.target.value)}
                placeholder="(Optional) Location for new desk"
            />
            <button onClick={handleAddDesk}>Add</button>

            <input
                type="text"
                value={deskToDelete}
                onChange={(e) => setDeskToDelete(e.target.value)}
                placeholder="Id of desk to be deleted."
            />
            <button onClick={handleDeleteDesk}>Delete</button>
        </div>
    );
};

export default DeskList;