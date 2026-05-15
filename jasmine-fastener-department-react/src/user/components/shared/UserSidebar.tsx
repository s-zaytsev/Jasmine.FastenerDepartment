import Sidebar from "../../../shared/components/sidebar/Sidebar.tsx";
import {
    BusinessOutlined,
    CloudDownloadOutlined,
    DesignServicesOutlined,
    HistoryOutlined,
    ListAltOutlined,
    LocalShippingOutlined,
    PrintOutlined
} from "@mui/icons-material";

const mainItems = [
    {title: "Товары", link: "/products", icon: <ListAltOutlined/>},
    {title: "Печать", link: "/print", icon: <PrintOutlined/>},
    {title: "Заказы", link: "/orders", icon: <LocalShippingOutlined/>},
    {title: "Поставщики", link: "/suppliers", icon: <BusinessOutlined/>},
    {title: "Типы товаров", link: "/product-types", icon: <DesignServicesOutlined/>},
    {title: "Экспорт", link: "/export", icon: <CloudDownloadOutlined/>},
    {title: "История", link: "/history", icon: <HistoryOutlined/>}
];

const UserSidebar = () => {
    return (<Sidebar mainItems={mainItems}/>)
};

export default UserSidebar;