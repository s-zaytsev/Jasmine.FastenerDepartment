import type {Recipient} from "../../models/recipientModels.ts";
import {Box, Grow} from "@mui/material";
import EmptyGrid from "../../../shared/components/EmptyGrid.tsx";
import RecipientsGridCard from "./RecipientsGridCard.tsx";

type RecipientsGridProps = {
    recipients: Recipient[];
    onEdit: (id: string) => void;
}

const RecipientsGrid = (props: RecipientsGridProps) => {
    if (props.recipients.length === 0) {
        return <EmptyGrid message={'Список контактов пуст'}/>
    }

    return (
        <Box className={'flex flex-wrap gap-[1rem]'}>
            {props.recipients.map((recipient, index) =>
                <Grow key={recipient.id} in={true} timeout={index * 150}>
                    <Box className={'w-[23%]'}>
                        <RecipientsGridCard
                            recipient={recipient}
                            onEdit={props.onEdit}
                        />
                    </Box>
                </Grow>
            )}
        </Box>
    );
}

export default RecipientsGrid;