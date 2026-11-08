import { styled } from '@mui/material/styles';
import Button, {type ButtonProps } from '@mui/material/Button';
import CircularProgress from '@mui/material/CircularProgress';

const StyledButton = styled(Button)(() => ({
    boxShadow: 'none',
    height: 42,
    padding: '12px 24px',
    textTransform: 'none',
    width: 240,
    '&:hover': { boxShadow: 'none' },
}));

export interface FilledButtonProps extends ButtonProps {
    loading?: boolean;
}

const FilledButton = ({loading = false, children, disabled, ...props }: FilledButtonProps) => (
    <StyledButton disabled={loading || disabled} {...props}>
        {loading ? <CircularProgress size={16} color="inherit" /> : children}
    </StyledButton>
);

export default FilledButton;