import {type AxiosResponse} from "axios";

export const downloadService = {
    downloadFile(response: AxiosResponse<Blob, object>) {
        const disposition = response.headers['content-disposition'];
        let filename = "file.bin";
        if (disposition) {
            let match = disposition.match(/filename\*=UTF-8''([^;]+)/i);
            if (match) {
                filename = decodeURIComponent(match[1]);
            } else {
                match = disposition.match(/filename="?([^"]+)"?/);
                if (match) filename = match[1];
            }
        }

        const blob = response.data;
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement("a");
        a.href = url;
        a.download = filename;
        document.body.appendChild(a);
        a.click();
        a.remove();
        window.URL.revokeObjectURL(url);
    }
}