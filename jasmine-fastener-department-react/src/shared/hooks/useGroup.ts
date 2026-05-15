import type {GroupOptions} from "../models/models.ts";

const useGroup = () => {
    function groupBy<T, K extends keyof any>(
        array: T[],
        keySelector: (item: T) => K,
        options: GroupOptions<T> = {}
    ): Record<K, T[]> {
        const grouped = array.reduce((acc, item) => {
            const key = keySelector(item);
            if (!acc[key]) {
                acc[key] = [];
            }
            acc[key].push(item);
            return acc;
        }, {} as Record<K, T[]>);

        if (options.sortFn) {
            Object.keys(grouped).forEach(key => {
                const group = grouped[key as K];
                group.sort(options.sortFn);
            });
        }

        if (options.sortGroups) {
            const sortedEntries = Object.entries(grouped).sort(([keyA], [keyB]) =>
                String(keyA).localeCompare(String(keyB))
            );
            return Object.fromEntries(sortedEntries) as Record<K, T[]>;
        }

        return grouped;
    }

    return {
        groupBy
    }
}

export default useGroup;