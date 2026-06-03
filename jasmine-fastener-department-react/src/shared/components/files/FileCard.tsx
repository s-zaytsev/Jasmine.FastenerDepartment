import Card from "../Card.tsx";
import {Box} from "@mui/material";
import Typography from "../Typography.tsx";
import {semanticColors} from "../../../assets/variables/semanticColors.ts";
import IconButton from "../buttons/IconButton.tsx";
import {CloseOutlined} from "@mui/icons-material";

type FileCardProps = {
    file: File;
    onDelete: (file: File) => void;
}

const FileCard = (props: FileCardProps) => {
    return (
        <Card backgroundColor={semanticColors.surface.light}>
            <Box className={'flex justify-between items-center w-full'}>
                <Box className={'flex flex-col'}>
                    <Typography variant={'bodyRegular'} color={'primary'}>{props.file.name}</Typography>
                    <Typography variant={'labelRegular'} color={'tertiary'} sx={{marginTop: '0.5rem'}}>{props.file.size / 100} Kb</Typography>
                </Box>
                <Box>
                    <IconButton description={'Удалить из списка'} onClick={() => props.onDelete(props.file)}>
                        <CloseOutlined />
                    </IconButton>
                </Box>
            </Box>
        </Card>
    )
}

export default FileCard;