import ListItemPrimitive from '@mui/material/ListItem';
import ListItemIconPrimitive from '@mui/material/ListItemIcon';
import { styled } from '@mui/system';
import type {ReactNode} from 'react';
import { NavLink } from 'react-router-dom';

const ListItem = styled(ListItemPrimitive)({
  display: 'flex',
  justifyContent: 'start',
  padding: 0,
});

const ListItemText = styled('span')({
  display: 'flex',
  justifyContent: 'center',
});

const ListItemIcon = styled(ListItemIconPrimitive)({
  display: 'flex',
  justifyContent: 'center',
});

const SideLink = styled(NavLink)(
  ({ theme }) => `
    text-decoration: none;
    align-items: center;
    display: flex;
    gap: 0.5rem;

    ${ListItemIcon} {
      display: grid;
      place-items: center;
      max-width: 36px;
      min-width: 36px;
      justify-content: center;
      border-radius: 6px;
      background-color: ${theme.palette.primary.defaultMain};
      color: ${theme.palette.primary.dafaultLines};
      height: 36px;
    }

    ${ListItemText} {
      color: ${theme.palette.text.default};
      font-style: normal;
      font-size: 16px;
      line-height: 110%;
      text-align: center;
    }

    &:hover ${ListItemIcon} {
      background-color: ${theme.palette.primary.hoverMain};
      color: ${theme.palette.primary.hoverLines};
    }

    &:hover ${ListItemText} {
      color: ${theme.palette.text.hover};
    }

    &.active ${ListItemIcon} {
      background-color: ${theme.palette.primary.main};
      color: ${theme.palette.primary.lines};
    }

    &.active ${ListItemText} {
      color: ${theme.palette.text.active};
    }

    &:active ${ListItemIcon} {
      background-color: ${theme.palette.primary.focusedMain};
      color: ${theme.palette.primary.focusedLines};
    }

    &:active ${ListItemText} {
      color: ${theme.palette.text.focused};
    }
  `
);

export interface SidebarListItemProps {
  icon: ReactNode;
  link: string;
  title: string;
}

const SidebarListItem = ({ icon, link, title }: SidebarListItemProps) => {
  return (
    <ListItem>
      <SideLink to={link}>
        <ListItemIcon>{icon}</ListItemIcon>
        <ListItemText>{title}</ListItemText>
      </SideLink>
    </ListItem>
  );
};

export default SidebarListItem;
