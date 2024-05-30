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


const CreateTrack = () => {
    const [redirect, setRedirect] = useState(false);
    const navigate = useNavigate();
    document.body.style.backgroundColor = '#161616';

    const [status, setStatus] = useState('');

    const [title, setTitle] = useState('');

    const [selectedFile, setSelectedFile] = useState(null);
    const [selectedFile1, setSelectedFile1] = useState(null);

    const accessToken = localStorage.getItem('accessToken');

    const [isOn, setIsOn] = useState(false);

    const handleToggle = () => {
        setIsOn(!isOn);
    };

    const handleCreatePost = async () => {
        if (!selectedFile || !selectedFile1) {
            console.error('Файлы не выбраны');
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
                throw new Error('Ошибка при загрузке файла изображения');
            }

            const PictureId = (await uploadResponse.text()).replace(/"/g, '');

            const formData1 = new FormData();
            formData1.append('File', selectedFile1); // Используйте formData1 здесь

            const uploadResponse1 = await fetch('https://localhost:8000/music/upload', {
                method: 'POST',
                headers: {
                    'Authorization': `Bearer ${accessToken}`
                },
                body: formData1
            });

            if (!uploadResponse1.ok) {
                throw new Error('Ошибка при загрузке файла музыки');
            }

            const MusicId = (await uploadResponse1.text()).replace(/"/g, '');
            
            const genresMap = {
                'Русский рэп': 'd4b7bc34-3b01-4247-a9bc-02d4c5185e08',
                'Мэшап' : '50e3aa21-ada0-42d3-a143-cf45922b97a5' ,  
                'Что-то странное' : '251ceebe-752a-4ea4-810e-03b8eaee7e5f'
            };
            
            const selectedGenreId = genresMap[selectedOption];

            const postFormData = {
                Title: title,
                Description: status,
                IsExplicit: isOn,
                PictureId: PictureId,
                MusicId: MusicId,
                Genres: [selectedGenreId]
            };
            
            
            const addPostResponse = await fetch('https://localhost:8000/track/add', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${accessToken}`
                },
                body: JSON.stringify(postFormData)
            });

            if (!addPostResponse.ok) {
                throw new Error('Ошибка при добавлении поста');
            }

            console.log('Трек успешно добавлен');
            setRedirect(true);
            if (redirect) {
                navigate('/');
            }
        } catch (error) {
            console.error('Ошибка:', error);
        }
    };

    useEffect(() => {
        // Если установлен флаг перенаправления, перенаправляем пользователя
        if (redirect) {
            navigate('/');
        }
    }, [redirect, navigate]);


    const handleFileChange = (event) => {
        const file = event.target.files[0];
        setSelectedFile(file);
    };

    const handleFileChange1 = (event) => {
        const file = event.target.files[0];
        setSelectedFile1(file);
    };

    const handleChange = (event) => {
        setStatus(event.target.value); // Обновление состояния status при вводе текста
    };

    const handleChange1 = (event) => {
        setTitle(event.target.value); // Обновление состояния status при вводе текста
    };

    

    const [selectedOption, setSelectedOption] = useState('');

    const handleSelectChange = (event) => {
        setSelectedOption(event.target.value);
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
                        Название
                    </Typography>
                </div>

                <div style={{marginTop: '20px'}}>
                    <textarea
                        style={{
                            flex: '1',
                            height: '48px',
                            width: '380px',
                            backgroundColor: '#262626',
                            border: 'none',
                            resize: 'none',
                            color: '#FFFFFF'
                        }}
                        value={title}
                        onChange={handleChange1} // Добавление обработчика изменений
                    />
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
                        Описание
                    </Typography>
                </div>

                <div style={{marginTop: '20px'}}>
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
                onChange={handleChange} // Добавление обработчика изменений
            />
                    <div style={{marginTop: '30px', display: ''}}>
                        <Typography
                            variant="Body-Large"
                            style={{
                                color: '#FFFFFF',
                                fontFamily: 'Gilroy, sans-serif',
                                fontWeight: 600,
                                fontSize: '16px',
                                lineHeight: '20px',
                            }}>
                            Жанры
                        </Typography>
                    </div>
                    <div style={{marginTop: '30px', display: ''}}>
                        <select
                            value={selectedOption}
                            onChange={handleSelectChange}
                            style={{
                                width: '200px',
                                height: '40px',
                                fontSize: '16px',
                                border: '1px solid #ccc',
                                borderRadius: '0px',
                                padding: '5px',
                                backgroundColor: '#262626',
                                color: '#FFFFFF', // измените цвет текста здесь
                            }}
                        >
                            <option value=""></option>
                            <option value="Что-то странное">Что-то странное</option>
                            <option value="Мэшап">Мэшап</option>
                            <option value="Русский рэп">Русский рэп</option>
                        </select>
                    </div>

                    <div style={{marginTop: '30px', display: 'flex'}}>
                        <input
                            type="checkbox"
                            checked={isOn}
                            onChange={handleToggle}
                            style={{display: 'none'}}
                        />
                        <div
                            style={{
                                position: 'relative',
                                width: '60px',
                                height: '30px',
                                borderRadius: '15px',
                                backgroundColor: isOn ? '#4CAF50' : '#ccc',
                                cursor: 'pointer',
                                transition: 'background-color 0.3s ease',
                            }}
                            onClick={handleToggle}
                        >
                            <div
                                style={{
                                    width: '30px',
                                    height: '30px',
                                    borderRadius: '50%',
                                    backgroundColor: '#fff',
                                    position: 'absolute',
                                    top: '0',
                                    left: isOn ? '30px' : '0',
                                    transition: 'left 0.3s ease',
                                }}
                            />
                        </div>

                        <Typography
                            variant="Body-Large"
                            style={{
                                color: '#FFFFFF',
                                fontFamily: 'Gilroy, sans-serif',
                                fontWeight: 600,
                                marginTop: '5px',
                                marginLeft: '10px',
                                fontSize: '16px',
                                lineHeight: '20px',
                            }}>
                            Explicit
                        </Typography>

                    </div>

                </div>

                <div style={{display: 'flex', marginTop: '120px'}}>
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

            <div style={{display: '', marginLeft: '80px'}}>
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

            <div style={{display: '', marginLeft: '20px'}}>
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
                        Трек
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
                        Максимальный размер 20МБ < br/>
                        Допустимый формат: .mp3
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
                        accept=".mp3"
                        onChange={handleFileChange1}
                        style={{opacity: 0, width: '80%', height: '55%'}}
                    />
                    {selectedFile1 ? (<p style={{color: '#FFFFFF', textAlign: 'center'}}>
                        Файл {selectedFile1.name} выбран
                    </p>) : (<p style={{color: '#666666', textAlign: 'center'}}>
                        Перетащите файл сюда или нажмите для выбора
                    </p>)}
                </div>


            </div>

        </div>
    </>);
};

export default CreateTrack;