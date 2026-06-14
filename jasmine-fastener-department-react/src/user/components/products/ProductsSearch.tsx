import {type BaseSyntheticEvent, memo, useEffect, useState} from "react";
import {Box, TextField} from "@mui/material";

export interface ProductsSearchProps {
    value: string;
    onSearch: (value: string) => void;
    placeholder?: string;
}

const ProductsSearch = (props: ProductsSearchProps) => {
    const [searchText, setSearchText] = useState<string>(props.value);

    const handleSearchTextChange = (event: BaseSyntheticEvent) => {
        const text = event.target.value;
        setSearchText(text);
    };

    useEffect(() => {
        const timerId = setTimeout(() => {
            props.onSearch(searchText);
        }, 500);
        return () => {
            clearTimeout(timerId);
        };
    }, [searchText]);

    return (
        <Box className={'w-full'}>
            <TextField
                fullWidth
                placeholder={"Начните вводить артикул или наименование..."}
                autoComplete={'off'}
                value={searchText}
                onChange={handleSearchTextChange}
            />
        </Box>
    );
};

export default memo(ProductsSearch);