import {type Control, type FieldValues, useWatch} from "react-hook-form";
import {useEffect} from "react";

type FormObserverProps<T> = {
    control: Control<any>;
    name?: string;
    onChange: (data: T) => void;
}

const FormObserver = <T extends FieldValues, >(props: FormObserverProps<T>) => {
    const values = useWatch({
        control: props.control,
        ...(props.name && {name: props.name}),
    });

    useEffect(() => {
        if (values) {
            const timer = setTimeout(() => props.onChange(values), 1000);
            return () => {
                clearTimeout(timer);
            };
        }
    }, [values, props.onChange]);

    return null;
};

export default FormObserver;