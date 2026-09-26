import type {Recipient} from "../../models/recipientModels.ts";
import Card from "../../../shared/components/Card.tsx";
import {Box} from "@mui/material";
import IconBox from "../../../shared/components/IconBox.tsx";
import {Edit, PermIdentityOutlined} from "@mui/icons-material";
import IconButton from "../../../shared/components/buttons/IconButton.tsx";
import Typography from "../../../shared/components/Typography.tsx";
import {memo} from "react";

type RecipientsGridCardProps = {
    recipient: Recipient;
    onEdit: (id: string) => void;
}

const RecipientsGridCard = (props: RecipientsGridCardProps) => {
    return (
        <Card>
            <Box className={'w-full flex flex-col gap-[1rem]'}>
                <Box className={'w-full flex justify-between items-center'}>
                    <IconBox>
                        <PermIdentityOutlined/>
                    </IconBox>

                    <IconButton
                        description={'Редактировать'}
                        onClick={() => props.onEdit(props.recipient.id)}
                    >
                        <Edit/>
                    </IconButton>
                </Box>

                <Box className={'w-full'}>
                    <Typography variant={'headlineH2'}>{props.recipient.name}</Typography>
                </Box>

                <Box className={'w-full flex flex-col gap-[0.5rem]'}>
                    <Box className={'w-full flex justify-between items-center'}>
                        <Typography variant={'bodySmall'} color={'tertiary'}>Электронная почта</Typography>
                        <Typography variant={'bodySmall'}>{props.recipient.email}</Typography>
                    </Box>
                </Box>
            </Box>
        </Card>
    );
}

export default memo(RecipientsGridCard);