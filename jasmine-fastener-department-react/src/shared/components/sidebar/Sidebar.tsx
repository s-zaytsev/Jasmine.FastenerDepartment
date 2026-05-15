import {Box} from '@mui/material/';
import SidebarListItem, {type SidebarListItemProps} from './SidebarListItem';
import Typography from "../Typography.tsx";

export interface SidebarProps {
    mainItems?: SidebarListItemProps[];
    bottomItems?: SidebarListItemProps[];
};

const Sidebar = (props: SidebarProps) => {
    return (
        <Box className={'w-[10rem]'}>
            <Box className={'mb-[2rem]'}>
                <Typography variant={'headlineH2'} color={'primary'}>Жасмин</Typography>
                <Typography variant={'bodySmallBold'} color={'tertiary'}>Отдел крепежа</Typography>
            </Box>

            <Box className={'flex flex-col gap-[1rem]'}>
                {props.mainItems?.map((item, i) => (
                    <SidebarListItem key={i} {...item} />
                ))}
            </Box>
        </Box>
        /* <Paper
           sx={{
             backgroundColor: 'white',
             border: 0,
             padding: '20px',
             width: '104px',
           }}
         >
           <Toolbar
             sx={{
               display: 'flex',
               flexDirection: 'column',
               flexShrink: 0,
               height: '100%',
               padding: 0,
             }}
             variant="regular"
           >
             <List
               sx={{
                 flex: 1,
                 display: 'flex',
                 flexDirection: 'column',
                 justifyContent: 'space-between',
                 padding: 0,
                 gap: 2.5
               }}
             >
               <Box
                 sx={{
                   display: 'flex',
                   flexDirection: 'column',
                   gap: 2.5,
                 }}
               >
                 <SidebarLogoListItem />

                 {mainItems.map((item, i) => (
                   <SidebarListItem key={i} {...item} />
                 ))}
               </Box>

               <Box>
                 {bottomItems.map((item, i) => (
                   <SidebarListItem key={i} {...item} />
                 ))}
               </Box>
             </List>
           </Toolbar>
         </Paper>*/
    );
};

export default Sidebar;
