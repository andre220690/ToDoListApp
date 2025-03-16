import { createTheme } from "@mui/material/styles";

const ThemeService = () => {
  const getButtonTheme = () => {
    return createTheme({
      palette: {
        primary: {
          main: '#e01215',
        },
        secondary: {
          main: '#faf616',
        },
        success: {
          main: '#1adb44',
        }
      },
    });
  };

  const getButtonColor = (statusId: number) => {
    switch (statusId) {
      case 1:
        return "primary";
      case 2:
        return "secondary";
      case 3:
        return "success";
      default:
        return "secondary";
    }
  };

  return { getButtonTheme, getButtonColor };
}

export default ThemeService;