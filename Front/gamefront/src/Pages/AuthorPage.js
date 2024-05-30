import React, {useEffect, useState} from 'react';
import {Link, useParams} from 'react-router-dom';
import dogCoin from "./dogCoin.png";
import Typography from "@mui/material/Typography";
import pog from './pog.png'
import './Player.css'
import TextField from '@mui/material/TextField';
import ReactAudioPlayer from 'react-audio-player';
import trend from "./trend.png";


const AuthorPage = () => {
    document.body.style.backgroundColor = '#161616';
    const {authorId} = useParams(); // Получаем id автора из параметров маршрута
    const [authorInfo, setAuthorInfo] = useState([]);
    const [sameAuthor, setSameAuthor] = useState([]);
    const [postInfo, setPostInfo] = useState([]);

    // Загрузка данных о треке с использованием id
    useEffect(() => {
        const fetchData = async () => {
            try {
                const responseAuthorData = await fetch(`https://localhost:8000/user/profile/${authorId}`);
                if (!responseAuthorData.ok) {
                    throw new Error('Ошибка при получении данных');
                }
                const authorData = await responseAuthorData.json();
                if (!authorData) {
                    throw new Error('Неверный формат данных');
                }
                setAuthorInfo(authorData);
            } catch (error) {
                console.error('Ошибка при получении данных трека:', error);
            }
        };

        const fetchSameData = async () => {
            try {
                const responseSameData = await fetch(`https://localhost:8000/track/from-user/${authorId}?take=5&exclude-track-id=null&skip=0`);
                if (!responseSameData.ok) {
                    throw new Error('Ошибка при получении данных');
                }
                const sameData = await responseSameData.json();
                if (!sameData || !Array.isArray(sameData)) {
                    throw new Error('Неверный формат данных');
                }
                setSameAuthor(sameData);
            } catch (error) {
                console.error('Ошибка при получении данных от пользователя:', error);
            }
        };

        const fetchPostData = async () => {
            try {
                const responsePostData = await fetch(`https://localhost:8000/posts/from-user/${authorId}?take=2&skip=0`);
                if (!responsePostData.ok) {
                    throw new Error('Ошибка при получении данных');
                }
                const postData = await responsePostData.json();
                if (!postData || !Array.isArray(postData)) {
                    throw new Error('Неверный формат данных');
                }
                setPostInfo(postData);
            } catch (error) {
                console.error('Ошибка при получении данных от пользователя:', error);
            }
        };

        fetchData();
        if (authorInfo) {
            fetchSameData();
            fetchPostData();
        }
    }, [authorId, authorInfo]);

    const registrationDate = new Date(authorInfo.registrationDate);

// Извлечение дня, месяца и года
    const day = registrationDate.getDate();

    const month = registrationDate.getMonth() + 1; // Месяцы в JS начинаются с 0

    const year = registrationDate.getFullYear();


// Форматирование даты в виде dd.mm.yyyy
    const formattedDate = `${day < 10 ? '0' + day : day}.${month < 10 ? '0' + month : month}.${year}`;


    const [content, setContent] = useState('content1');

    const handleButtonClick = (newContent) => {
        setContent(newContent);
    };

    const [button1Clicked, setButton1Clicked] = useState(false);
    const [button2Clicked, setButton2Clicked] = useState(false);

    const handleButtonClick1 = () => {
        setButton1Clicked(true);
        setButton2Clicked(false);
        // Дополнительная логика для обработки нажатия кнопки 1
    };

    const handleButtonClick2 = () => {
        setButton1Clicked(false);
        setButton2Clicked(true);
        // Дополнительная логика для обработки нажатия кнопки 2
    };


    return (<>
        <div style={{marginLeft: '370px', marginRight: '350px', display: 'flex', marginTop: '50px'}}>
            {authorInfo && (<img src={`https://localhost:8000/image/${authorInfo.avatarId}`} alt="authorPic"
                                 style={{width: '280px', height: '280px'}}/>)}
            <div style={{
                display: 'flex', flexDirection: 'column', width: '900px', height: '280px', backgroundColor: '#262626'
            }}>
                <div style={{
                    display: 'flex', height: '40px', marginTop: '17px', marginLeft: '20px'
                }}>
                    <div>
                        {authorInfo && (<Typography variant="body2" style={{
                            color: '#FFFFFF',
                            fontFamily: 'Gilroy, sans-serif',
                            fontWeight: 700,
                            fontSize: '32px',
                            lineHeight: '40px',
                        }}>
                            {authorInfo.nickname}
                        </Typography>)}
                    </div>
                    <div style={{height: '20px', marginTop: '13px', marginLeft: '20px'}}>
                        {authorInfo && (<Typography variant="Body-Large" style={{
                            color: '#FFFFFF',
                            fontFamily: 'Gilroy, sans-serif',
                            fontWeight: 500,
                            fontSize: '12px',
                            lineHeight: '15px',
                        }}>
                            на сайте с {formattedDate}
                        </Typography>)}
                    </div>
                </div>

                <div style={{height: '148px', width: '580px', marginTop: '15px', marginLeft: '25px'}}>
                    {authorInfo && (<Typography variant="Body-Large" style={{
                        color: '#FFFFFF',
                        fontFamily: 'Gilroy, sans-serif',
                        fontWeight: 500,
                        fontSize: '14px',
                        lineHeight: '18px',
                    }}>
                        {authorInfo.status}
                    </Typography>)}
                </div>

            </div>
        </div>

        <div style={{
            width: '1160px',
            height: '870px',
            marginLeft: '370px',
            marginRight: '370px',
            marginTop: '50px',
            backgroundColor: '#262626',
        }}>
            {/* Верхняя панель */}
            <div style={{height: '50px', display: 'flex'}}>
                {/* Кнопка 1 */}
                <button
                    style={{
                        width: '590px',
                        backgroundColor: button2Clicked ? '#393939' : '#262626',
                        display: 'flex',
                        justifyContent: 'flex-start', // Выравнивание текста по правому краю
                        alignItems: 'center', // Выравнивание текста по вертикали
                        padding: '0 20px', // Добавление отступов для текста
                        border: 'none', // Убрать границу
                        outline: 'none', // Убрать контур
                        boxShadow: 'none', // Убрать тень
                    }}
                    onClick={() => {
                        handleButtonClick1();
                        handleButtonClick('content1');
                    }}
                >
                    <Typography
                        variant="Body-Large"
                        style={{
                            color: '#FFFFFF',
                            fontFamily: 'Gilroy, sans-serif',
                            fontWeight: 600,
                            fontSize: '14px',
                            lineHeight: '18px',
                        }}
                    >
                        Треки
                    </Typography>
                </button>
                {/* Кнопка 2 */}
                <button
                    style={{
                        width: '590px',
                        backgroundColor: button1Clicked ? '#393939' : '#262626',
                        display: 'flex',
                        justifyContent: 'flex-start', // Выравнивание текста по правому краю
                        alignItems: 'center', // Выравнивание текста по вертикали
                        padding: '0 20px', // Добавление отступов для текста
                        border: 'none', // Убрать границу
                        outline: 'none', // Убрать контур
                        boxShadow: 'none', // Убрать тень
                    }}
                    onClick={() => {
                        handleButtonClick2();
                        handleButtonClick('content2');
                    }}
                >
                    <Typography
                        variant="Body-Large"
                        style={{
                            color: '#FFFFFF',
                            fontFamily: 'Gilroy, sans-serif',
                            fontWeight: 600,
                            fontSize: '14px',
                            lineHeight: '18px',
                        }}
                    >
                        Сообщество
                    </Typography>
                </button>
            </div>
            {/* Область для отображения содержимого */}
            <div style={{height: '520px'}}>
                {content === 'content1' && (<div style={{marginTop: '20px', marginLeft: ''}}>
                    <div className="track-list">
                        <div className="track-header" style={{display: 'flex'}}>
                            <div className="header-item" style={{justifyContent: 'flex-start', marginLeft: '130px'}}>
                                <Typography
                                    variant="Body-Large"
                                    style={{
                                        color: '#FFFFFF',
                                        fontFamily: 'Gilroy, sans-serif',
                                        fontWeight: 600,
                                        fontSize: '20px',
                                        lineHeight: '22px',
                                    }}
                                >
                                    Название
                                </Typography></div>
                            <div className="header-item" style={{justifyContent: 'flex-start', marginLeft: '270px'}}>
                                <Typography
                                    variant="Body-Large"
                                    style={{
                                        color: '#FFFFFF',
                                        fontFamily: 'Gilroy, sans-serif',
                                        fontWeight: 600,
                                        fontSize: '20px',
                                        lineHeight: '22px',
                                    }}
                                >
                                    Прослушивания
                                </Typography></div>
                            <div className="header-item" style={{justifyContent: 'flex-start', marginLeft: '200px'}}>
                                <Typography
                                    variant="Body-Large"
                                    style={{
                                        color: '#FFFFFF',
                                        fontFamily: 'Gilroy, sans-serif',
                                        fontWeight: 600,
                                        fontSize: '20px',
                                        lineHeight: '22px',
                                    }}
                                >
                                    Средняя оценка
                                </Typography></div>
                        </div>
                        <hr style={{width: '1155px', height: '0.5px', backgroundColor: '#808080'}}/>

                        {sameAuthor.map((track, index) => (<div className="track-item" key={index}
                                                                style={{
                                                                    display: 'flex',
                                                                    marginLeft: '20px',
                                                                    marginTop: '30px'
                                                                }}>
                            <div>
                                <Link to={`/MusicPage/${track.trackInfo.id}`}
                                      style={{textDecoration: 'none', color: '#FFFFFF'}}>
                                    <img src={`https://localhost:8000/image/${track.trackInfo.pictureId}`}
                                         alt="authorPic"
                                         style={{width: '70px', height: '70px'}}
                                    />
                                </Link>
                            </div>

                            <div className="item" style={{marginLeft: '40px', marginTop: '25px', width: '160px'}}>
                                <Typography
                                    variant="Body-Large"
                                    style={{
                                        color: '#FFFFFF',
                                        fontFamily: 'Gilroy, sans-serif',
                                        fontWeight: 500,
                                        fontSize: '16px',
                                        lineHeight: '18px',
                                    }}>
                                    <Link to={`/MusicPage/${track.trackInfo.id}`} style={{
                                        textDecoration: 'none', color: '#FFFFFF'
                                    }}>{track.trackInfo.title}</Link>
                                </Typography></div>
                            <div className="item" style={{marginLeft: '270px', marginTop: '25px'}}>
                                <Typography
                                    variant="Body-Large"
                                    style={{
                                        color: '#FFFFFF',
                                        fontFamily: 'Gilroy, sans-serif',
                                        fontWeight: 600,
                                        fontSize: '16px',
                                        lineHeight: '18px',
                                    }}>
                                    {track.trackInfo.listensCount}
                                </Typography></div>
                            <div className="item" style={{marginLeft: '390px', marginTop: '25px'}}>
                                <Typography
                                    variant="Body-Large"
                                    style={{
                                        color: '#FFFFFF',
                                        fontFamily: 'Gilroy, sans-serif',
                                        fontWeight: 600,
                                        fontSize: '16px',
                                        lineHeight: '18px',
                                    }}>
                                    {track.rate.rating}
                                </Typography>
                            </div>
                        </div>))}
                    </div>
                </div>)}

                {content === 'content2' && (<div style={{marginTop: '20px', marginLeft: ''}}>
                    <div className="track-list">
                        <div className="track-header" style={{display: 'flex'}}>
                        </div>

                        {postInfo.map((post, index) =>
                            (<div className="track-item" key={index}
                                  style={{display: 'flex', marginLeft: '20px', marginTop: '30px'}}>
                                <div>
                                    <img src={`https://localhost:8000/image/${post.post.assetId}`}
                                         alt="authorPic"
                                         style={{width: '560px', height: '350px'}}/>
                                </div>

                                <div style={{}}>
                                    <div className="item" style={{
                                        marginLeft: '40px',
                                        marginTop: '25px',
                                        width: '540px',
                                        height: '150px'
                                    }}>
                                        <Typography
                                            variant="Body-Large"
                                            style={{
                                                color: '#FFFFFF',
                                                fontFamily: 'Gilroy, sans-serif',
                                                fontWeight: 500,
                                                fontSize: '16px',
                                                lineHeight: '18px',
                                            }}>
                                            {post.post.text}
                                        </Typography>
                                    </div>
                                    <div style={{display: 'flex'}}>
                                        <div className="item" style={{
                                            marginLeft: '200px',
                                            marginTop: '90px',
                                            width: '200px',
                                            height: '50px'
                                        }}>
                                            <Typography
                                                variant="Body-Large"
                                                style={{
                                                    color: '#FFFFFF',
                                                    fontFamily: 'Gilroy, sans-serif',
                                                    fontWeight: 600,
                                                    fontSize: '16px',
                                                    lineHeight: '18px',
                                                }}>
                                                дата
                                                загрузки {new Date(post.post.uploadDate).toLocaleDateString('ru-RU')}
                                            </Typography>
                                        </div>
                                        <div className="item" style={{
                                            marginLeft: '50px',
                                            marginTop: '84px',
                                            width: '200px',
                                            height: '30px',
                                            display:'flex'
                                        }}>
                                            <Typography
                                                variant="Body-Large"
                                                style={{
                                                    color: '#FFFFFF',
                                                    fontFamily: 'Gilroy, sans-serif',
                                                    fontWeight: 600,
                                                    fontSize: '16px',
                                                    lineHeight: '18px',
                                                }}>
                                                {post.rate} классов <img src={pog} alt="pog" style={{width: '30px', height: '30px', verticalAlign: 'middle' }}/>
                                            </Typography>
                                        </div>
                                    </div>
                                </div>
                            </div>))}
                    </div>
                </div>)}
            </div>
        </div>
    </>);
};

export default AuthorPage;