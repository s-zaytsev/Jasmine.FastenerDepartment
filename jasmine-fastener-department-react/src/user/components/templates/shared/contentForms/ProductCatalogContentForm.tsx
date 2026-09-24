import {type Control, Controller, type FieldErrors} from "react-hook-form";
import {
    type ChangeTemplate,
    type ContentTableColumn,
    type ProductCatalogTemplateContent,
    type TemplateContentTableColumn
} from "../../../../models/templateModels.ts";
import {Box, Checkbox, FormControl, FormControlLabel} from "@mui/material";
import ContentTable from "../ContentTable.tsx";
import {memo} from "react";

type CatalogContentFormProps = {
    control: Control<ChangeTemplate, unknown, ChangeTemplate>;
    errors: FieldErrors<ChangeTemplate>;
    columns: TemplateContentTableColumn[];
    onChange: () => void;
}

const ProductCatalogContentForm = (props: CatalogContentFormProps) => {
    const catalogControl = props.control as unknown as Control<{
        content: ProductCatalogTemplateContent;
    }>;

    return (
        <Box className="flex flex-col gap-[1rem]">
            <Controller
                name="content.groupByType"
                control={catalogControl}
                render={({field}) => (
                    <FormControlLabel
                        control={
                            <Checkbox
                                checked={!!field.value}
                                onChange={(e) => {
                                    field.onChange(e.target.checked);
                                    props.onChange();
                                }}
                            />
                        }
                        label="Группировать по типу"
                    />
                )}
            />

            <Controller
                name="content.tableColumns"
                control={catalogControl}
                render={({field}) => {
                    const currentColumns = field.value || [];
                    const handleToggleColumn = (code: ContentTableColumn) => {
                        const exists = currentColumns.map(x => x.code).includes(code);
                        let updatedCodes: TemplateContentTableColumn[];

                        if (exists)
                            updatedCodes = currentColumns.filter(c => c.code !== code);
                        else
                            updatedCodes = [...currentColumns, props.columns.find(x => x.code === code)!];

                        field.onChange(updatedCodes);
                        props.onChange();
                    }
                    return (
                        <FormControl fullWidth margin="normal">
                            <ContentTable
                                columns={props.columns}
                                usedColumns={currentColumns.map(x => x.code)}
                                onChange={handleToggleColumn}
                            />
                        </FormControl>)
                }}
            />
        </Box>
    );
};

export default memo(ProductCatalogContentForm);