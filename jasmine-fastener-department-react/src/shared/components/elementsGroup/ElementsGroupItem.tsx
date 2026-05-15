import {Box} from "@mui/material";
import {primitives} from "../../../assets/variables/primitives.ts";
import {semanticColors} from "../../../assets/variables/semanticColors.ts";
import Typography from "../Typography.tsx";

type ElementsGroupItemProps<T> = {
    id: T;
    title: string;
    isChecked: boolean;
    onChange: (value: T) => void;
}

const ElementsGroupItem = <T, >(props: ElementsGroupItemProps<T>) => {
    return (
        <Box
            onClick={() => props.onChange(props.id)}
            sx={{
                backgroundColor: props.isChecked ? primitives.colors.tonal : '',
                border: `1px solid ${props.isChecked ? primitives.colors.primary : semanticColors.text.secondary}`,
                borderRadius: primitives.border.radius,
                padding: '0.5rem 1rem',
                cursor: 'pointer',
                whiteSpace: 'nowrap'
            }}>
            <Typography
                variant={props.isChecked ? 'bodyRegularBold' : 'bodyRegular'}
                color={props.isChecked ? 'primary' : ''}>
                {props.title}
            </Typography>
        </Box>
    )
}

export default ElementsGroupItem;