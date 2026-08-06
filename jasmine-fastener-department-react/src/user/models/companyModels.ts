import type {StateBase} from "../../shared/models/models.ts";

export interface CompaniesPageState extends StateBase {
    companies: Company[];
}

export interface CreateCompanyPageState extends StateBase {
    model: ChangeCompany;
}

export interface ChangeCompanyPageState extends StateBase {
    model: ChangeCompany;
}

export interface Company {
    id: string;
    title: string;
    type: string;
}

export interface CompanyDetails {
    id: string;
    title: string;
    firstName: string;
    middleName: string;
    lastName: string;
    email: string;
    city: string;
    street: string;
    buildingNumber: string;
    inn: string;
    phoneNumber: string;
}

export interface ChangeCompany {
    title: string;
    firstName: string;
    middleName: string;
    lastName: string;
    inn: string;
    email: string;
    city: string;
    street: string;
    buildingNumber: string;
    phoneNumber: string;
}