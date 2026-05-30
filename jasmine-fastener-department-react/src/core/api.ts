import Axios from 'axios';
// eslint-disable-next-line @typescript-eslint/ban-ts-comment
// @ts-expect-error
import qs from 'qs';
import languageService from "../shared/services/languageService.ts";

const api = Axios.create({
    baseURL: (window as any).app.config.apiBaseUrl,
    headers: {'Content-Type': 'application/json'},
    paramsSerializer: params => qs.stringify(params, {arrayFormat: 'repeat'})
});

api.interceptors.request.use((config) => {
    config.headers['Accept-Language'] = languageService.getLanguage();
    return config;
});

export default api;
