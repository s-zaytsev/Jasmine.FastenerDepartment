import {useState} from "react";
import {Box} from "@mui/material";
import {primitives} from "../../../assets/variables/primitives.ts";
import {semanticColors} from "../../../assets/variables/semanticColors.ts";

type ToggleAdvancedProps = {
    checked?: boolean;
    onChange?: (checked: boolean) => void;
    inactiveText: string;
    activeText: string;
}

const ToggleAdvanced = (props: ToggleAdvancedProps) => {
    const [isChecked, setIsChecked] = useState(props.checked);

    const handleToggle = () => {
        const newState = !isChecked;
        setIsChecked(newState);
        props.onChange?.(newState);
    };

    const thumbWidth = 36;
    const thumbOffset = 4;
    const trackWidth = 150;

    return (
        <Box
            sx={{
                position: 'relative',
                width: `${trackWidth}px`,
                height: '44px',
                cursor: 'pointer',
                userSelect: 'none',
            }}
            onClick={handleToggle}
        >
            <Box
                sx={{
                    position: 'absolute',
                    top: 0,
                    left: 0,
                    right: 0,
                    bottom: 0,
                    backgroundColor: isChecked ? primitives.colors.primary : semanticColors.surface.medium,
                    borderRadius: '44px',
                    transition: 'background-color 0.3s ease',
                }}
            />

            <Box
                sx={{
                    position: 'absolute',
                    top: '50%',
                    transform: 'translateY(-50%)',
                    fontSize: '12px',
                    fontWeight: 'bold',
                    color: isChecked ? 'white' : semanticColors.text.secondary,
                    whiteSpace: 'nowrap',
                    transition: 'left 0.3s ease',
                    left: isChecked ? `${thumbOffset + 8}px` : 'auto',
                    right: isChecked ? 'auto' : `${thumbOffset + 8}px`,
                    zIndex: 1,
                    pointerEvents: 'none',
                }}
            >
                {isChecked ? props.activeText : props.inactiveText}
            </Box>

            <Box
                sx={{
                    position: 'absolute',
                    top: `${thumbOffset}px`,
                    width: `${thumbWidth}px`,
                    height: `${thumbWidth}px`,
                    backgroundColor: 'white',
                    borderRadius: '50%',
                    transition: 'left 0.3s ease',
                    left: isChecked ? `calc(100% - ${thumbWidth + thumbOffset}px)` : `${thumbOffset}px`,
                    boxShadow: '0 2px 4px rgba(0,0,0,0.2)',
                    zIndex: 2,
                }}
            />
        </Box>
    );
};

export default ToggleAdvanced;