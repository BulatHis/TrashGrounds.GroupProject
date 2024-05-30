import SingIn from './Pages/SingIn'
import SingUp from './Pages/SingUp'
import Header from './Pages/Header'
import MainPage from './Pages/MainPage'
import Posts from './Pages/Posts'
import Search from './Pages/Search'
import NotFound from './Pages/NotFound'
import MusicPage from './Pages/MusicPage'
import AuthorPage from './Pages/AuthorPage'
import AccountPage from './Pages/AccountPage'
import CreatePost from './Pages/CreatePost'
import CreateTrack from './Pages/CreateTrack'
import {BrowserRouter as Router, Route, Routes} from 'react-router-dom';

function App() {
    return (
        <div>
            <Router>
                <Header />
                <Routes>
                    <Route path="/SingIn" element={<SingIn/>}/>
                    <Route path="/Search" element={<Search/>}/>
                    <Route path="/CreateTrack" element={<CreateTrack/>}/>
                    <Route path="/CreatePost" element={<CreatePost/>}/>
                    <Route path="/Posts" element={<Posts/>}/>
                    <Route path="/" element={<MainPage/>}/>
                    <Route path="/SingUp" element={<SingUp/>}/>
                    <Route path="/AccountPage" element={<AccountPage/>}/>
                    <Route path="/Author/:authorId" element={<AuthorPage/>}/>
                    <Route path="/MusicPage/:trackId" element={<MusicPage/>}/>
                    <Route path="*" element={<NotFound/>}/>
                </Routes>
            </Router>
        </div>
    );
}

export default App;
