import {Box} from "@mui/material";
import type {Recipient} from "../../../models/recipientModels.ts";
import Card from "../../../../shared/components/Card.tsx";
import Typography from "../../../../shared/components/Typography.tsx";
import IconBox from "../../../../shared/components/IconBox.tsx";
import {PermIdentityOutlined} from "@mui/icons-material";
import {memo} from "react";
import RecipientsCardItem from "./RecipientsCardItem.tsx";

type RecipientsCardProps = {
    recipients: Recipient[];
    selectedRecipient?: Recipient;
    onSelectRecipient: (id: string) => void;
}

const RecipientsCard = (props: RecipientsCardProps) => {
    return (
        <Card>
            <Box className={'w-full flex flex-col gap-[1rem]'}>
                <Box className={'flex items-center gap-[0.5rem]'}>
                    <IconBox>
                        <PermIdentityOutlined/>
                    </IconBox>

                    <Box className={'flex flex-col gap-[0.2rem]'}>
                        <Typography variant={'headlineH3'}>Контакты</Typography>
                        <Typography variant={'labelSmall'} color={'tertiary'}>
                            Выберите контакт, которому будет отправлен бланк заказа
                        </Typography>
                    </Box>
                </Box>

                <Box className={'flex flex-col gap-[0.5rem]'}>
                    {props.recipients.map(recipient => (
                        <RecipientsCardItem
                            key={recipient.id}
                            recipient={recipient}
                            isActive={props.selectedRecipient?.id === recipient.id}
                            onChange={props.onSelectRecipient}
                        />
                    ))}
                </Box>
            </Box>
        </Card>
    )
}

export default memo(RecipientsCard);