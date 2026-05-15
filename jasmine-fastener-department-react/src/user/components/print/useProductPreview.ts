import {useReactToPrint} from "react-to-print";
import {useRef} from "react";
import type {ProductToPrint, ProductToPrintRow, ProductToPrintRowItem} from "../../models/printModels.ts";
import {PriceTagCode} from "../../models/productModel.ts";

const useProductPreview = () => {

    const printRef = useRef<HTMLDivElement>(null);

    const handlePrint = useReactToPrint({
        contentRef: printRef,
        pageStyle: `
          @page { size: auto;}
          @media print {
            body {
                -webkit-print-color-adjust: exact;
                margin: 0 auto;
              }
          }`,
        documentTitle: 'Жасмин. Отдел крепежа. Ценники'
    });

    const getRowHeight = (priceTagCode: PriceTagCode) => {
        switch (priceTagCode) {
            case PriceTagCode.s:
                return 76;
            case PriceTagCode.l:
                return 76;
            case PriceTagCode.m:
                return 152;
            case PriceTagCode.xl:
                return 211;

            default:
                return 0;
        }
    }

    const getPriceTagWidth = (priceTagCode: PriceTagCode) => {
        switch (priceTagCode) {
            case PriceTagCode.s:
                return 100.8;
            case PriceTagCode.l:
                return 211.8;
            case PriceTagCode.m:
                return 156;
            case PriceTagCode.xl:
                return 156;
            default:
                return 0;
        }
    }

    const template = (productsToPrint: ProductToPrint[]): ProductToPrintRow[] => {
        let products: ProductToPrintRowItem[] = [];

        if (productsToPrint.length === 0) {
            return [];
        }

        let rows: ProductToPrintRow[] = [];
        let count = 1000;

        for (let i = 0; i < productsToPrint.length; i++) {
            for (let j = 0; j < productsToPrint[i].count; j++) {
                products = [...products, {id: (i * 10) + (j + 1), product: productsToPrint[i].product}];
            }
        }

        let height = 0;

        while (products.length > 0) {
            if (height > 1000) {
                const temp = rows[rows.length - 1];
                rows[rows.length - 1] = {id: ++count, products: [], height: 0, isSorted: true};
                rows = [...rows, temp];
                height = rows[rows.length - 1].height;
            }

            const s = products.filter(x => x.product.priceTagCode === PriceTagCode.s);
            const l = products.filter(x => x.product.priceTagCode === PriceTagCode.l);
            const m = products.filter(x => x.product.priceTagCode === PriceTagCode.m);
            const xl = products.filter(x => x.product.priceTagCode === PriceTagCode.xl);

            if (xl.length >= 4) {
                const row: ProductToPrintRow = {
                    id: ++count,
                    products: xl.slice(0, 4),
                    height: getRowHeight(PriceTagCode.xl),
                    isSorted: true
                };
                rows = [...rows, row];
                height += row.height;

                products = products.filter(x => !row.products.includes(x));
                continue;
            }

            if (s.length >= 6) {
                const row: ProductToPrintRow = {
                    id: ++count,
                    products: s.slice(0, 6),
                    height: getRowHeight(PriceTagCode.s),
                    isSorted: true
                };
                rows = [...rows, row];
                height += row.height;

                products = products.filter(x => !row.products.includes(x));
                continue;
            }

            if (l.length >= 3) {
                const row: ProductToPrintRow = {
                    id: ++count,
                    products: l.slice(0, 3),
                    height: getRowHeight(PriceTagCode.l),
                    isSorted: true
                };
                rows = [...rows, row];
                height += row.height;

                products = products.filter(x => !row.products.includes(x));
                continue;
            }

            if (m.length >= 4) {
                const row: ProductToPrintRow = {
                    id: ++count,
                    products: m.slice(0, 4),
                    height: getRowHeight(PriceTagCode.m),
                    isSorted: true
                };
                rows = [...rows, row];
                height += row.height;

                products = products.filter(x => !row.products.includes(x));
                continue;
            }

            if (s.length >= 4 && l.length >= 1) {
                let items = s.slice(0, 4);
                items = [...items, l.at(0)!];

                const row: ProductToPrintRow = {
                    id: ++count,
                    products: items,
                    height: getRowHeight(PriceTagCode.s),
                    isSorted: true
                };
                rows = [...rows, row];
                height += row.height;

                products = products.filter(x => !row.products.includes(x));
                continue;
            }

            if (s.length >= 2 && l.length >= 2) {
                let items = s.slice(0, 2);
                items = [...items, l.at(0)!, l.at(1)!];

                const row: ProductToPrintRow = {
                    id: ++count,
                    products: items,
                    height: getRowHeight(PriceTagCode.l),
                    isSorted: true
                };
                rows = [...rows, row];
                height += row.height;

                products = products.filter(x => !row.products.includes(x));
                continue;
            }

            const unsortedProducts = products.sort(
                (a, b) =>
                    getRowHeight(b.product.priceTagCode) - getRowHeight(a.product.priceTagCode));

            const unsortedProductRowItems: ProductToPrintRowItem[] = [];

            for (let i = 0; i < unsortedProducts.length; i++) {
                unsortedProductRowItems.push(unsortedProducts[i]);
                const totalWidth = unsortedProductRowItems
                    .map(x => Number(getPriceTagWidth(x.product.priceTagCode)))
                    .filter(num => num)
                    .reduce((sum, num) => sum + num, 0);

                if (i < unsortedProducts.length - 1 && (totalWidth + getPriceTagWidth(unsortedProducts[i + 1].product.priceTagCode)) > 670) {
                    break;
                }
            }

            const row: ProductToPrintRow = {
                id: ++count,
                products: unsortedProductRowItems,
                height: Math.max(...products.map((x) => getRowHeight(x.product.priceTagCode))),
                isSorted: false
            };
            rows = [...rows, row];
            height += row.height;

            products = products.filter(x => !row.products.includes(x));
        }

        return rows;
    }

    return {
        template,
        printRef,
        handlePrint
    }
}

export default useProductPreview;