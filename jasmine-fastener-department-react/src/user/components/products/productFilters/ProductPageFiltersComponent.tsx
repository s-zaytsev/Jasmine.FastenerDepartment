import {PriceTagCode, type ProductPageFilters} from "../../../models/productModel.ts";
import MultiFilterComponent from "../../../../shared/components/filters/MultiFilterComponent.tsx";
import {Box} from "@mui/material";
import SingleFilterComponent from "../../../../shared/components/filters/SingleFilterComponent.tsx";
import PriceRangeFilterComponent from "../../../../shared/components/filters/PriceRangeFilterComponent.tsx";
import Card from "../../../../shared/components/Card.tsx";
import ProductsSearch from "../ProductsSearch.tsx";
import GroupFilter from "../../../../shared/components/filters/GroupFilter.tsx";

type ProductPageFiltersProps = {
    searchValue: string;
    filters: ProductPageFilters;
    onSearch: (value: string) => void;
    onSuppliersChange: (id: string, isEnabled: boolean) => void;
    onPriceTagsChange: (id: PriceTagCode, isEnabled: boolean) => void;
    onTypeChange: (id: string, isEnabled: boolean) => void;
    onOnlyToPrintChange: (isEnabled: boolean) => void;
    onOnlyToOrderChange: (isEnabled: boolean) => void;
    onPriceRangeChange: (from: number, to: number) => void;
    resetSuppliers: () => void;
    resetPriceTags: () => void;
    resetTypes: () => void;
    resetOnlyToPrint: () => void;
    resetOnlyToOrder: () => void;
    resetPriceRange: () => void;
}

const ProductPageFiltersComponent = (props: ProductPageFiltersProps) => {
    return (
        <Card>
            <Box className={'w-full h-full flex items-center'}>
                <Box className={'w-full'}>
                    <Box className={'w-full flex justify-around items-center mb-[1rem] gap-[1rem]'}>
                        <ProductsSearch value={props.searchValue} onSearch={props.onSearch}/>
                        <GroupFilter filter={props.filters.priceTags} prefix={'Размер'} onChange={props.onPriceTagsChange}/>
                    </Box>

                    <Box className={'flex justify-between gap-[1rem]'}>
                        <MultiFilterComponent
                            title={'Поставщики'}
                            filter={props.filters?.suppliers}
                            onChange={props.onSuppliersChange}
                        />

                        <MultiFilterComponent
                            title={'Тип товара'}
                            filter={props.filters?.types}
                            onChange={props.onTypeChange}
                        />

                        {props.filters.priceRange && <PriceRangeFilterComponent
                            title={'Цена'}
                            filter={props.filters?.priceRange}
                            onChange={props.onPriceRangeChange}
                        />}
                    </Box>
                </Box>

                <Box className={'h-full flex flex-col justify-around min-w-[15rem]'}>
                    <SingleFilterComponent
                        title={'В списке печати'}
                        filter={props.filters.onlyToPrint}
                        onChange={props.onOnlyToPrintChange}
                    />

                    <SingleFilterComponent
                        title={'В списке заказа'}
                        filter={props.filters.onlyToOrder}
                        onChange={props.onOnlyToOrderChange}
                    />
                </Box>
            </Box>
        </Card>

    )
}

export default ProductPageFiltersComponent;