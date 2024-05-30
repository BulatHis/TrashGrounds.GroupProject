import React, {useEffect, useState} from 'react';
import {Link, useLocation} from 'react-router-dom';
import {AppBar, Toolbar, Typography, Button} from '@mui/material';
import logoMini from './logomini4.png'

export default function Header() {
    const buttonStyle = {
        fontFamily: 'Gilroy, sans-serif',
        fontSize: '16px',
        fontWeight: 600,
        lineHeight: '20px',
        textAlign: 'left',
        color: '#FFFFFF',
        marginTop: '20px',
        textTransform: 'none',
    };
    const {pathname} = useLocation();

    const accessToken = localStorage.getItem('accessToken');
    const refreshToken = localStorage.getItem('refreshToken');

    const [userInfo, setUserInfo] = useState([]);
    
    useEffect(() => {
        const fetchData = async () => {
            try {
                const responseUserData = await fetch(`https://localhost:8000/user/profile/my`, {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${accessToken}`
                    }
                });

                const userData = await responseUserData.json();
                setUserInfo(userData); // Сохранение данных пользователя в состоянии

                if (!responseUserData.ok) {
                    console.log('Обновляем токен для my');
                    const refreshResponse = await fetch('https://localhost:8000/auth/refresh', {
                        method: 'POST',
                        headers: {
                            'Authorization': `Bearer ${accessToken}`,
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify({
                            Token: `${accessToken}`,
                            RefreshToken: `${refreshToken}`
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
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                            'Authorization': `Bearer ${newAccessToken}`
                        }
                    });

                    if (!retryResponse.ok) {
                        throw new Error('Ошибка при получение my после обновления токена');
                    }

                    // Получение данных пользователя
                    const userData = await retryResponse.json();
                    setUserInfo(userData); // Сохранение данных пользователя в состоянии
                }
            } catch (error) {
                console.error('Ошибка при отправке комментария:', error);
            }
        };

        fetchData();
    }, [userInfo]);

    if (pathname === '/SingUp' || pathname === '/SingIn') {
        return null;
    }

    return (<>
        {userInfo && userInfo.avatarId !== undefined ? (
        <AppBar position="static" sx={{background: '#161616', height: '80px', boxShadow: 'none'}}>
            <Toolbar>
                <img src={logoMini} alt="logo"
                     style={{
                         width: '138px',
                         height: '40px',
                         marginLeft: '350px',
                         marginRight: '240px',
                         marginTop: '20px',
                     }}/>
                <Button component={Link} to="/" sx={buttonStyle}>Главная</Button>
                <Button component={Link} to="/Search" sx={buttonStyle}>Поиск</Button>
                <Button component={Link} to="/AccountPage" sx={buttonStyle}>Мой профиль</Button>
                <Button component={Link} to={`/Author/${userInfo.id}`} sx={buttonStyle}>Мои посты</Button>
                <div style={{
                    fontFamily: 'Gilroy, sans-serif',
                    fontSize: '16px',
                    fontWeight: 600,
                    lineHeight: '20px',
                    textAlign: 'left',
                    color: '#FFFFFF',
                    marginTop: '20px',
                    textTransform: 'none', marginLeft: '245px'
                }}>
                    <Link to={`/AccountPage`}
                          style={{textDecoration: 'none', color: '#FFFFFF'}}>
                        {userInfo.nickname}
                    </Link>

                </div>
                <Link to={`/AccountPage`}
                      style={{textDecoration: 'none', color: '#FFFFFF'}}>
                    <img src={`https://localhost:8000/image/${userInfo.avatarId}`} alt="logo"
                         style={{width: '40px', height: '40px', marginTop: '20px', marginLeft: '10px'}}/>
                </Link>

            </Toolbar>
        </AppBar>
            ) :
            (
                <AppBar position="static" sx={{background: '#161616', height: '80px', boxShadow: 'none'}}>
                    <Toolbar>
                        <img src={logoMini} alt="logo"
                             style={{
                                 width: '138px',
                                 height: '40px',
                                 marginLeft: '350px',
                                 marginRight: '240px',
                                 marginTop: '20px',
                             }}/>
                        <Button component={Link} to="/" sx={buttonStyle}>Главная</Button>
                        <Button component={Link} to="/search" sx={buttonStyle}>Поиск</Button>
                        <div style={{ marginTop: '20px'}}>
                            <Link to="/SingUp" style={{ textDecoration: 'none', color: '#FFFFFF',
                                width: '40px', height: '40px', marginLeft: '445px',  fontFamily: 'Gilroy, sans-serif',
                                fontSize: '16px',
                                fontWeight: 600,
                                lineHeight: '20px',
                                textAlign: 'left',
                                textTransform: 'none'}}>Зарегистрироваться</Link>
                        </div>
                        
                    </Toolbar>
                </AppBar>
            )}
        </>
    );
};