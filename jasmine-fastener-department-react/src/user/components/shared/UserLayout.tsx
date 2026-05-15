import UserSidebar from "./UserSidebar.tsx";
import {Box} from "@mui/material";
import {Outlet} from "react-router-dom";

const UserLayout = () => {
    return (
        <>
            <UserSidebar />
            <Box sx={{ display: 'flex', justifyContent: 'stretch' }}>
                <Outlet />
            </Box>
        </>
    );
};

export default UserLayout;