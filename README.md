# WebJetMovies

## Introduction
The WebJetMovies WepApp is meant for those people whome are interesting in finding the cheapest movies available from various different stores. So if you like cheap movies then this is for you.

### Project sturcure

#### WebJetMovie.UI
Contains all the ReactJS code that will get built in the build pipeline and copied into the wwwroot folder of the Api project which is the backend and responsible for service up the site static files.


#### WebJetMovies.Api
Is written in .Net 5 and contains the logic for retrieving movies from various sources and returning the cheapest ones to the user.

The main folders within this project are:

**Api** - Contains all the Api specific items such as controllers, wwwroot folder and middleware

**Application** - This contains all the orchestration logic that calls various services and performs processing on data returned.

**Infrastucture** - In this layer we have logic to call external services. 

**Domain** - Here is where the domain models live and any domain rules should be applied here.

**Common** - Anything that can be shared between layers but this folder must not have a dependency on any other layer.

##### API Settings
The api project can take in the following environment variables:

**Api:AccessToken** - The access token of the movie Api </br>
**Api:ApiServerAddress** - The server that hosts the Apis  </br>
**Api:MovieApiUrls:GetCinemaworldMovieList** - The endpoint that contains the list of cinemaWorld movies  </br>
**Api:MovieApiUrls:GetFilmworldMovieList** - The endpoint that contains the list of filmworld movies  </br>
**Api:MovieApiUrls:GetCinemaworldMovieDetail** - The endpoint that returns the CinemaWold movie details  </br>
**Api:MovieApiUrls:GetFilmworldMovieDetail** - The endpoint that returns the FilmWorld movie details  </br>

**Api:ApiRequestDetails:ApiRequestTimeOutInSeconds** - Int that specidfies how many seconds to wait before timing out a request (polly will retry the request on timeouts)  </br>
**Api:ApiRequestDetails:MaxDurationOfClientRequest** - Int that specifies the max amount of seconds that a request has to complete including retries. 
