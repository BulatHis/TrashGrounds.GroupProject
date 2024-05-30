import * as React from 'react';
import {useEffect, useState} from 'react';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import {useLocation, useNavigate} from 'react-router-dom';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import {createTheme, ThemeProvider} from '@mui/material/styles';

const defaultTheme = createTheme();
export default function NotFoundPage() {
    useEffect(() => {
        document.body.style.backgroundColor = '#161616';
        // Отключаем прокрутку страницы при монтировании компонента
        document.body.style.overflow = 'hidden';
        // Возвращаем прокрутку при размонтировании компонента
        return () => {
            document.body.style.overflow = 'visible';
        };
    }, []);

    return (
        <div style={{
            backgroundColor: '#161616',
            color: 'white',
            fontFamily: 'Gilroy, sans-serif',
            height: '100vh',
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
            justifyContent: 'center'
        }}>
            <Typography variant="h1" component="h1" gutterBottom>404</Typography>
            <Typography variant="h5" component="h2">Страница не найдена</Typography>
        </div>
    );
};