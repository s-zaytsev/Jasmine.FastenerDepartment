import {Box, CircularProgress} from "@mui/material";
import Typography from "./Typography";
import {neutralColors} from "../../assets/variables/neutralColors.ts";
import {primitives} from "../../assets/variables/primitives.ts";

type LoaderProps = {
    text?: string;
}

const Loader = (props: LoaderProps) => {
    return (
        <Box
            sx={{
                width: "100%",
                height: "100%",
                display: "flex",
                flexDirection: "column",
                justifyContent: "center",
                alignItems: "center"
            }}
        >
            <Box
                sx={{
                    display: "flex",
                    flexDirection: "column",
                    justifyContent: "center",
                    alignItems: "center",
                    gap: 2.5
                }}>
                <Box
                    sx={{
                        width: "64px",
                        height: "64px",
                        display: "flex",
                        flexDirection: "column",
                        justifyContent: "center",
                        alignItems: "center",
                        borderRadius: "4px",
                        backgroundColor: primitives.colors.primary
                    }}>
                    <CircularProgress size={24} thickness={5.6} sx={{color: neutralColors.white}}/>
                </Box>

                <Typography variant="headlineH3">
                    {props.text ? props.text : 'Загрузка данных'}
                </Typography>
            </Box>
        </Box>
    );
};

export default Loader;