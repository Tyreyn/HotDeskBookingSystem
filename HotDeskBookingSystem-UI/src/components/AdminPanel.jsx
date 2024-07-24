import LocationList from './LocationList';
import DeskList from './DeskList';
import { TableRow, TableCell, Container } from '../../node_modules/@mui/material/index';
const AdminPanel = (auth) => {
    return (
        <Container className="admin-panel">
            <h1>Admin Panel</h1>
            <TableRow>
                <LocationList auth={auth} />
            </TableRow>
            <TableRow>
                <DeskList auth={auth} />
            </TableRow >
        </Container>
    );
};

export default AdminPanel;