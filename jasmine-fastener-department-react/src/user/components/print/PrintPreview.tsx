import PriceTag from "../../../shared/components/priceTags/PriceTag.tsx";
import type {ProductToPrint, ProductToPrintRow} from "../../models/printModels.ts";

type PrintReviewProps = {
    products: ProductToPrint[];
    template: ProductToPrintRow[];
}

const PrintPreview = (props: PrintReviewProps) => {

    /*    const ProductsList = () => {
            let products: ProductToPrintRowItem[] = [];

            if (props.products.length === 0) {
                return products.map((x) => <PriceTag product={x.product}/>)
            }

            let rows: ProductToPrintRow[] = [];
            let count = 0;

            for (let i = 0; i < props.products.length; i++) {
                for (let j = 0; j < props.products[i].count; j++) {
                    products = [...products, {id: i * j, product: props.products[i].product}];
                }
            }
            while (products.length > 0) {

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
                    products = products.filter(x => !row.products.includes(x));
                    continue;
                }

                if (s.length >= 2 && l.length >= 2) {
                    let items = s.slice(0, 2);
                    items = [...items, l.at(0)!, l.at(1)!];

                    const row: ProductToPrintRow = {
                        id: ++count,
                        products: items,
                        height: getRowHeight(PriceTagCode.s),
                        isSorted: true
                    };
                    rows = [...rows, row];
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
                products = products.filter(x => !row.products.includes(x));
            }

            rows = rows.sort((a, b) => {
                if (a.isSorted !== b.isSorted) {
                    return (a.isSorted ? -1 : 1);
                }
                return b.height - a.height;
            });

            let height = 0;
            let pageCutterKey = 100000000;

            const optimizedRows: JSX.Element[] = [];

            for (let i = 0; i < rows.length; i++) {
                const firstRowOnPage = height === 0;
                const rowIndex = i;
                if (optimizedRows.find(x => x.key === rows[rowIndex].id.toString())) {
                    continue;
                }

                //TODO Work out why rows disappear with this logic
                if ((height + rows[rowIndex].height) > 1000) {
                    /!*    const index = rows.findIndex(x => (x.height + height) <= 1000);
                        if (index != -1) {
                            rowIndex = index;
                        } else {*!/
                    height = 0;
                    optimizedRows.push(<div key={pageCutterKey} className="page-cutter"/>);
                    pageCutterKey++;
                    //       }
                }

                height += rows[rowIndex].height;

                optimizedRows.push(
                    <Box key={rows[rowIndex].id} className={'flex flex-wrap'}
                         sx={{height: `${rows[rowIndex].height}px`, marginTop: firstRowOnPage ? '2px' : '-1px'}}>
                        {rows[rowIndex].products.map((product, index) =>
                            <Box key={rows[rowIndex].id + Date.now() + index}>
                                <PriceTag key={Date.now() + index} product={product.product}/>
                            </Box>)}
                    </Box>
                )
            }

            return optimizedRows.map(x => x);
        }*/

    const ProductList = () => {
        return props.template.map((x) => {
            return <div className={'flex'} key={x.id}>
                {
                    x.height === 0 ?
                        <div className={'page-cutter'}></div> :
                        x.products.map((y) => {
                            return <PriceTag key={y.id} product={y.product}/>
                        })
                }
            </div>
        })
    }

    return (
        <div className={"w-full h-full"} style={{margin: '0 auto'}}>
            <div className={"print-preview-wrapper"}>
                <div className={"print-preview"}>
                    {<ProductList/>}
                </div>
            </div>
        </div>
    )
}

export default PrintPreview;