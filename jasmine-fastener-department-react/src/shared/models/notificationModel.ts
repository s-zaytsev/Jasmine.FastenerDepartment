import type {AxiosError} from "axios";

class ApiError {
    static AXIOS_UNAUTHORIZED_ERROR_MESSAGE = "Request failed with status code 401";

    static DEFAULT_ERROR_MESSAGE = 'Oops, something went wrong';
    static ACCESS_DENIED_ERROR_MESSAGE = 'Access denied';
    static TOKEN_EXPIRED_ERROR_MESSAGE = 'Your session has run out, please log in again';

    message!: string;
    code?: number;

    constructor(message: string, code?: number) {
        this.message = message;
        this.code = code;
    }

    static create(error: any): ApiError {
        if (!error) {
            return new ApiError(this.DEFAULT_ERROR_MESSAGE);
        }

        if (typeof error === "string") {
            return new ApiError(error);
        }

        if (typeof error === "object" && ("detail" in error || "title" in error || "status" in error)) {
            const problem: any = error;
            const msg = problem.detail || problem.title || this.DEFAULT_ERROR_MESSAGE;
            const code = problem.status;
            return new ApiError(msg, code);
        }

        const axiosError = error as AxiosError<any>;

        if (axiosError.response) {
            if (axiosError.response.data) {
                if (typeof axiosError.response.data === "string") {
                    return new ApiError(axiosError.response.data);
                }

                const responseData = axiosError.response.data;
                const errorMessage = responseData.detail || 
                                   responseData.message || 
                                   responseData.title || 
                                   this.DEFAULT_ERROR_MESSAGE;
                
                return new ApiError(errorMessage, responseData.status || axiosError.response.status);
            }

            if (axiosError.response.status === 403) {
                return new ApiError(this.ACCESS_DENIED_ERROR_MESSAGE);
            }

            if (axiosError.response.status === 401) {
                return new ApiError(this.TOKEN_EXPIRED_ERROR_MESSAGE, 401);
            }
        }

        if (axiosError.message) {
            if (axiosError.message === this.AXIOS_UNAUTHORIZED_ERROR_MESSAGE) {
                return new ApiError(this.TOKEN_EXPIRED_ERROR_MESSAGE, 401);
            }

            return new ApiError(axiosError.message);
        }

        return new ApiError(this.DEFAULT_ERROR_MESSAGE);
    }
}

export class NotificationMessage {
    message?: string;
    errorCode?: number;

    static error(error: any): NotificationMessage {
        const apiError = ApiError.create(error);
        return { message: apiError.message, errorCode: apiError.code };
    }
}