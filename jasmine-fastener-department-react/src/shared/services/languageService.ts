import {LanguageCode} from "../models/models.ts";
import {localStorageService} from "./localStorageService.ts";

const LanguageService = {
    getLanguage: () => {
        return localStorageService.getLanguage();
    },

    setLanguage: (code: LanguageCode) => {
        localStorageService.setLanguage(code);
    }
}

export default LanguageService;