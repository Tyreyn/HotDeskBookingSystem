const EmployeeEntity = ({ employees }) => {
    return (
        <>
            {
                <div key={employees.Id}>
                    <tr><h5>Employee ID: {employees.Id}</h5></tr>
                    <tr><h5>Employee Email: {employees.Email} </h5></tr>
                    <tr><h5>Password: {employees.Password}</h5></tr>
                    <tr><h5>Role: {employees.Role}</h5></tr>
                </div>
            }
        </>
    );
};
export default EmployeeEntity;