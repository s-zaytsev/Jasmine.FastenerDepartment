class ApplicationExpressions {
    hardwareMultiSizeRegex: string = '(\\s+[А-ЯA-Z]+\\d+)?(\\s+\\d+[,./-]?\\d*[A-Za-zА-Яа-я]*)(\\s*[хХxX]\\s*\\d+[,./-]?\\d*[A-Za-zА-Я-а-я]*)+';
    hardwareSingleSizeRegex: string = "(\\s+\\d+[,./-]?\\d*[A-Za-zА-Яа-я]+\\s?)";

    hasHardwareSize(name: string) {
        let result = new RegExp(this.hardwareMultiSizeRegex).test(name);
        if (!result) {
            result = new RegExp(this.hardwareSingleSizeRegex).test(name)
        }
        return result;
    }

    getHardwareSize(name: string) {
        let result = new RegExp(this.hardwareMultiSizeRegex).exec(name);
        if (!result) {
            result = new RegExp(this.hardwareSingleSizeRegex).exec(name)
        }
        return result;
    }
}

export default new ApplicationExpressions();


