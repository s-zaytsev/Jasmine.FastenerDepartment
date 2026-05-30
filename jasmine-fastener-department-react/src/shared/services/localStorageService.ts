import {LanguageCode} from "../models/models.ts";

const printItemCountsKey = 'itemCounts';
const languageCodeKey = "languageCode";

export type ItemCounts = Record<string, number>;

export const localStorageService = {
    getItemCounts: (): ItemCounts => {
        try {
            const counts = localStorage.getItem(printItemCountsKey);
            return counts ? JSON.parse(counts) : {};
        } catch (error) {
            console.error('Error reading from localStorage:', error);
            return {};
        }
    },

    getItemCount: (productId: string): number => {
        const counts = localStorageService.getItemCounts();
        return counts[productId] || 1;
    },

    setItemCount: (productId: string, count: number): void => {
        const counts = localStorageService.getItemCounts();
        counts[productId] = count;
        localStorage.setItem(printItemCountsKey, JSON.stringify(counts));
    },

    removeItemCount: (productId: string): void => {
        const counts = localStorageService.getItemCounts();
        delete counts[productId];
        localStorage.setItem(printItemCountsKey, JSON.stringify(counts));
    },

    clearAllCounts: (): void => {
        localStorage.removeItem(printItemCountsKey);
    },

    getLanguage: () => {
        return localStorage.getItem(languageCodeKey) as LanguageCode ?? LanguageCode.en;
    },

    setLanguage: (code: LanguageCode) => {
        localStorage.setItem(languageCodeKey, code);
    }
};