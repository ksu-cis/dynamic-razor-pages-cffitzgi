using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public static class MovieDatabase
    {
        private static List<Movie> movies = new List<Movie>();

        /// <summary>
        /// Loads the movie database from the JSON file
        /// </summary>
        static MovieDatabase() {    // Static, so can not be instanciated as an object, but is essentially called once.
            
            using (StreamReader file = System.IO.File.OpenText("movies.json"))
            {
                string json = file.ReadToEnd();
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            }

            HashSet<string> genreSet = new HashSet<string>();
            foreach (Movie movie in movies)
            {
                if (movie.MajorGenre != null)
                    genreSet.Add(movie.MajorGenre);
            }
            genres = genreSet.ToArray();
        }

        /// <summary>
        /// Gets all the movies in the database
        /// </summary>
        public static IEnumerable<Movie> All { get { return movies; } }

        /// <summary>
        /// Searches the database for matching movie.
        /// </summary>
        /// <param name="terms">Search terms</param>
        /// <returns>Enumeration of movies which include the search term</returns>
        public static IEnumerable<Movie> Search(string terms)
        {
            List<Movie> results = new List<Movie>();

            if (terms == null) return All;
            foreach(Movie movie in All)
            {
                if (movie.Title != null && movie.Title.Contains(terms, StringComparison.InvariantCultureIgnoreCase))
                    results.Add(movie);
            }
            return results;
        }

        /// <summary>
        /// Gets the possible MPAARatings
        /// </summary>
        public static string[] MPAARating
        {
            get => new string[]
            {
                "G",
                "PG",
                "PG-13",
                "R",
                "NC-17"
            };
        }

        /// <summary>
        /// Filters provided collection of movie by MPAA rating
        /// </summary>
        /// <param name="movies">List of movies to filter</param>
        /// <param name="ratings">Ratings to include</param>
        /// <returns></returns>
        public static IEnumerable<Movie> FilterByMPAARating(IEnumerable<Movie> movies, IEnumerable<string> ratings)
        {
            List<Movie> results = new List<Movie>();

            if (ratings == null || ratings.Count() == 0) return movies;

            foreach(Movie movie in movies)
            {
                if (movie.MPAARating != null && ratings.Contains(movie.MPAARating))
                    results.Add(movie);
            }

            return results;
        }

        public static IEnumerable<Movie> FilterByGenre(IEnumerable<Movie> movies, IEnumerable<string> genreList)
        {
            List<Movie> results = new List<Movie>();

            if (genreList == null || genreList.Count() == 0) return movies;

            foreach (Movie movie in movies)
            {
                if (movie.MajorGenre != null && genreList.Contains(movie.MajorGenre))
                    results.Add(movie);
            }

            return results;
        }

        public static IEnumerable<Movie> FilterByIMDBRating(IEnumerable<Movie> movies, double? min, double? max)
        {
            List<Movie> results = new List<Movie>();

            if ((min == null || min <= 0.0) && (max == null || max >= 10.0)) return movies;

            if (min == null)
            {
                foreach (Movie movie in movies)
                {
                    if (movie.IMDBRating <= max)
                        results.Add(movie);
                }
            }
            else if (max == null)
            {
                foreach (Movie movie in movies)
                {
                    if (min <= movie.IMDBRating)
                        results.Add(movie);
                }
            }
            else
            {
                foreach (Movie movie in movies)
                {
                    if (min <= movie.IMDBRating && movie.IMDBRating <= max)
                        results.Add(movie);
                }
            }

            return results;
        }

        public static IEnumerable<Movie> FilterByRottenTomatoesRating(IEnumerable<Movie> movies, double? min, double? max)
        {
            List<Movie> results = new List<Movie>();

            if ((min == null || min <= 0) && (max == null || max >= 100)) return movies;

            if (min == null)
            {
                foreach (Movie movie in movies)
                {
                    if (movie.RottenTomatoesRating <= max)
                        results.Add(movie);
                }
            }
            else if (max == null)
            {
                foreach (Movie movie in movies)
                {
                    if (min <= movie.RottenTomatoesRating)
                        results.Add(movie);
                }
            }
            else
            {
                foreach (Movie movie in movies)
                {
                    if (min <= movie.RottenTomatoesRating && movie.RottenTomatoesRating <= max)
                        results.Add(movie);
                }
            }

            return results;
        }

        /// <summary>
        /// genre represented in database
        /// </summary>
        private static string[] genres;
        /// <summary>
        /// Gets the movie genres represented in the database.
        /// </summary>
        public static string[] Genres => genres;
        
    }
}
