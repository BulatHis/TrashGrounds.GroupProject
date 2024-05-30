import Button from '@mui/material/Button';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import CssBaseline from '@mui/material/CssBaseline';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import {Link} from 'react-router-dom';
import {createTheme, ThemeProvider} from '@mui/material/styles';
import React, {useEffect, useState} from 'react';
import dogCoin from './dogCoin.png'
import pop from './popular.png'
import trend from './trend.png'
import NEW from './new.png'
import pog from './pog.png'


const defaultTheme = createTheme();

function TrackCard({track}) {
    return (
        <div style={{width: '180px', height: '260px', backgroundColor: '#262626'}}>
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
        </div>
    );
}

export default function MainPage() {


    const [tracksTrend, setTracksTrend] = useState([]);
    const [tracksNew, setTracksNew] = useState([]);
    const [tracksPop, setTracksPop] = useState([]);

    useEffect(() => {
        document.body.style.backgroundColor = '#161616';
        const fetchData = async () => {
            try {
                const responseTrend = await fetch('https://localhost:8000/track/MostStreaming?take=6&skip=0');
                if (!responseTrend.ok) {
                    throw new Error('Ошибка при получении данных1');
                }
                const dataTrend = await responseTrend.json();

                const responseNew = await fetch('https://localhost:8000/track/New?take=6&skip=0');
                if (!responseNew.ok) {
                    throw new Error('Ошибка при получении данных2');
                }
                const dataNew = await responseNew.json();

                const responsePop = await fetch('https://localhost:8000/track/MostStreaming?take=6&skip=0');
                if (!responsePop.ok) {
                    throw new Error('Ошибка при получении данных3');
                }
                const dataPop = await responsePop.json();
                

                // Проверяем структуру данных
                if (!Array.isArray(dataTrend) || !dataTrend.every(item => item.trackInfo ||
                    !Array.isArray(dataNew) || !dataNew.every(item => item.trackInfo ||
                        !Array.isArray(dataPop) || !responsePop.every(item => item.trackInfo )))) {
                    throw new Error('Неверный формат данных');
                }

                setTracksTrend(dataTrend);
                setTracksNew(dataNew);
                setTracksPop(dataPop);
            } catch (error) {
                console.error('Ошибка при получении данных:', error);
            }
        };

        fetchData();
    }, []);


    return (
        <>
            <div style={{marginLeft: '370px', marginRight: '370px', display: 'flex', marginTop: '50px'}}>
                <div style={{width: '880px', height: '200px', backgroundColor: '#162632'}}>
                    <div
                        style={{
                            marginLeft: '45px',
                            marginTop: '30px',
                            width: '425px',
                            height: '90px',
                            overflow: 'hidden'
                        }}>
                        <Typography variant="body1" style={{
                            color: '#FFFFFF',
                            fontFamily: 'Gilroy, sans-serif',
                            fontWeight: 520,
                            fontSize: '24px',
                            lineHeight: '30px'
                        }}>
                            trash grounds - лучшая платформа для публикации трэшовых мэшапов и гачи-ремиксов.
                        </Typography>
                    </div>
                    <div
                        style={{
                            marginLeft: '45px',
                            marginTop: '10px',
                            width: '565px',
                            height: '40px',
                            overflow: 'hidden'
                        }}>
                        <Typography variant="body2" style={{
                            color: '#FFFFFF',
                            fontFamily: 'Gilroy, sans-serif',
                            fontWeight: 100,
                            fontSize: '16px',
                            lineHeight: '20px'
                        }}>
                            Вы можете отпрвить свои three hundred DOGE на адрес DA2GabbJUcFDWPzPZFw2AyfTaioDMwZAHr
                            чтобы
                            поддержать проект
                        </Typography>
                    </div>
                </div>
                {/* Отступ справа от поля */}
                <div style={{marginRight: '20px'}}/>
                {/* Изображение размером 280x224px */}
                <img src={dogCoin} alt="dogCoin" style={{width: '280px', height: '200px'}}/>
            </div>

            <div style={{marginLeft: '370px', marginRight: '370px', marginTop: '50px', display: 'flex'}}>
                {/* Ваше содержимое для нового отсека здесь */}
                <div style={{marginLeft: '8px'}}/>
                {/* Изображение размером 32 */}
                <img src={trend} alt="dogCoin" style={{width: '32px', height: '32px'}}/>
                <div style={{
                    marginLeft: '10px',
                    marginTop: '2px',
                    width: '160px',
                    height: '30px',
                    overflow: 'hidden'
                }}>
                    <Typography variant="body3" style={{
                        color: '#FFFFFF',
                        fontFamily: 'Gilroy, sans-serif',
                        fontWeight: 700,
                        fontSize: '24px',
                        lineHeight: '30px'
                    }}>
                        В тренде
                    </Typography>
                </div>
                <div style={{marginLeft: '875px', marginTop: '10px'}}>
                    <Typography variant="body3" style={{
                        color: '#FFFFFF',
                        fontFamily: 'Gilroy, sans-serif',
                        fontWeight: 100,
                        fontSize: '16px',
                        lineHeight: '30px'
                    }}>
                        больше>
                    </Typography>
                </div>
            </div>

            <div style={{
                marginLeft: '370px',
                marginRight: '370px',
                marginTop: '20px', display: 'flex', justifyContent: 'space-between'
            }}>
                {/*в тренде*/}

                {tracksTrend.map(tracksTrend => (
                    <Link to={`/MusicPage/${tracksTrend.trackInfo.id}`} key={tracksTrend.trackInfo.id}>
                        <TrackCard track={tracksTrend}/>
                    </Link>
                ))}
            </div>

            <div style={{marginLeft: '370px', marginRight: '370px', marginTop: '50px', display: 'flex'}}>
                {/* Ваше содержимое для нового отсека здесь */}
                <div style={{marginLeft: '8px'}}/>
                {/* Изображение размером 32 */}
                <img src={NEW} alt="dogCoin" style={{width: '32px', height: '32px'}}/>
                <div style={{
                    marginLeft: '10px',
                    marginTop: '2px',
                    width: '160px',
                    height: '30px',
                    overflow: 'hidden'
                }}>
                    <Typography variant="body3" style={{
                        color: '#FFFFFF',
                        fontFamily: 'Gilroy, sans-serif',
                        fontWeight: 700,
                        fontSize: '24px',
                        lineHeight: '30px'
                    }}>
                        Новое
                    </Typography>
                </div>
                <div style={{marginLeft: '875px', marginTop: '10px'}}>
                    <Typography variant="body3" style={{
                        color: '#FFFFFF',
                        fontFamily: 'Gilroy, sans-serif',
                        fontWeight: 100,
                        fontSize: '16px',
                        lineHeight: '30px'
                    }}>
                        больше>
                    </Typography>
                </div>
            </div>

            <div style={{
                marginLeft: '370px',
                marginRight: '370px',
                marginTop: '20px', display: 'flex', justifyContent: 'space-between'
            }}>
                {/*в новое*/}

                {tracksNew.map(tracksNew => (
                    <Link to={`/MusicPage/${tracksNew.trackInfo.id}`} key={tracksNew.trackInfo.id}>
                        <TrackCard track={tracksNew}/>
                    </Link>
                ))}

            </div>

            <div style={{marginLeft: '370px', marginRight: '370px', marginTop: '50px', display: 'flex'}}>
                {/* Ваше содержимое для нового отсека здесь */}
                <div style={{marginLeft: '8px'}}/>
                {/* Изображение размером 32 */}
                <img src={pop} alt="dogCoin" style={{width: '32px', height: '32px'}}/>
                <div style={{
                    marginLeft: '10px',
                    marginTop: '2px',
                    width: '160px',
                    height: '30px',
                    overflow: 'hidden'
                }}>
                    <Typography variant="body3" style={{
                        color: '#FFFFFF',
                        fontFamily: 'Gilroy, sans-serif',
                        fontWeight: 700,
                        fontSize: '24px',
                        lineHeight: '30px'
                    }}>
                        Популярное
                    </Typography>
                </div>
                <div style={{marginLeft: '875px', marginTop: '10px'}}>
                    <Typography variant="body3" style={{
                        color: '#FFFFFF',
                        fontFamily: 'Gilroy, sans-serif',
                        fontWeight: 100,
                        fontSize: '16px',
                        lineHeight: '30px',
                    }}>
                        больше>
                    </Typography>
                </div>
            </div>

            <div style={{
                marginLeft: '370px',
                marginRight: '370px',
                marginTop: '20px', display: 'flex', justifyContent: 'space-between'
            }}>
                {/* популярное */}

                {tracksPop.map(tracksPop => (
                    <Link to={`/MusicPage/${tracksPop.trackInfo.id}`} key={tracksPop.trackInfo.id}>
                        <TrackCard track={tracksPop}/>
                    </Link>
                ))}
            </div>
            
        </>
    );
}
            