import { useEffect, useState } from "react";
import { Button, TextField } from "@mui/material";
import { User } from "../../models/User";
import { login, registration } from "../../services/RequestService";
import styles from "./Auth.module.css";


interface AuthProps {
    setIsAuthorized: React.Dispatch<React.SetStateAction<boolean>>
}

const Auth: React.FC<AuthProps> = ({ setIsAuthorized }) => {
    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');

    useEffect(() => {
        if (localStorage.getItem('token') != null) {
            setIsAuthorized(true);
        }
      }, []);    

    const invokeLogin = async () => {
        const user: User = {
            userName: userName,
            password: password
        }

        const response = await login(user);
        if (response.isAuthorized) {
            localStorage.setItem('token', response.token);
            setIsAuthorized(true);
        }
    }

    const invokeRegister = async () => {
        const user: User = {
            userName: userName,
            password: password
        }

        const response = await registration(user);
        if (response.isAuthorized) {
            localStorage.setItem('token', response.token);
            setIsAuthorized(true);
        }
    }

    return (
        <>
            {localStorage.getItem('token') == null
                &&
                <div className={styles.conteier}>
                    <div>
                        <TextField
                            id="standard-basic"
                            label="Имя"
                            variant="standard"
                            value={userName}
                            onChange={(e) => setUserName(e.target.value)} />
                        <TextField
                            id="standard-basic"
                            label="Пароль"
                            variant="standard"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)} />
                    </div>

                    <div>
                        <Button color="secondary" variant="contained" onClick={invokeLogin}>Вход</Button>
                        <Button color="secondary" variant="contained" onClick={invokeRegister}>Регистрация</Button>
                    </div>
                </div>
            }
        </>
    )
}

export default Auth