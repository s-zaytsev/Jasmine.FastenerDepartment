import {StrictMode} from 'react'
import {createRoot} from 'react-dom/client'
import '../index.css'
import {createTheme} from "@mui/material/styles";
import {BrowserRouter} from "react-router-dom";
import {ThemeProvider} from "@mui/material";
import NotificationProvider from "../shared/providers/NotificationProvider.tsx";
import {typography} from "../assets/variables/typography.ts";
import {QueryClient, QueryClientProvider} from "@tanstack/react-query";
import {ReactQueryDevtools} from "@tanstack/react-query-devtools";
import {store} from "../core/store.ts";
import {Provider} from "react-redux";
import AppRoutes from "./Routes.tsx";

declare module '@mui/material/styles/createPalette' {
    interface SimplePaletteColorOptions {
        defaultLines?: string;
        defaultMain?: string;
        focusedLines?: string;
        focusedMain?: string;
        hoverLines?: string;
        hoverMain?: string;
        lines?: string;
    }

    interface TypeText {
        active?: string;
        default?: string;
        focused?: string;
        hover?: string;
    }
}

const theme = createTheme({
    components: {},
    palette: {
        primary: {
            defaultLines: '#8B8198',
            defaultMain: 'transparent',
            focusedLines: '#F4EDFD', //'#E6D6FA',
            focusedMain: '#B688F2', //'#B688F2', //'#B688F2',
            hoverLines: '#8638E9', //'#8638E9',
            hoverMain: '#E6D6FA', //'#F4EDFD',
            lines: '#F7F7F8',
            main: '#8638E9',
        },
        text: {
            active: '#4D119C',
            default: '#232027',
            focused: '#8638E9', //'#8638E9',
            hover: '#B688F2',
        },
    },
    shadows: [
        'none',
        'none',
        'none',
        'none',
        'none',
        'none',
        'none',
        'none',
        'none',
        'none',
        'none',
        'none',
        'none',
        'none',
        'none',
        'none',
        'none',
        'none',
        'none',
        'none',
        'none',
        'none',
        'none',
        'none',
        'none',
    ],
    shape: {
        borderRadius: 4,
    },
    typography: {
        fontFamily: typography.family
    },
});

const queryClient = new QueryClient();

createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <Provider store={store}>
            <QueryClientProvider client={queryClient}>
                <ThemeProvider theme={theme}>
                    <NotificationProvider>
                        <BrowserRouter future={{
                            v7_startTransition: true,
                        }}>
                            <AppRoutes />
                        </BrowserRouter>
                        <ReactQueryDevtools initialIsOpen={false}/>
                    </NotificationProvider>
                </ThemeProvider>
            </QueryClientProvider>
        </Provider>
    </StrictMode>
);