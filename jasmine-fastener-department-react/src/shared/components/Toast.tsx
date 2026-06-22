import {Alert, IconButton, Snackbar} from "@mui/material";
import Typography from "./Typography";
import {primitives} from "../../assets/variables/primitives.ts";
import {semanticColors} from "../../assets/variables/semanticColors.ts";
import InfoCircleIcon from "../../assets/icons/InfoCircleIcon.tsx";
import TickCircleIcon from "../../assets/icons/TickCircleIcon.tsx";
import WarningIcon from "../../assets/icons/WarningIcon.tsx";
import CloseCircleIcon from "../../assets/icons/CloseCircleIcon.tsx";
import CloseIcon from "../../assets/icons/CloseIcon.tsx";
import {typography} from "../../assets/variables/typography.ts";

const colorSets = {
    processing: {
        backgroundColor: semanticColors.surface.light,
        color: semanticColors.text.primary,
        iconColor: primitives.colors.primary
    },
    info: {
        backgroundColor: semanticColors.surface.light,
        color: semanticColors.text.primary,
        iconColor: primitives.colors.primary
    },
    success: {
        backgroundColor: semanticColors.success.tonal,
        color: semanticColors.success.primary,
        iconColor: semanticColors.success.primary
    },
    warning: {
        backgroundColor: semanticColors.warning.tonal,
        color: semanticColors.warning.primary,
        iconColor: semanticColors.warning.primary
    },
    error: {
        backgroundColor: semanticColors.error.tonal,
        color: semanticColors.error.primary,
        iconColor: semanticColors.error.primary
    }
};

const icons = {
    processing: <InfoCircleIcon />,
    info: <InfoCircleIcon />,
    success: <TickCircleIcon />,
    warning: <WarningIcon />,
    error: <CloseCircleIcon />
};

export type ToastSeverity = "processing" | "info" | "success" | "warning" | "error";

export interface ToastProps {
    message?: string;
    severity?: ToastSeverity;
    open?: boolean;
    autoHideDuration?: number | null;
    onClose?: () => void;
}

const Toast = ({
                   message = "",
                   open = false,
                   severity = "info",
                   autoHideDuration = 5000,
                   onClose
               }: ToastProps) => {
    const alertSeverity = severity === "processing" ? "info" : severity;
    const colorSet = colorSets[severity];
    const icon = icons[severity];

    return (
        <Snackbar
            open={open}
            autoHideDuration={autoHideDuration}
            onClose={onClose}
            anchorOrigin={{ horizontal: "right", vertical: "top" }}
            sx={{
                "&.MuiSnackbar-root.MuiSnackbar-anchorOriginBottomCenter": {
                    bottom: {
                        sm: 40
                    }
                }
            }}
        >
            <Alert
                variant="filled"
                severity={alertSeverity}
                icon={icon}
                action={
                    <IconButton size="small" onClick={onClose}>
                        <CloseIcon />
                    </IconButton>
                }
                sx={{
                    fontFamily: typography.family,
                    display: "flex",
                    alignItems: "center",
                    borderRadius: "8px",
                    padding: "12px 16px",
                    backgroundColor: colorSet.backgroundColor,
                    color: colorSet.color,
                    boxShadow: "0px 5px 10px 0px rgba(0, 0, 0, 0.15)",
                    ".MuiAlert-icon": {
                        padding: 0,
                        marginRight: "10px",
                        ".MuiSvgIcon-root": {
                            width: "24px",
                            height: "24px"
                        }
                    },
                    ".MuiAlert-message": {
                        padding: "4.2px 0"
                    },
                    ".MuiAlert-action": {
                        padding: 0,
                        margin: "0 0 0 24px",
                        ".MuiButtonBase-root": {
                            padding: 0,
                            color: colorSet.color
                        }
                    }
                }}
            >
                <Typography variant="bodySmallBold">
                    {message}
                </Typography>
            </Alert>
        </Snackbar>
    );
};

export default Toast;