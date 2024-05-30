import * as React from 'react';
import {useEffect, useState} from 'react';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Checkbox from '@mui/material/Checkbox';
import Container from '@mui/material/Container';
import {createTheme, ThemeProvider} from '@mui/material/styles';
import {useNavigate} from "react-router-dom";
import {IconButton, InputAdornment} from '@mui/material';
import FormControlLabel from '@mui/material/FormControlLabel';
import Visibility from '@mui/icons-material/Visibility';
import VisibilityOff from '@mui/icons-material/VisibilityOff';
import myImage from './tyan2.png'
import logo from './logo4.png'


const defaultTheme = createTheme();


export default function SignUp() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [name, setName] = useState('');
    const [errorPassword, setErrorPassword] = useState('');
    const [errorEmail, setErrorEmail] = useState('');
    const [error, setError] = useState('');
    const [errorName, setErrorName] = useState('');
    const [showPassword, setShowPassword] = useState(false);
    const navigate = useNavigate();

    const theme = createTheme({
        typography: {
            fontFamily: ['Gilroy', 'Roboto', '"Helvetica Neue"', 'Arial', 'sans-serif',].join(','),
        },
    });


    const validatePassword = (password) => {
        const uppercaseRegex = /[A-Z]/;
        const digitRegex = /[0-9]/;
        const latinLettersRegex = /^[a-zA-Z0-9]+$/;


        if (password.length < 8) {
            setErrorPassword('Пароль одлжен быть не меньше 8 символов');
        } else if (password.length > 60) {
            setErrorPassword('Пароль одлжен быть не больше 60 символов');
        } else if (!latinLettersRegex.test(password)) {
            setErrorPassword('Только латинские буквы и арабские цифры');
        } else if (!uppercaseRegex.test(password)) {
            setErrorPassword('Должкен содержать хотя бы 1 заглавную букву');
        } else if (!digitRegex.test(password)) {
            setErrorPassword('Пароль должкен содержать хотя бы 1 цифру');
        } else {
            setErrorPassword('');
        }
    };

    const validateName = (name) => {
        const nameLength = name.length;
        const nameRegex = /^[a-zA-Z0-9]+$/;

        if (nameLength < 3 || nameLength > 50) {
            setErrorName('Длина от 3 до 50 символов');
        } else if (!nameRegex.test(name)) {
            setErrorName('Только латинские буквы и арабские цифры');
        } else {
            setErrorName('');
        }
    };

    const validateEmail = (email) => {
        const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        if (!emailRegex.test(email)) {
            setErrorEmail('Неправильный формат');
        } else {
            setErrorEmail('');
        }
    };

    const handleClickShowPassword = () => {
        setShowPassword(!showPassword);
    };

    const handleMouseDownPassword = (event) => {
        event.preventDefault();
    };


    //  localStorage.setItem("accessToken",data.value.accessToken);
    // localStorage.setItem("refreshToken",data.value.refreshToken);
    const singUpOnClick = () => {
        if (errorPassword !== '' || errorName !== '' || errorEmail !== '') {
            setError('Ошибка при отправке данных на сервер');
        } else {
            fetch('https://localhost:8000/auth/register', {
                method: 'POST',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    Email: email,
                    Password: password,
                    Nickname: name
                })
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Ошибка при отправке данных на сервер');
                    }
                    return response.json();
                })
                .then(data => {
                    navigate('/');
                })
                .catch(error => {
                    console.error('Ошибка при отправке данных на сервер', error);
                    setError('Ошибка при отправке данных на сервер');
                });
        }
    }


    return (<ThemeProvider theme={theme}>
        <div style={{display: 'flex', height: '100vh'}}>
            <img src={myImage} alt="tyan" style={{width: '50%', height: 'auto'}}/>
            <div style={{backgroundColor: '#161616', width: '50%', padding: '20px'}}>
                <img src={logo} alt="logo"
                     style={{width: '406px', height: '118.54px', position: 'absolute', top: '3%', left: '67%'}}/>
                <Container component="main" sx={{marginTop: '45px'}}>

                    <CssBaseline/>
                    <Box
                        sx={{
                            marginTop: 30,
                            display: 'flex',
                            flexDirection: 'column',
                            alignItems: 'center',
                            marginLeft: 40
                        }}
                    >
                        <div style={{marginLeft: '-315px'}}>
                            <Typography component="h1" variant="h5">
                                <div className="button-text" style={{
                                    fontSize: '24px',
                                    fontWeight: '700',
                                    lineHeight: '30px',
                                    color: '#FFFFFF',
                                    marginLeft: 75,
                                    marginTop: 15
                                }}>
                                    Регистрация
                                </div>
                            </Typography>
                        </div>
                        <Box noValidate sx={{mt: 2}}>
                            <Grid container spacing={4}>
                                <Grid item xs={12} sm={6}>
                                    <div className="button-text"
                                         style={{fontSize: '12px', color: '#C6C6C6', marginTop: 20}}>
                                        E-mail
                                    </div>
                                    <TextField
                                        onChange={(e) => {
                                            setEmail(e.target.value)
                                        }}
                                        onBlur={(p) => {
                                            validateEmail(p.target.value);
                                        }}
                                        required
                                        fullWidth
                                        id="email"
                                        name="email"
                                        autoComplete="email"
                                        error={Boolean(errorEmail)}
                                        helperText={errorEmail}
                                        sx={{
                                            '& .MuiInputBase-root': {
                                                width: '291px',
                                                height: '48px',
                                                color: 'white',
                                                borderRadius: '#262626',
                                                bgcolor: '#262626',
                                            }
                                        }}
                                    />
                                    <div className="button-text"
                                         style={{fontSize: '12px', color: '#C6C6C6', marginTop: 30}}>
                                        Логин
                                    </div>
                                    <TextField
                                        onChange={(e) => {
                                            setName(e.target.value)
                                        }}
                                        onBlur={(p) => {
                                            validateName(p.target.value);
                                        }}
                                        required
                                        fullWidth
                                        id="name"
                                        name="name"
                                        autoComplete="name"
                                        error={Boolean(errorName)}
                                        helperText={errorName}
                                        sx={{
                                            '& .MuiInputBase-root': {

                                                width: '291px',
                                                height: '48px',
                                                color: 'white',
                                                borderRadius: '#262626',
                                                bgcolor: '#262626'
                                            }
                                        }}
                                    />
                                </Grid>
                                <Grid item xs={10}>
                                    <div className="button-text" style={{fontSize: '12px', color: '#C6C6C6'}}>
                                        Пароль
                                    </div>
                                    <TextField
                                        onChange={(p) => {
                                            setPassword(p.target.value);
                                        }}
                                        onBlur={(p) => {
                                            validatePassword(p.target.value);
                                        }}
                                        required
                                        fullWidth
                                        name="password"
                                        type={showPassword ? 'text' : 'password'}
                                        id="password"
                                        autoComplete="new-password"
                                        error={Boolean(errorPassword)}
                                        helperText={errorPassword}
                                        sx={{
                                            '& .MuiInputBase-root': {
                                                width: '291px',
                                                height: '48px',
                                                color: 'white',
                                                borderRadius: '#373737',
                                                bgcolor: '#262626'
                                            }
                                        }}
                                        InputProps={{
                                            endAdornment: (<InputAdornment position="end">
                                                <IconButton
                                                    aria-label="toggle password visibility"
                                                    onClick={handleClickShowPassword}
                                                    onMouseDown={handleMouseDownPassword}
                                                    edge="end"
                                                    sx={{color: '#737373'}}
                                                >
                                                    {showPassword ? <VisibilityOff/> : <Visibility/>}
                                                </IconButton>
                                            </InputAdornment>)
                                        }}
                                    />
                                </Grid>
                            </Grid>


                            <div style={{display: 'flex', flexDirection: 'column'}}>
                                <Button onClick={singUpOnClick}
                                        fullWidth
                                        variant="contained"
                                        sx={{
                                            mt: 5,
                                            bgcolor: 'transparent',
                                            borderRadius: 0,
                                            color: 'black',
                                            border: '1px solid white',
                                            textTransform: 'none',
                                            width: '291px',
                                            height: '48px',
                                            justifyContent: 'flex-start',
                                            '&:hover': {
                                                bgcolor: '#262626',
                                            },
                                        }}
                                >
                                    <div className="button-text"
                                         style={{fontSize: '14px', color: '#FFFFFF', lineHeight: '18px'}}>
                                        Зарегистрироваться
                                    </div>
                                </Button>
                                <div style={{fontSize: '12px', color: '#d32f2f', marginLeft: 18}}>{error}</div>
                            </div>
                            <div style={{marginLeft: '80px'}}>
                                <Grid container>
                                    <Grid item>
                                        <div style={{color: 'white', marginTop: 30}}>
                                            Есть аккаунт?{' '}

                                            <Link href="/SingIn" variant="body2" sx={{textDecoration: 'none'}}>
                                                Войти
                                            </Link>
                                        </div>
                                    </Grid>
                                </Grid>
                            </div>
                        </Box>
                    </Box>

                </Container>
            </div>
        </div>
    </ThemeProvider>);
};
