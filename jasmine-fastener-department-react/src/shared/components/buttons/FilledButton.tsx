import { Button } from '@mui/material';
import { styled } from '@mui/system';

const FilledButton = styled(Button)(
  () => `
    '&:hover': {
        box-shadow: none;
    }

    box-shadow: none;
    height: 42px;
    padding-x: 24px;
    padding-y: 12px;
    text-transform: none;
    width: 240px;
`
);

export default FilledButton;
