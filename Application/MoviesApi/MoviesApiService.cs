using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.MoviesApi
{
    public class MoviesApiService
    {
        private readonly List<string> _moviesTitlesTemplated;
        private readonly string _api;
        public MoviesApiService(string api)
        {
            _api = api;
            _moviesTitlesTemplated = new List<string>()
            {
                "Matrix",
                "Misery",
                "Terminator"
            };
        }
        public MoviesApiService(string api, List<string> titles) 
        {
            _api = api;
            _moviesTitlesTemplated = titles;
        }
        private async Task<string?> GetFirstMovieImdb(HttpClient client, string requestPath)
        {
            try
            {
                var response = await client.GetAsync(requestPath);
                var responseString = await response.Content.ReadAsStringAsync();
                JsonDocument jd = JsonDocument.Parse(responseString);
                JsonElement je = jd.RootElement.GetProperty("Search")[0];
                return je.GetProperty("imdbID").GetString();
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<MovieData>> LoadSomeMovies()
        {
            var movies = new List<MovieData>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    foreach (var movieTitle in _moviesTitlesTemplated)
                    {
                        var imdb = await GetFirstMovieImdb(client, $"http://www.omdbapi.com/?s={movieTitle}&plot=full&&apikey={_api}");
                        if (imdb == null)
                            continue;
                        var response = client.Send(new HttpRequestMessage(HttpMethod.Get,
                            $"http://www.omdbapi.com/?i={imdb}&apikey={_api}"));
                        var responseString = await response.Content.ReadAsStringAsync();
                        JsonDocument jd = JsonDocument.Parse(responseString);
                        JsonElement root = jd.RootElement;
                        var title = root.GetProperty("Title").GetString();
                        var poster = root.GetProperty("Poster").GetString();
                        var year = root.GetProperty("Year").GetString();
                        var genre = root.GetProperty("Genre").GetString();
                        var director = root.GetProperty("Director").GetString();
                        var desc = root.GetProperty("Plot").GetString();
                        var movie = new MovieData(title!, desc!, int.Parse(year!), director!, genre!, poster);
                        movies.Add(movie);
                    }
                }
            }
            catch
            {
            }
            return movies;
        }
    }
}
