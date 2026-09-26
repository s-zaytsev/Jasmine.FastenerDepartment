import type {StateBase} from "../../shared/models/models.ts";

export interface RecipientsPageState extends StateBase {
    recipients: Recipient[];
    selected?: Recipient;
}

export interface Recipient {
    id: string;
    name: string;
    email: string;
}

export interface ChangeRecipient {
    name: string;
    email: string;
}