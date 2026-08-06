import api from "../../core/api.ts";
import type {ChangeCompany, Company, CompanyDetails} from "../models/companyModels.ts";

class CompaniesApi {
    getCompanies(): Promise<Company[]> {
        return api.get<Company[]>(`/companies`)
            .then(x => x.data);
    }

    getCompany(id: string): Promise<CompanyDetails> {
        return api.get<CompanyDetails>(`/companies/${id}`)
            .then(x => x.data);
    }

    changeCompany(id: string, model: ChangeCompany): Promise<void> {
        return api.put(`/companies/${id}`, model)
            .then(x => x.data);
    }

    createCompany(model: ChangeCompany): Promise<void> {
        return api.post(`/companies`, model)
            .then(x => x.data);
    }
}

export default new CompaniesApi();