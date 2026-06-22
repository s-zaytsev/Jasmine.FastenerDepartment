import {createContext, type ReactNode, useCallback, useContext, useState} from "react";
import Toast, {type ToastSeverity} from "../components/Toast.tsx";

export interface NotificationProps {
    message?: string;
    severity?: ToastSeverity;
    autoHideDuration?: number | null;
}

export interface NotificationProviderProps {
    children: ReactNode;
}

let notifyRef: (props: NotificationProps) => void = () => {};

export const apiNotify = (severity: ToastSeverity, message: string) => {
    notifyRef({ message, severity });
};

const NotificationContext = createContext<((props: NotificationProps) => void)>(() => { });

export const useNotify = () => {
    const context = useContext(NotificationContext);

    if (!context) {
        throw new Error("Component must be wrapped in a NotificationProvider.");
    }

    const notify = useCallback((severity: ToastSeverity, message: string) => {
        context({ message, severity });
    }, [context]);

    const notifySuccess = useCallback((message: string) => {
        notify("success", message);
    }, [notify]);

    const notifyError = useCallback((message: string) => {
        notify("error", message);
    }, [notify]);

    return { notify, notifySuccess, notifyError };
};

const NotificationProvider = (props: NotificationProviderProps) => {
    const [open, setOpen] = useState(false);
    const [message, setMessage] = useState("");
    const [severity, setSeverity] = useState<ToastSeverity>("info");

    const handleShowNotification = useCallback((props: NotificationProps) => {
        setMessage(props.message || "");
        setSeverity(props.severity || "info");
        setOpen(true);
    }, []);

    notifyRef = handleShowNotification;

    const handleClose = useCallback(() => {
        setOpen(false);
    }, []);

    return (
        <NotificationContext.Provider value={handleShowNotification}>
            {props.children}

            <Toast
                message={message}
                severity={severity}
                autoHideDuration={5000}
                open={open}
                onClose={handleClose}
            />
        </NotificationContext.Provider>
    );
};

export default NotificationProvider;