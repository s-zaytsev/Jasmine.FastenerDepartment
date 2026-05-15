import {useRouteError} from "react-router-dom";

interface Error {
    message: string;
    statusText: string;
}

const ErrorPage = () => {
    const error = useRouteError() as Error;

    return (
        <div id="error-page">
            <h1>Oops!</h1>
            <p>Sorry, an unexpected error has occurred.</p>
            <p>
                <i>{error.statusText || error.message}</i>
            </p>
        </div>
    );
}

export default ErrorPage;
