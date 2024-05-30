import React, {useEffect, useState} from 'react';
import {Link, useParams} from 'react-router-dom';
import dogCoin from "./dogCoin.png";
import Typography from "@mui/material/Typography";
import pog from './pog.png'
import './Player.css'
import TextField from '@mui/material/TextField';
import ReactAudioPlayer from 'react-audio-player';
import trend from "./trend.png";
import Button from "@mui/material/Button";
import { useNavigate } from 'react-router-dom';



const CreatePost = () => {
    const [redirect, setRedirect] = useState(false);
    const navigate = useNavigate();
    
    document.body.style.backgroundColor = '#161616';

    const [status, setStatus] = useState('');

    const [selectedFile, setSelectedFile] = useState(null);

    const accessToken = localStorage.getItem('accessToken');

    const handleCreatePost = async () => {
        if (!selectedFile) {
            console.error('Файл не выбран');
            return;
        }

        try {
            const formData = new FormData();
            formData.append('File', selectedFile);

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
                Text: status,
                AssetId: AssetId,
                IsHidden: false
            };

            const addPostResponse = await fetch('https:/localhost:8000/posts/add', {
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

            console.log('Пост успешно добавлен');

            setRedirect(true);
            if (redirect) {
                navigate('/AccountPage');
            }
        } catch (error) {
            console.error('Ошибка:', error);
        }
    };

    useEffect(() => {
        // Если установлен флаг перенаправления, перенаправляем пользователя
        if (redirect) {
            navigate('/AccountPage');
        }
    }, [redirect, navigate]);

    const handleFileChange = (event) => {
        const file = event.target.files[0];
        setSelectedFile(file);
    };

    const handleChange = (event) => {
        setStatus(event.target.value); // Обновление состояния status при вводе текста
    };


    return (<>
        (
        <div style={{display: 'flex', marginLeft: '370px', marginTop: '50px'}}>
            <div style={{display: ''}}>
                <div style={{marginTop: 'px'}}>
                    <Typography
                        variant="Body-Large"
                        style={{
                            color: '#FFFFFF',
                            fontFamily: 'Gilroy, sans-serif',
                            fontWeight: 600,
                            fontSize: '16px',
                            lineHeight: '20px',
                        }}>
                        Текст
                    </Typography>
                </div>

                <div style={{marginTop: '20px'}}>
            <textarea
                style={{
                    flex: '1',
                    height: '144px',
                    width: '380px',
                    backgroundColor: '#262626',
                    border: 'none',
                    resize: 'none',
                    color: '#FFFFFF'
                }}
                value={status}
                onChange={handleChange} // Добавление обработчика изменений
            />
                </div>

                <div style={{display: 'flex', marginTop: '184px'}}>
                    <Link to="/AccountPage" style={{textDecoration: 'none'}}>
                        <Button
                            fullWidth
                            variant="contained"
                            sx={{
                                mt: 5,
                                bgcolor: 'transparent',
                                borderRadius: 0,
                                color: 'black',
                                border: '1px solid white',
                                textTransform: 'none',
                                width: '180px',
                                height: '48px',
                                justifyContent: 'flex-start',
                                '&:hover': {
                                    bgcolor: '#262626',
                                },
                            }}
                        >
                            <div className="button-text"
                                 style={{fontSize: '14px', color: '#FFFFFF', lineHeight: '18px'}}>
                                Отменить
                            </div>
                        </Button>
                    </Link>

                    <Button
                        fullWidth
                        variant="contained"
                        sx={{
                            mt: 5,
                            marginLeft: '20px',
                            bgcolor: 'transparent',
                            borderRadius: 0,
                            color: 'black',
                            border: '1px solid white',
                            textTransform: 'none',
                            width: '180px',
                            height: '48px',
                            justifyContent: 'flex-start',
                            '&:hover': {
                                bgcolor: '#262626',
                            },
                        }}
                        onClick={handleCreatePost}
                    >
                        <div className="button-text" style={{fontSize: '14px', color: '#FFFFFF', lineHeight: '18px'}}>
                            Создать
                        </div>
                    </Button>
                </div>
            </div>

            <div style={{display: '', marginLeft: '120px'}}>
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
                        Обложка
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
                    {selectedFile ? (<p style={{color: '#FFFFFF', textAlign: 'center'}}>
                        Файл {selectedFile.name} выбран
                    </p>) : (<p style={{color: '#666666', textAlign: 'center'}}>
                        Перетащите файл сюда или нажмите для выбора
                    </p>)}
                </div>
            </div>
        </div>
    </>);
};

export default CreatePost;