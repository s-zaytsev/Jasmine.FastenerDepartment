import {Outlet} from "react-router-dom";
import {Box} from "@mui/material";
import UserSidebar from "../../../user/components/shared/UserSidebar.tsx";
import {semanticColors} from "../../../assets/variables/semanticColors.ts";

const RootLayout = () => {
    return (
        <Box
            sx={{
                backgroundColor: semanticColors.surface.light,
                display: 'grid',
                justifyContent: 'stretch',
                gridTemplateColumns: 'auto 1fr',
                gap: '20px',
                padding: '24px',
                flex: 1,
            }}
        >
            <UserSidebar />
            <Outlet />
        </Box>
    );
};

export default RootLayout;