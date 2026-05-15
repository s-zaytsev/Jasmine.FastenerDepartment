import {MenuItem, Select, type SelectChangeEvent} from "@mui/material";
import {ArrowDropDown} from "@mui/icons-material";

type TablePageSizeSelectProps = {
    value: number;
    onChange: (value: number) => void;
}

const TablePageSizeSelect = (props: TablePageSizeSelectProps) => {
    const handleChange = (event: SelectChangeEvent) => {
        props.onChange(Number(event.target.value));
    };

    return (
            <div className={"flex items-center"}>
                <p className={'mr-[1rem]'}>Количество на странице</p>

                <div>
                    <Select
                        style={{height: '30px'}}
                        displayEmpty
                        value={props.value.toString()}
                        onChange={handleChange}
                        className="ml-2"
                        IconComponent={ArrowDropDown}>
                        <MenuItem value={10}>10</MenuItem>
                        <MenuItem value={15}>15</MenuItem>
                        <MenuItem value={20}>20</MenuItem>
                        <MenuItem value={50}>50</MenuItem>
                    </Select>
                </div>
            </div>
    )
}

export default TablePageSizeSelect;