import React, {useEffect, useState} from 'react';
import {Link, useParams} from 'react-router-dom';
import dogCoin from "./dogCoin.png";
import Typography from "@mui/material/Typography";
import pog from './pog.png'
import './Player.css'
import TextField from '@mui/material/TextField';

import ReactAudioPlayer from 'react-audio-player';
import trend from "./trend.png";

function TrackCard({track}) {
    return (<div style={{width: '180px', height: '260px', backgroundColor: '#262626'}}>
        <div>
            <img src={`https://localhost:8000/image/${track.trackInfo.pictureId}`} alt="Avatar"
                 style={{width: '180px', height: '180px'}}/>
        </div>
        <div style={{marginLeft: '15px', marginTop: '8px'}}>
            <Typography variant="body4" style={{
                color: '#FFFFFF',
                fontFamily: 'Gilroy, sans-serif',
                fontWeight: 100,
                fontSize: '16px',
                lineHeight: '20px'
            }}>
                {track.trackInfo.title}
            </Typography>
        </div>
        <div style={{
            display: 'flex',
            justifyContent: 'space-between',
            alignItems: 'center',
            marginLeft: '15px',
            marginRight: '10px',
            marginBottom: '5px'
        }}>
            <Typography variant="body4" style={{
                color: '#FFFFFF',
                fontFamily: 'Gilroy, sans-serif',
                fontWeight: 100,
                fontSize: '14px',
                lineHeight: '18px'
            }}>
                {track.userInfo.nickname}
            </Typography>
            <div style={{display: 'flex', alignItems: 'center'}}>
                <Typography variant="body5" style={{
                    color: '#FFFFFF',
                    fontFamily: 'Gilroy, sans-serif',
                    fontWeight: 100,
                    fontSize: '14px',
                    lineHeight: '18px'
                }}>
                    {track.rate.rating}
                </Typography>
                <img src={pog} alt="pog" style={{width: '16px', height: '16px', marginLeft: '2px'}}/>
            </div>
        </div>
    </div>);
}

const MusicPage = () => {
        document.body.style.backgroundColor = '#161616';
        const {trackId} = useParams(); // Получаем id трека из параметров маршрута
        const [trackInfo, setTrackInfo] = useState([]);
        const [sameAuthor, setSameAuthor] = useState([]);
        const [commentInfo, setCommentInfo] = useState([]);

        // Загрузка данных о треке с использованием id
        useEffect(() => {
            const fetchData = async () => {
                try {
                    const responseTrackData = await fetch(`https://localhost:8000/track/${trackId}`);
                    if (!responseTrackData.ok) {
                        throw new Error('Ошибка при получении данных');
                    }
                    const trackData = await responseTrackData.json();
                    if (!trackData || !trackData.track) {
                        throw new Error('Неверный формат данных');
                    }
                    setTrackInfo(trackData);
                } catch (error) {
                    console.error('Ошибка при получении данных трека:', error);
                }
            };

            const fetchSameData = async () => {
                try {
                    const responseSameData = await fetch(`https://localhost:8000/track/from-user/${trackInfo.userInfo.id}?take=4&exclude-track-id=null&skip=0`);
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

            const fetchCommentData = async () => {
                try {
                    const responseCommentData = await fetch(`https://localhost:8000/comments/track/${trackId}?take=5&exclude-track-id=null&skip=0`);
                    if (!responseCommentData.ok) {
                        throw new Error('Ошибка при получении данных');
                    }
                    const commentData = await responseCommentData.json();
                    if (!commentData || !Array.isArray(commentData)) {
                        throw new Error('Неверный формат данных');
                    }
                    setCommentInfo(commentData);
                } catch (error) {
                    console.error('Ошибка при получении данных от пользователя:', error);
                }
            };

            fetchData();
            if (trackInfo && trackInfo.userInfo) {
                fetchSameData();
                fetchCommentData();
            }
            
        }, [trackId, trackInfo]);

        const listensCount = trackInfo && trackInfo.rate ? Math.round(trackInfo.rate.rating) : 0;

        const images = [];
        for (let i = 0; i < listensCount; i++) {
            images.push(<img
                key={i}
                src={pog}
                alt="pog"
                style={{
                    width: '40px', // Пример ширины
                    height: '40px', // Пример высоты
                    marginRight: '-20px', // Пример отступа справа между изображениями
                }}
            />);
        }

        const accessToken = localStorage.getItem('accessToken');
        const refreshToken = localStorage.getItem('refreshToken');

        const [comment, setComment] = useState('');

        const handleKeyPress = async (event) => {
            if (event.key === 'Enter' && event.shiftKey) {
                try {
                    const response = await fetch(`https://localhost:8000/comments/add/track/${trackId}`, {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                            'Authorization': `Bearer ${accessToken}`
                        },
                        body: JSON.stringify({
                            Message: comment,
                            ReplyTo: null
                        })
                    });

                    if (!response.ok) {
                        console.log('Обновляем токен');
                        const refreshResponse = await fetch('https://localhost:8000/auth/refresh',
                            {
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

                        const { accessToken: newAccessToken, refreshToken: newRefreshToken } = await refreshResponse.json();

                        // Сохраняем новые значения токенов в localStorage
                        localStorage.setItem('accessToken', newAccessToken);
                        localStorage.setItem('refreshToken', newRefreshToken);

                        // Повторный запрос с обновленным accessToken
                        const retryResponse = await fetch(`https://localhost:8000/comments/add/track/${trackId}`, {
                            method: 'POST',
                            headers: {
                                'Content-Type': 'application/json',
                                'Authorization': `Bearer ${newAccessToken}`
                            },
                            body: JSON.stringify({
                                Message: comment,
                                ReplyTo: null
                            })
                        });

                        if (!retryResponse.ok) {
                            throw new Error('Ошибка при отправке комментария после обновления токена');
                        }
                    } 
                } catch (error) {
                    console.error('Ошибка при отправке комментария:', error);
                }
            }
        };


        const handleChange = (event) => {
            setComment(event.target.value);
        };


        return (<>
            <div style={{marginLeft: '370px', marginRight: '370px', display: 'flex', marginTop: '50px'}}>
                {trackInfo.track && (<img src={`https://localhost:8000/image/${trackInfo.track.pictureId}`} alt="trackPic"
                                          style={{width: '280px', height: '280px'}}/>)}
                <div style={{
                    display: 'flex', flexDirection: 'column', width: '880px', height: '280px', backgroundColor: '#262626'
                }}>
                    <div style={{
                        display: 'flex', height: '40px', marginTop: '17px', marginLeft: '20px'
                    }}>
                        {trackInfo.track && `https://localhost:8000/image/${trackInfo.track.isExplicit}` && (
                            <div style={{width: '24px', height: '24px', backgroundColor: '#393939', marginTop: '8px'}}>
                                <Typography variant="body1" style={{
                                    marginLeft: '8px',
                                    marginTop: '3px',
                                    color: '#FFFFFF',
                                    fontFamily: 'Gilroy, sans-serif',
                                    fontWeight: 520,
                                    fontSize: '14px',
                                    lineHeight: '18px',
                                    letterSpacing: '0.16px'
                                }}>
                                    E
                                </Typography>
                            </div>)}
                        <div style={{marginLeft: '10px'}}>
                            {trackInfo.track && (<Typography variant="body2" style={{
                                color: '#FFFFFF',
                                fontFamily: 'Gilroy, sans-serif',
                                fontWeight: 700,
                                fontSize: '32px',
                                lineHeight: '40px',
                            }}>
                                {trackInfo.track.title}
                            </Typography>)}
                        </div>
                        <div style={{height: '20px', marginTop: '13px', marginLeft: '30px'}}>
                            {trackInfo.userInfo && (<Typography variant="Body-Large" style={{
                                color: '#FFFFFF',
                                fontFamily: 'Gilroy, sans-serif',
                                fontWeight: 600,
                                fontSize: '16px',
                                lineHeight: '20px',
                            }}>
                                by <Link to={`/Author/${trackInfo.userInfo.id}`} style={{
                                textDecoration: 'none', color: '#FFFFFF'
                            }}>{trackInfo.userInfo.nickname}</Link>
                            </Typography>)}
                        </div>

                        <div style={{height: '32px', marginTop: '7px', marginLeft: '10px',}}>
                            {trackInfo.track && (
                                <img src={`https://localhost:8000/image/${trackInfo.userInfo.avatarId}`} alt="Avatar"
                                     style={{width: '32px', height: '32px'}}/>)}
                        </div>
                    </div>


                    <div style={{
                        display: 'flex', height: '40px', marginTop: '-35px', marginLeft: 'auto', marginRight: '35px'
                    }}>
                        <div style={{height: '20px', marginTop: '10px', marginRight: '4px',}}>
                            {trackInfo.track && trackInfo.rate && (<Typography variant="Body-Large" style={{
                                color: '#FFFFFF',
                                fontFamily: 'Gilroy, sans-serif',
                                fontWeight: 600,
                                fontSize: '16px',
                                lineHeight: '20px',
                            }}>
                                {trackInfo.rate.rating} ({trackInfo.track.listensCount})
                            </Typography>)}
                        </div>

                        <div style={{flexDirection: 'row',}}>
                            {images}
                        </div>

                    </div>

                    <div style={{height: '20px', marginTop: '15px', marginRight: '30px', marginLeft: 'auto'}}>
                        {trackInfo.track && trackInfo.rate && (<Typography variant="Body-Large" style={{
                            color: '#FFFFFF',
                            fontFamily: 'Gilroy, sans-serif',
                            fontWeight: 600,
                            fontSize: '16px',
                            lineHeight: '20px',
                        }}>
                            {trackInfo.track.listensCount} прослушиваний
                        </Typography>)}
                    </div>

                    <div style={{height: '108px', width: '480px', marginTop: '-20px', marginLeft: '20px'}}>
                        {trackInfo.track && trackInfo.rate && (<Typography variant="Body-Large" style={{
                            color: '#FFFFFF',
                            fontFamily: 'Gilroy, sans-serif',
                            fontWeight: 500,
                            fontSize: '14px',
                            lineHeight: '18px',
                        }}>
                            {trackInfo.track.description}
                        </Typography>)}
                    </div>

                    <div style={{height: '15px', marginTop: '20px', marginLeft: '20px'}}>
                        {trackInfo.track && trackInfo.rate && (<Typography variant="Body-Large" style={{
                            color: '#FFFFFF',
                            fontFamily: 'Gilroy, sans-serif',
                            fontWeight: 500,
                            fontSize: '12px',
                            lineHeight: '15px',
                        }}>
                            {trackInfo.track.genres.map(genre => genre.name).join(', ')}
                        </Typography>)}
                    </div>


                    <div style={{height: '50px', width: '900px', display: 'flex'}}>

                        <div className="music-player" style={{
                            height: '30px', width: '800px', marginTop: '-15px', backgroundColor: 'rgba(51, 51, 51, 0)'
                        }}>
                            {trackInfo.track && (
                                <audio controls>
                                    <source src={`https://localhost:8000/music/${trackInfo.track.musicId}`} type="audio/mpeg" />
                                    Your browser does not support the audio element.
                                </audio>
                            )}
                        </div>


                        <div style={{marginTop: '20px', marginLeft: '20px', marginRight: '40px'}}>
                            {trackInfo.track && (<a href={`https://localhost:8000/music/${trackInfo.track.musicId}`}
                                                    download={`${trackInfo.track.title}.mp3`} style={{
                                color: '#78A9FF',
                                fontFamily: 'Gilroy, sans-serif',
                                fontWeight: 500,
                                fontSize: '14px',
                                lineHeight: '18px',
                                textDecoration: 'none' // Убираем подчеркивание ссылки
                            }}>
                                Скачать
                            </a>)}
                        </div>
                    </div>
                </div>
            </div>

            <div style={{marginLeft: '370px', marginRight: '370px', display: 'flex', marginTop: '50px'}}>
                <Typography variant="Body-Large" style={{
                    color: '#FFFFFF',
                    fontFamily: 'Gilroy, sans-serif',
                    fontWeight: 700,
                    fontSize: '24px',
                    lineHeight: '30px',
                }}>
                    Комментарии
                </Typography>

                <div style={{marginLeft: '620px'}}>
                    <Typography variant="Body-Large" style={{
                        color: '#FFFFFF',
                        fontFamily: 'Gilroy, sans-serif',
                        fontWeight: 700,
                        fontSize: '24px',
                        lineHeight: '30px',
                    }}>
                        От этого автора
                    </Typography>
                </div>
            </div>

            <div style={{marginLeft: '370px', marginRight: '870px', marginTop: '25px'}}>
                <div style={{display: 'flex',}}>
                      <textarea
                          placeholder="Введите комментарий"
                          style={{
                              flex: '1',
                              height: '96px',
                              backgroundColor: '#262626',
                              border: 'none',
                              resize: 'none',
                              color: '#FFFFFF'
                          }}
                          value={comment}
                          onChange={handleChange}
                          onKeyDown={handleKeyPress}
                      />
                </div>


                <div style={{marginTop: '45px', width: '575px', height: '80px'}}>
                    {commentInfo.map((comment, index) => (
                        <div className="track-item" key={index}
                             style={{display: 'flex', marginTop: '30px'}}>
                            <div>
                                <img src={`https://localhost:8000/image/${comment.userInfo.avatarId}`}
                                     alt="authorPic"
                                     style={{width: '80px', height: '80px'}}/>
                            </div>

                            <div>
                                <div style={{display: 'flex'}}>
                                    <div className="item" style={{marginLeft: '20px'}}>
                                        <Typography
                                            variant="Body-Large"
                                            style={{
                                                color: '#FFFFFF',
                                                fontFamily: 'Gilroy, sans-serif',
                                                fontWeight: 600,
                                                fontSize: '16px',
                                                lineHeight: '20px',
                                            }}>
                                            {comment.userInfo.nickname}
                                        </Typography>
                                    </div>

                                    <div className="item" style={{marginLeft: '20px'}}>
                                        <Typography
                                            variant="Body-Large"
                                            style={{
                                                color: '#FFFFFF',
                                                fontFamily: 'Gilroy, sans-serif',
                                                fontWeight: 500,
                                                fontSize: '12px',
                                                lineHeight: '15px',
                                            }}>
                                            {new Date(comment.comment.sendAt).toLocaleDateString('ru-RU')}
                                        </Typography>
                                    </div>
                                </div>

                                <div className="item" style={{marginLeft: '20px', marginTop: '15px', width: '475px'}}>
                                    <Typography
                                        variant="Body-Large"
                                        style={{
                                            color: '#FFFFFF',
                                            fontFamily: 'Gilroy, sans-serif',
                                            fontWeight: 500,
                                            fontSize: '16px',
                                            lineHeight: '18px',
                                        }}>
                                        {comment.comment.message}
                                    </Typography>
                                </div>
                            </div>
                        </div>))}
                </div>
            </div>


            <div style={{marginLeft: '1150px', marginTop: '-235px'}}>
                <div style={{display: 'flex'}}>
                    {sameAuthor.slice(0, 2).map(sameAuthor => (<React.Fragment key={sameAuthor.trackInfo.id}>
                        <Link to={`/MusicPage/${sameAuthor.trackInfo.id}`}>
                            <TrackCard track={sameAuthor}/>
                        </Link>
                        <div style={{marginRight: '20px'}}/>
                    </React.Fragment>))}
                </div>
                <div style={{display: 'flex', marginTop: '20px'}}>
                    {sameAuthor.slice(2, 4).map(sameAuthor => (<React.Fragment key={sameAuthor.trackInfo.id}>
                        <Link to={`/MusicPage/${sameAuthor.trackInfo.id}`}>
                            <TrackCard track={sameAuthor}/>
                        </Link>
                        <div style={{marginRight: '20px'}}/>
                    </React.Fragment>))}
                </div>
            </div>
        </>);
    }
;

export default MusicPage;