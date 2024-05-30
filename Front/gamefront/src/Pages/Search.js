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


const Search = () => {
    document.body.style.backgroundColor = '#161616';

    const [authorInfo, setAuthorInfo] = useState([]);
    const [sameAuthor, setSameAuthor] = useState([]);

    const [status, setStatus] = useState();

    const handleChange = (event) => {
        setStatus(event.target.value);
    };

    const handleClick1 = async () => {
        try {
            const responseSameData = await fetch(`https://localhost:8000/track/search?query=${status}&dto=null&take=10&skip=0`);
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

    return (<>
        <div style={{display: 'flex'}}>
            <div>
                <div style={{marginTop: '50px', marginLeft: '370px',}}>
                    <Typography
                        variant="Body-Large"
                        style={{
                            color: '#FFFFFF',
                            fontFamily: 'Gilroy, sans-serif',
                            fontWeight: 600,
                            fontSize: '16px',
                            lineHeight: '20px',
                        }}>
                        Введите название трека
                    </Typography>
                </div>

                <div style={{marginTop: '20px', marginLeft: '370px',}}>
                <textarea
                    style={{
                        flex: '1',
                        height: '50px',
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
            </div>

            <div style={{display: 'flex', flexDirection: 'column', marginTop: '52px', marginLeft: '370px',}}>
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
                        Найти
                    </div>
                </Button>
            </div>

        </div>


        <div style={{marginTop: '70px', marginLeft: '370px'}}>
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
                <hr style={{width: '1155px', height: '0.5px', backgroundColor: '#808080', marginLeft: '0px'}}/>

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
        </div>
    </>);
};

export default Search;