import '../styles/App.css';
import MovieList from './movieList/MovieList';
import movieApi from '../services/MovieApi';
import {useState, useEffect} from 'react';
import { config } from '@fortawesome/fontawesome-svg-core'

config.autoA11y = true

function App() {
  const [movies, setMovies] = useState([{}]);
  const [isFetching, setIsFetching] = useState(true);

  useEffect(() => {
    movieApi.getMovieListAsync().then((response) => {
      if(response.status === 200)
      {
        setMovies(response.data.movies);
        setIsFetching(false);
      }
    });
  }, [])

  return (
    <div className="App">
        <MovieList movies={movies} isFetching={isFetching}></MovieList>
    </div>
  );
}

export default App;
