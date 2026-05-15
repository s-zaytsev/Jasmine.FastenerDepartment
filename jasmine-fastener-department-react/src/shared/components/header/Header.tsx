import { Box, type SxProps } from "@mui/material";
import type {ReactNode} from "react";

export interface HeaderProps {
  children?: ReactNode;
  sx?: SxProps;
}

const Header = ({ children, sx }: HeaderProps) => {
  return (
    <Box
      sx={{
        alignItems: 'center',
        display: 'flex',
        height: '70px',
        justifyContent: 'space-between',
        mb: "0.75rem",
        ...sx
      }}
    >
      {children}
    </Box>
  );
};

export default Header;