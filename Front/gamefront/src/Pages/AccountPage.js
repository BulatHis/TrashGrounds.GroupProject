import React, {useEffect, useState} from 'react';
import {Link, useParams} from 'react-router-dom';
import Typography from "@mui/material/Typography";
import './Player.css'
import TextField from '@mui/material/TextField';
import Button from "@mui/material/Button";
import Grid from "@mui/material/Grid";
import {IconButton, InputAdornment} from "@mui/material";
import VisibilityOff from "@mui/icons-material/VisibilityOff";
import Visibility from "@mui/icons-material/Visibility";


const AccountPage = () => {
    document.body.style.backgroundColor = '#161616';
    const [accountInfo, setAccountInfo] = useState([]);


    const [status, setStatus] = useState('');

    const accessToken = localStorage.getItem('accessToken');
    const refreshToken = localStorage.getItem('refreshToken');


    // Загрузка данных о треке с использованием id
    useEffect(() => {
        const fetchData = async () => {
            try {
                const responseUserData = await fetch(`https://localhost:8000/user/profile/my`, {
                    method: 'GET', headers: {
                        'Content-Type': 'application/json', 'Authorization': `Bearer ${accessToken}`
                    }
                });

                const userData = await responseUserData.json();
                setAccountInfo(userData); // Сохранение данных пользователя в состоянии


                if (!responseUserData.ok) {
                    console.log('Обновляем токен для my');
                    const refreshResponse = await fetch('https://localhost:8000/auth/refresh', {
                        method: 'POST', headers: {
                            'Authorization': `Bearer ${accessToken}`, 'Content-Type': 'application/json',
                        }, body: JSON.stringify({
                            Token: `${accessToken}`, RefreshToken: `${refreshToken}`
                        })
                    });

                    if (!refreshResponse.ok) {
                        throw new Error('Ошибка при обновлении токена refreshToken');
                    }

                    const {accessToken: newAccessToken, refreshToken: newRefreshToken} = await refreshResponse.json();

                    // Сохраняем новые значения токенов в localStorage
                    localStorage.setItem('accessToken', newAccessToken);
                    localStorage.setItem('refreshToken', newRefreshToken);

                    // Повторный запрос с обновленным accessToken
                    const retryResponse = await fetch(`https://localhost:8000/user/profile/my`, {
                        method: 'POST', headers: {
                            'Content-Type': 'application/json', 'Authorization': `Bearer ${newAccessToken}`
                        }
                    });

                    if (!retryResponse.ok) {
                        throw new Error('Ошибка при получение my после обновления токена');
                    }

                    // Получение данных пользователя
                    const userData = await retryResponse.json();
                    setAccountInfo(userData); // Сохранение данных пользователя в состоянии
                }
            } catch (error) {
                console.error('Ошибка при отправке комментария:', error);
            }
        };
        
        fetchData();
    }, [accountInfo]);

    const [password, setPassword] = useState('');
    const [password1, setPassword1] = useState('');
    const [showPassword, setShowPassword] = useState(false);
    const [showPassword1, setShowPassword1] = useState(false);
    const [errorPassword, setErrorPassword] = useState('');
    const [errorPassword1, setErrorPassword1] = useState('');

    const handleClickShowPassword = () => {
        setShowPassword(!showPassword);
    };

    const handleClickShowPassword1 = () => {
        setShowPassword1(!showPassword1);
    };

    const handleMouseDownPassword = (event) => {
        event.preventDefault();
    };

    const handleMouseDownPassword1 = (event) => {
        event.preventDefault();
    };

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

    const validatePassword1 = (password) => {
        const uppercaseRegex = /[A-Z]/;
        const digitRegex = /[0-9]/;
        const latinLettersRegex = /^[a-zA-Z0-9]+$/;


        if (password.length < 8) {
            setErrorPassword1('Пароль одлжен быть не меньше 8 символов');
        } else if (password.length > 60) {
            setErrorPassword1('Пароль одлжен быть не больше 60 символов');
        } else if (!latinLettersRegex.test(password)) {
            setErrorPassword1('Только латинские буквы и арабские цифры');
        } else if (!uppercaseRegex.test(password)) {
            setErrorPassword1('Должкен содержать хотя бы 1 заглавную букву');
        } else if (!digitRegex.test(password)) {
            setErrorPassword1('Пароль должкен содержать хотя бы 1 цифру');
        } else {
            setErrorPassword1('');
        }
    };

    

    const handleChange = (event) => {
        setStatus(event.target.value);
    };

    const handleClick = async () => {
        try {
            const statusToSend = {
                NewStatus: status
            };
            const response = await fetch('https://localhost:8000/user/status', {
                method: 'PATCH', headers: {
                    'Authorization': `Bearer ${accessToken}`, 'Content-Type': 'application/json',
                }, body: JSON.stringify(statusToSend),
            });
            if (!response.ok) {
                throw new Error('Ошибка при отправке статуса на сервер');
            }
            // Если запрос выполнен успешно, обновите статус или выполните другие действия по необходимости
            console.log('Статус успешно отправлен');
        } catch (error) {
            console.error('Ошибка при отправке статуса:', error);
        }
    };

    const handleClick1 = async () => {
        try {
            const passwords = {
                OldPassword: password, NewPassword: password1
            };
            const response = await fetch('https://localhost:8000/auth/change-password', {
                method: 'PATCH', headers: {
                    'Authorization': `Bearer ${accessToken}`, 'Content-Type': 'application/json',
                }, body: JSON.stringify(passwords),
            });
            if (!response.ok) {
                throw new Error('Ошибка при отправке статуса на сервер');
            }
            // Если запрос выполнен успешно, обновите статус или выполните другие действия по необходимости
            console.log('Статус успешно отправлен');
        } catch (error) {
            console.error('Ошибка при отправке статуса:', error);
        }
    };
    
    

    const [file, setFile] = useState(null);

    // Обработчик изменения файла
    const handleFileChange = (event) => {
        const file = event.target.files[0];
        setFile(file);
    };

    const handleClick2 = async () => {
        if (!file) {
            console.error('Файл не выбран');
            return;
        }

        try {
            const formData = new FormData();
            formData.append('File', file);

            const uploadResponse = await fetch('https://localhost:8000/image/upload', {
                method: 'POST',
                headers: {
                    'Authorization': `Bearer ${accessToken}`
                },
                body: formData
            });

            if (!uploadResponse.ok) {
                throw new Error('Ошибка при загрузке файла');
            }

            const AssetId = (await uploadResponse.text()).replace(/"/g, '');


            const postFormData = {
                NewAvatarId: AssetId
            };

            const addPostResponse = await fetch('https:/localhost:8000/user/avatar', {
                method: 'PATCH',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${accessToken}`
                },
                body: JSON.stringify(postFormData)
            });

            if (!addPostResponse.ok) {
                throw new Error('Ошибка при добавлении поста');
            }

            console.log('фОТО  успешно добавлен');
        } catch (error) {
            console.error('Ошибка:', error);
        }
    };


    return (<>
        {accountInfo && (<div style={{display: 'flex', marginLeft: '370px', marginTop: '50px'}}>
            <div style={{}}>
                <div>
                    <Typography
                        variant="Body-Large"
                        style={{
                            color: '#FFFFFF',
                            fontFamily: 'Gilroy, sans-serif',
                            fontWeight: 700,
                            fontSize: '24px',
                            lineHeight: '30px',
                        }}>
                        Аккаунт
                    </Typography>
                </div>
                <div style={{marginTop: '30px'}}>
                    <Typography
                        variant="Body-Large"
                        style={{
                            color: '#FFFFFF',
                            fontFamily: 'Gilroy, sans-serif',
                            fontWeight: 500,
                            fontSize: '14px',
                            lineHeight: '18px',
                        }}>
                        Имя пользователя: {accountInfo.nickname}
                    </Typography>
                </div>
                <div style={{marginTop: '50px'}}>
                    <Typography
                        variant="Body-Large"
                        style={{
                            color: '#FFFFFF',
                            fontFamily: 'Gilroy, sans-serif',
                            fontWeight: 600,
                            fontSize: '16px',
                            lineHeight: '20px',
                        }}>
                        Изменение статуса
                    </Typography>
                </div>

                <div style={{marginTop: '50px'}}>
                     <textarea
                         style={{
                             flex: '1',
                             height: '120px',
                             width: '380px',
                             backgroundColor: '#262626',
                             border: 'none',
                             resize: 'none',
                             color: '#FFFFFF'
                         }}
                         value={status}
                         onChange={handleChange}
                     />
                </div>

                <div style={{display: 'flex', flexDirection: 'column'}}>
                    <Button onClick={handleClick}
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
                            Обновить
                        </div>
                    </Button>
                </div>

                <div style={{marginTop: '50px'}}>
                    <Typography
                        variant="Body-Large"
                        style={{
                            color: '#FFFFFF',
                            fontFamily: 'Gilroy, sans-serif',
                            fontWeight: 600,
                            fontSize: '16px',
                            lineHeight: '20px',
                        }}>
                        Изменение пароля
                    </Typography>
                </div>


                <Grid item xs={10}>
                    <div className="button-text" style={{fontSize: '12px', color: '#C6C6C6', marginTop: '12px'}}>
                        <Typography
                            variant="Body-Large"
                            style={{
                                color: '#FFFFFF',
                                fontFamily: 'Gilroy, sans-serif',
                                fontWeight: 400,
                                fontSize: '12px',
                                lineHeight: '16px',
                            }}>
                            Старый пароль
                        </Typography>
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
                                width: '380px',
                                height: '48px',
                                color: 'white',
                                borderRadius: '#373737',
                                bgcolor: '#262626',
                                marginTop: '8px'
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


                <Grid item xs={10}>
                    <div className="button-text" style={{fontSize: '12px', color: '#C6C6C6', marginTop: '12px'}}>
                        <Typography
                            variant="Body-Large"
                            style={{
                                color: '#FFFFFF',
                                fontFamily: 'Gilroy, sans-serif',
                                fontWeight: 400,
                                fontSize: '12px',
                                lineHeight: '16px',
                            }}>
                            Новый пароль
                        </Typography>

                    </div>
                    <TextField
                        onChange={(p) => {
                            setPassword1(p.target.value);
                        }}
                        onBlur={(p) => {
                            validatePassword1(p.target.value);
                        }}
                        required
                        fullWidth
                        name="password"
                        type={showPassword1 ? 'text' : 'password'}
                        id="password"
                        autoComplete="new-password"
                        error={Boolean(errorPassword1)}
                        helperText={errorPassword1}
                        sx={{
                            '& .MuiInputBase-root': {
                                width: '380px',
                                height: '48px',
                                color: 'white',
                                borderRadius: '#373737',
                                bgcolor: '#262626',
                                marginTop: '8px'
                            }
                        }}
                        InputProps={{
                            endAdornment: (<InputAdornment position="end">
                                    <IconButton
                                        aria-label="toggle password visibility"
                                        onClick={handleClickShowPassword1}
                                        onMouseDown={handleMouseDownPassword1}
                                        edge="end"
                                        sx={{color: '#737373'}}
                                    >
                                        {showPassword1 ? <VisibilityOff/> : <Visibility/>}
                                    </IconButton>
                                </InputAdornment>)
                        }}
                    />
                </Grid>

                <div style={{display: 'flex', flexDirection: 'column'}}>
                    <Button onClick={handleClick1}
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
                            Обновить
                        </div>
                    </Button>
                </div>

            </div>
            <div style={{display: 'flex', flexDirection: 'column', marginTop: '50px', marginLeft: '100px'}}>
                <div>
                    <img src={`https://localhost:8000/image/${accountInfo.avatarId}`} alt="trackPic"
                         style={{width: '280px', height: '280px'}}/>
                </div>

                <div style={{marginTop: '25px'}}>
                    <Typography
                        variant="Body-Large"
                        style={{
                            color: '#FFFFFF',
                            fontFamily: 'Gilroy, sans-serif',
                            fontWeight: 600,
                            fontSize: '14px',
                            lineHeight: '18px',
                        }}>
                        Загрузите новую фотографию
                    </Typography>
                </div>

                <div style={{marginTop: '8px', width: '280px', height: '36px',}}>
                    <Typography
                        variant="Body-Large"
                        style={{
                            color: '#FFFFFF',
                            fontFamily: 'Gilroy, sans-serif',
                            fontWeight: 400,
                            fontSize: '14px',
                            lineHeight: '18px',
                        }}>
                        Максимальный размер 2МБ <br/>
                        Допустимые форматы: .png, .jpg, .webp
                    </Typography>
                </div>

                <div style={{
                    marginTop: '16px',
                    width: '280px',
                    height: '126px',
                    border: '2px dashed #CCCCCC',
                    display: 'flex',
                    flexDirection: 'column',
                    justifyContent: 'center',
                    alignItems: 'center'
                }}>
                    <input
                        type="file"
                        accept=".png"
                        onChange={handleFileChange}
                        style={{opacity: 0, width: '80%', height: '55%'}}
                    />
                    {file ? (<p style={{color: '#FFFFFF', textAlign: 'center'}}>
                        Файл {file.name} выбран
                    </p>) : (<p style={{color: '#666666', textAlign: 'center'}}>
                        Перетащите файл сюда или нажмите для выбора
                    </p>)}
                </div>

                <div style={{display: 'flex', flexDirection: 'column'}}>
                    <Button onClick={handleClick2}
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
                            Обновить
                        </div>
                    </Button>
                </div>

            </div>

            <div style={{display: 'flex', flexDirection: 'column', marginTop: '50px', marginLeft: '100px'}}>
                <div style={{display: 'flex', flexDirection: 'column'}}>
                    <Link to="/CreateTrack" style={{textDecoration: 'none'}}>
                        <Button fullWidth
                                variant="contained"
                                sx={{
                                    mt: 5,
                                    bgcolor: 'transparent',
                                    borderRadius: 0,
                                    color: 'black',
                                    border: '1px solid white',
                                    textTransform: 'none',
                                    width: '265px',
                                    height: '100px',
                                    justifyContent: 'flex-start',
                                    '&:hover': {
                                        bgcolor: '#262626',
                                    },
                                }}
                        >
                            <div className="button-text"
                                 style={{fontSize: '20px', color: '#FFFFFF', lineHeight: '18px'}}>
                                Добавить новый трек
                            </div>
                        </Button>
                    </Link>
                </div>

                <div style={{display: 'flex', flexDirection: 'column'}}>
                    <Link to="/CreatePost" style={{textDecoration: 'none'}}>
                        <Button fullWidth
                                variant="contained"
                                sx={{
                                    mt: 5,
                                    bgcolor: 'transparent',
                                    borderRadius: 0,
                                    color: 'black',
                                    border: '1px solid white',
                                    textTransform: 'none',
                                    width: '265px',
                                    height: '100px',
                                    justifyContent: 'flex-start',
                                    '&:hover': {
                                        bgcolor: '#262626',
                                    },
                                }}
                        >
                            <div className="button-text"
                                 style={{fontSize: '20px', color: '#FFFFFF', lineHeight: '18px'}}>
                                Добавить новый пост
                            </div>
                        </Button>
                    </Link>
                </div>
            </div>

        </div>)}
    </>);
};

export default AccountPage;