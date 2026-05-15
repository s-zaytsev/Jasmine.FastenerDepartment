import { SvgIcon } from "@mui/material";

const SortIcon = (props: any) => {
  return (
    <SvgIcon {...props}>
      <svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
        <path d="M15 8H5L10 3L15 8Z" fill="#C7C9CD" stroke="#C7C9CD" strokeLinejoin="round" />
        <path d="M15 12H5L10 17L15 12Z" fill="#333333" stroke="#333333" strokeLinejoin="round" />
      </svg>
    </SvgIcon>
  );
}

export default SortIcon;