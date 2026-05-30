import ElementsGroup from "../../../shared/components/elementsGroup/ElementsGroup.tsx";
import ElementsGroupItem from "../../../shared/components/elementsGroup/ElementsGroupItem.tsx";
import SettingsCard from "./SettingsCard.tsx";
import {LanguageCode, stringEnumToArray} from "../../../shared/models/models.ts";
import {LanguageOutlined} from "@mui/icons-material";

type LanguageSettingsCardProps = {
    currentLanguage: LanguageCode;
    onChange: (languageCode: LanguageCode) => void;
}

const LanguageSettingsCard = (props: LanguageSettingsCardProps) => {
    return (
        <SettingsCard title={'Язык'} icon={<LanguageOutlined/>}>
            <ElementsGroup>
                {stringEnumToArray<LanguageCode>(LanguageCode).map(key => {
                    const language = LanguageCode[key] as LanguageCode;
                    return (
                        <ElementsGroupItem
                            key={key}
                            id={key}
                            title={language ?? ''}
                            isChecked={props.currentLanguage === key}
                            onChange={() => props.onChange(key)}
                        />
                    );
                })
                }
            </ElementsGroup>
        </SettingsCard>
    )
}

export default LanguageSettingsCard;