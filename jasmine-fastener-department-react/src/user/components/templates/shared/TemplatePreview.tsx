import {semanticColors} from "../../../../assets/variables/semanticColors.ts";
import {memo} from "react";

type TemplatePreviewProps = {
    template: string;
}

const TemplatePreview = (props: TemplatePreviewProps) => {
    return (
        <iframe
            key={props.template.length}
            srcDoc={props.template}
            title="Document preview"
            style={{
                width: '100%',
                height: '100%',
                padding: '1rem',
                border: 'none',
                backgroundColor: semanticColors.surface.white
            }}
        />
    );
}

export default memo(TemplatePreview);