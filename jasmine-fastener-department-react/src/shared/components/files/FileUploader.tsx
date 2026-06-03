import {type ChangeEvent, useEffect, useState} from "react";
import {Box, Button} from "@mui/material";
import FileCard from "./FileCard.tsx";

type FileUploaderProps = {
    onChange: (files: File[]) => void;
}

const FileUploader = (props: FileUploaderProps) => {
    const [files, setFiles] = useState<File[]>([]);

    const handleAddFile = (event: ChangeEvent<HTMLInputElement>) => {
        if (!event.target.files) {
            return;
        }

        setFiles([...files, ...Array.from(event.target.files)]);
    }

    const handleDeleteFile = (file: File) => {
        if (!file) return;
        setFiles(files.filter((x) => x !== file));
    }

    useEffect(() => {
        props.onChange(files);
    }, [files]);

    return (
        <Box className={'flex flex-col items-center'}>

            {files.map((file: File) =>
                <FileCard key={file.name + file.size} file={file} onDelete={handleDeleteFile}/>
            )}

            <label htmlFor="file-input">
                <Button component="span" variant="text">
                    + добавить файлы
                </Button>
            </label>

            <input
                type="file"
                multiple
                accept={'.doc, .docx, .pdf'}
                id="file-input"
                style={{display: "none"}}
                onChange={handleAddFile}
            />
        </Box>
    )
}

export default FileUploader;