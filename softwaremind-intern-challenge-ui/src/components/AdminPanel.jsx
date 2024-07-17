import LocationList from './LocationList';
import DeskList from './DeskList';

const AdminPanel = (auth) => {
    return (
        <div className="admin-panel">
            <h1>Admin Panel</h1>
            <LocationList auth={auth} />
            <DeskList auth={auth} />
        </div>
    );
};

export default AdminPanel;