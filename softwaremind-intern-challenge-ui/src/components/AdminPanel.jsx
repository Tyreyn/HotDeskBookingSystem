import LocationList from './LocationList';
import DeskList from './DeskList';
import { Container } from '../../node_modules/@mui/material/index';
const AdminPanel = (auth) => {
    return (
        <Container className="admin-panel">
            <h1>Admin Panel</h1>
            <LocationList auth={auth} />
            <DeskList auth={auth} />
        </Container>
    );
};

export default AdminPanel;