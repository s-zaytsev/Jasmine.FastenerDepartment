import {Box, Pagination} from "@mui/material";
import TablePageSizeSelect from "./TablePageSizeSelect.tsx";
import Typography from "../Typography.tsx";

type TablePaginationProps = {
    pageNo: number;
    pageSize: number;
    totalCount?: number;
    variant?: "default" | "bottom";
    className?: string;
    onPageNoChange?: (pageNo: number) => void;
    onPageSizeChange?: (pageSize: number) => void;
};

const TablePagination = (props: TablePaginationProps) => {
    const PageSelect = () => {
        return (
            <Pagination
                variant="text"
                shape="rounded"
                count={totalPages || 1}
                page={props.pageNo}
                onChange={(_event, pageNo) => handlePageNoChange(pageNo)}
            />
        );
    };

    const ResultsIndicator = () => {
        if (!props.totalCount) {
            return (<></>);
        }

        const firstItemNo = props.pageSize * (props.pageNo - 1) + 1;
        const lastItemNo = Math.min(props.pageSize * props.pageNo, props.totalCount);

        return (
            <Typography variant={'bodyRegular'}>
                Товары {firstItemNo}-{lastItemNo} из {props.totalCount}
            </Typography>
        );
    };

    const handlePageNoChange = (pageNo: number) => {
        if (props.onPageNoChange) {
            props.onPageNoChange(pageNo);
        }
    };

    const handlePageSizeChange = (pageSize: number) => {
        if (props.onPageSizeChange) {
            props.onPageSizeChange(pageSize);
        }
    };

    const totalPages = Math.ceil((props.totalCount || 0) / props.pageSize);


    const bottomContainerStyle = {
        borderTop: `1px solid #FBFCFC`,
        backgroundColor: "#F6F6F7",
        padding: "0.75rem 1rem"
    };

    return (
        <Box
            style={{
                display: "flex",
                justifyContent: "space-between",
                alignItems: "center",
                backgroundColor: 'white',
                padding: '10px',
                borderRadius: '0 0 4px 4px'
            }}
            sx={props.variant === "bottom" ? bottomContainerStyle : {}}
        >
            <Box>
                <TablePageSizeSelect value={props.pageSize} onChange={handlePageSizeChange}/>
            </Box>
            <Box>
                <ResultsIndicator/>
            </Box>
            <PageSelect/>
        </Box>
    );
};

export default TablePagination;