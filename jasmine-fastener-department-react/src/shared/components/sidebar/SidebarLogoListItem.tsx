import { NavLink } from "react-router-dom";
import { styled } from '@mui/system';
import ListItemPrimitive from '@mui/material/ListItem';
import ListItemIconPrimitive from '@mui/material/ListItemIcon';
import LogoIcon from "../../../assets/LogoIcon.tsx";

const ListItem = styled(ListItemPrimitive)({
  display: 'flex',
  justifyContent: 'center',
  padding: 0,
});

const ListItemIcon = styled(ListItemIconPrimitive)({
  display: 'flex',
  justifyContent: 'center',
});

const SidebarLogoListItem = () => {
  return (
    <ListItem sx={{ marginBottom: 3.5 }}>
      <NavLink to={"/"}>
        <ListItemIcon
          sx={{
            width: "64px",
            display: "flex",
            alignContent: "center",
            alignItems: "center"
          }}
        >
          <LogoIcon />
        </ListItemIcon>
      </NavLink>
    </ListItem>
  );
};

export default SidebarLogoListItem;
