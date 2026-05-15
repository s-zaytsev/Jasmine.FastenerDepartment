import {Box, type SxProps} from "@mui/material";
import type {ReactNode} from "react";
import {textStyles} from "../../assets/variables/textStyles.ts";
import {semanticColors} from "../../assets/variables/semanticColors.ts";
import {primitives} from "../../assets/variables/primitives.ts";

export type TypographyVariant =
    | "displayLarge"
    | "displayRegular"
    | "displaySmall"
    | "headlineH1"
    | "headlineH2"
    | "headlineH3"
    | "bodyRegular"
    | "bodyRegularBold"
    | "bodySmall"
    | "bodySmallBold"
    | "labelRegular"
    | "labelRegularBold"
    | "labelSmall"
    | "labelSmallBold";

export type TypographyColor =
    | "primary"
    | "secondary"
    | "tertiary"
    | "warning"
    | "error";

const fontSizes = {
    "displayLarge": textStyles.desktop.display.large,
    "displayRegular": textStyles.desktop.display.regular,
    "displaySmall": textStyles.desktop.display.small,
    "headlineH1": textStyles.desktop.headline.h1,
    "headlineH2": textStyles.desktop.headline.h2,
    "headlineH3": textStyles.desktop.headline.h3,
    "bodyRegular": textStyles.desktop.body.regular,
    "bodyRegularBold": textStyles.desktop.body.regularBold,
    "bodySmall": textStyles.desktop.body.small,
    "bodySmallBold": textStyles.desktop.body.smallBold,
    "labelRegular": textStyles.desktop.label.regular,
    "labelRegularBold": textStyles.desktop.label.regularBold,
    "labelSmall": textStyles.desktop.label.small,
    "labelSmallBold": textStyles.desktop.label.smallBold
};

const colors = {
    primary: primitives.colors.primary,
    secondary: primitives.colors.tonal,
    tertiary: semanticColors.text.secondary,
    warning: semanticColors.warning.primary,
    error: semanticColors.error.primary
};

export interface TypographyProps {
    variant?: TypographyVariant;
    color?: TypographyColor | (string & {});
    children?: ReactNode;
    sx?: SxProps;
}

const Typography = ({ variant, color, children, sx }: TypographyProps) => {
    const fontSize = fontSizes[variant || "bodyRegular"] || textStyles.desktop.body.regular;
    const textColor = color && color in colors ? colors[color as TypographyColor] : color;

    return (
        <Box sx={{ ...fontSize, color: textColor, ...sx }}>
            {children}
        </Box>
    );
};

export default Typography;