const STORAGE_KEY = 'itemCounts';
export type ItemCounts = Record<string, number>;

export const localStorageService = {
    getItemCounts: (): ItemCounts => {
        try {
            const counts = localStorage.getItem(STORAGE_KEY);
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
        localStorage.setItem(STORAGE_KEY, JSON.stringify(counts));
    },

    removeItemCount: (productId: string): void => {
        const counts = localStorageService.getItemCounts();
        delete counts[productId];
        localStorage.setItem(STORAGE_KEY, JSON.stringify(counts));
    },

    clearAllCounts: (): void => {
        localStorage.removeItem(STORAGE_KEY);
    }
};