import Card from "../../../../shared/components/Card.tsx";
import FileUploader from "../../../../shared/components/files/FileUploader.tsx";
import {Box} from "@mui/material";
import IconBox from "../../../../shared/components/IconBox.tsx";
import {AttachFileOutlined} from "@mui/icons-material";
import Typography from "../../../../shared/components/Typography.tsx";
import {memo} from "react";

type ExtraFilesCard = {
    onChangeExtraFiles: (files: File[]) => void;
}

const ExtraFilesCard = (props: ExtraFilesCard) => {
    return (
        <Card>
            <Box className={'w-full flex flex-col gap-[1rem]'}>
                <Box className={'flex items-center gap-[0.5rem]'}>
                    <IconBox>
                        <AttachFileOutlined/>
                    </IconBox>

                    <Box className={'flex flex-col gap-[0.2rem]'}>
                        <Typography variant={'headlineH3'}>Дополнительные файлы</Typography>
                        <Typography variant={'labelSmall'} color={'tertiary'}>
                            Здесь вы можете прикрепить дополнительные файлы
                        </Typography>
                    </Box>
                </Box>

                <Box className={'flex flex-col gap-[0.5rem]'}>
                    <FileUploader onChange={props.onChangeExtraFiles}/>
                </Box>
            </Box>
        </Card>
    )
}

export default memo(ExtraFilesCard);