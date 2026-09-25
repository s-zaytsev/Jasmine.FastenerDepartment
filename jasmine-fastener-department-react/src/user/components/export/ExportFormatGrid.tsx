import {Box} from "@mui/material";
import ExportFormatGridItem from "./ExportFormatGridItem.tsx";
import {TemplateFormatCode} from "../../models/templateModels.ts";
import {semanticColors} from "../../../assets/variables/semanticColors.ts";

const types = [
    {code: TemplateFormatCode.word, title: 'DOCX', subtitle: 'Word'},
    {code: TemplateFormatCode.html, title: 'HTML', subtitle: 'Web'}
];

type ExportFormatGridProps = {
    selected: TemplateFormatCode;
    onChange: (code: TemplateFormatCode) => void;
}

const ExportFormatGrid = (props: ExportFormatGridProps) => {
    return (
        <Box sx={{
            display: 'flex',
            alignItems: 'center',
            backgroundColor: semanticColors.surface.medium,
            width: '100%',
        }}>
            {
                types.map(type => (
                    <ExportFormatGridItem
                        key={type.code}
                        type={type.code}
                        title={type.title}
                        subtitle={type.subtitle}
                        isActive={props.selected === type.code}
                        onChange={props.onChange}
                    />
                ))
            }
        </Box>
    )
}

export default ExportFormatGrid;