import type {ReactNode} from "react";
import {Box} from "@mui/material";
import {primitives} from "../../assets/variables/primitives.ts";

type IconBoxProps = {
    children: ReactNode;
}

const IconBox = (props: IconBoxProps) => {
    return (
        <Box sx={{
            display: 'flex',
            backgroundColor: primitives.colors.tonal,
            padding: '0.5rem',
            color: primitives.colors.primary,
            borderRadius: primitives.border.radius
        }}>
            {props.children}
        </Box>
    )
}

export default IconBox;