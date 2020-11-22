import axios from 'axios';

const getMovieListAsync = async () => {
try{
    var itemList = await axios.get('/api/v1/movie/list');

    return itemList;
}catch(e)
    {
        //TODO: send back and error code so calling code can display the appropriate error toast to the user
        console.log("Failed to fetch items");
    }
return [{}];
}

const MovieApi = {
    getMovieListAsync: getMovieListAsync
}

export default MovieApi;
