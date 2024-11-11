import * as React from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import Modal from '@mui/material/Modal';
import EmployeePanel from './EmployeePanel';
import AdminPanel from './AdminPanel';

const style = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    boxShadow: 24,
    p: 4,
};

const BasicModal = ({ Id, auth, Reservations }) => {
    const [open, setOpen] = React.useState(false);
    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    return (
        <div>
            <Button onClick={handleOpen}>{Id}</Button>
            <Modal
                open={open}
                onClose={handleClose}
            >
                <Box sx={style}>
                    {Reservations && Reservations.length > 0 ?
                        Reservations.map((reservation) => {
                            return (
                                <div>
                                    <Typography>Reservation Id: {reservation.Id}</Typography>
                                    <EmployeePanel auth={auth} Id={Id} ReservationId={reservation.Id} />
                                </div>
                            );
                        }) :
                        <div>
                            <Typography>There is no reservation</Typography>
                            <EmployeePanel auth={auth} Id={Id} ReservationId={undefined} />
                        </div>
                    }
                </Box>
            </Modal>
        </div>
    );
}

export default BasicModal;