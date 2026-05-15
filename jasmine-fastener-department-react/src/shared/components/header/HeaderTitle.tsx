import { Box } from '@mui/material';
import Typography from '../Typography';
import {neutralColors} from "../../../assets/variables/neutralColors.ts";

export interface HeaderTitleProps {
  title: string;
}

const HeaderTitle = (props: HeaderTitleProps) => {
  return (
    <Box>
      <Typography variant="headlineH1" sx={{ color: neutralColors.black }}>
        {props.title}
      </Typography>
    </Box>
  );
};

export default HeaderTitle;
