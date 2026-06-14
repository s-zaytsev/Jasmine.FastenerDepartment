import {memo, type ReactNode} from "react";
import {Box} from "@mui/material";
import {neutralColors} from "../../../assets/variables/neutralColors.ts";

type TableRowProps = {
    children: ReactNode;
    onClick?: () => void;
    hasHighlight?: boolean;
}

const TableRow = (props: TableRowProps) => {
    return (
        <Box
            onClick={props.onClick}
            sx={{
                display: "flex",
                alignItems: "center",
                backgroundColor: neutralColors.white,
                padding: '0.5rem',
                width: '100%',
                borderBottom: '1px solid #e4e6ea80',
                cursor: props.onClick ? 'pointer' : 'default',
                '&:hover': !props.hasHighlight ? {} : {
                    backgroundColor: '#FBF8FF'
                }
            }}>
            {props.children}
        </Box>
    )
}

export default memo(TableRow);