import Axios from 'axios';
// eslint-disable-next-line @typescript-eslint/ban-ts-comment
// @ts-expect-error
import qs from 'qs';

const api = Axios.create({
    baseURL: (window as any).app.config.apiBaseUrl,
    paramsSerializer: params => qs.stringify(params, { arrayFormat: 'repeat' })
});

export default api;
