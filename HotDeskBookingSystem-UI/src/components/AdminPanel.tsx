import { Container, Paper, TableRow, Typography } from "@mui/material";
import LocationList from "./LocationList";
import DeskList from "./DeskList";

const AdminPanel = (auth : any) => {
    return (
        <Paper sx={{ padding: "2%", backgroundColor: 'rgba(204, 200, 198, 60%)' }}>
            <Typography sx={{ flexGrow: "1", color: '#5E738C', fontWeight: "800" }}>Admin Panel</Typography>
            <TableRow>
                <LocationList auth={auth.auth} />
            </TableRow>
            <TableRow>
                <DeskList auth={auth.auth} />
            </TableRow >
        </Paper>
    );
};
export default AdminPanel;