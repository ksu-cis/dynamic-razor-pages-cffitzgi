using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// List of filtered movies
        /// </summary>
        public IEnumerable<Movie> Movies { get; protected set; }

        /// <summary>
        /// Search terms for filtering movies
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string SearchTerms { get; set; }

        /// <summary>
        /// Filtered MPAARatings
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string[] MPAARatings { get; set; }
        
        /// <summary>
        /// Filtered genres
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string[] Genres { get; set; }

        /// <summary>
        /// Minimum IMDB Rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? IMDBMin { get; set; }
        /// <summary>
        /// Maximum IMDB Rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? IMDBMax { get; set; }

        /// <summary>
        /// Minimum Rotten Tomatoes
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public int? RTMin { get; set; }

        /// <summary>
        /// Maximum rotten tomatoes rating.
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public int? RTMax { get; set; }

        /// <summary>
        ///On search.
        /// </summary>
        public void OnGet(/*double? imdbMin, double? imdbMax, int? rtMin, int? rtMax*/)
        {
            Movies = MovieDatabase.All;
            if (SearchTerms != null)
            {
                Movies = Movies.Where(movie => movie.Title != null && movie.Title.Contains(SearchTerms, StringComparison.CurrentCultureIgnoreCase));
                /*Movies = from movie in Movies
                         where movie.Title != null && movie.Title.Contains(SearchTerms, StringComparison.CurrentCultureIgnoreCase)
                         select movie;*/
            }

            if (MPAARatings != null && MPAARatings.Length != 0)
            {
                Movies = Movies.Where(movie => movie.MPAARating != null && MPAARatings.Contains(movie.MPAARating));
            }

            if (Genres != null && Genres.Length != 0)
            {
                Movies = Movies.Where(movie => movie.MajorGenre != null && Genres.Contains(movie.MajorGenre));
            }
            
            if (IMDBMin != null)
            {
                Movies = Movies.Where(movie => movie.IMDBRating != null && IMDBMin <= movie.IMDBRating);
            }
            if (IMDBMax != null)
            {
                Movies = Movies.Where(movie => movie.IMDBRating != null && movie.IMDBRating <= IMDBMax);
            }

            if (RTMin != null)
            {
                Movies = Movies.Where(movie => movie.RottenTomatoesRating != null && RTMin <= movie.RottenTomatoesRating);
            }
            if (RTMax != null)
            {
                Movies = Movies.Where(movie => movie.RottenTomatoesRating != null && movie.RottenTomatoesRating <= RTMax);
            }
            //SearchTerms = Request.Query["SearchTerms"];
            //MPAARatings = Request.Query["MPAARatings"];
            //Genres = Request.Query["Genres"];
            //IMDBMin = imdbMin;
            //IMDBMax = imdbMax;
            //RTMin = rtMin;
            //RTMax = rtMax;

            //Movies = MovieDatabase.Search(SearchTerms);
            //Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);
            //Movies = MovieDatabase.FilterByGenre(Movies, Genres);
            //Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
            //Movies = MovieDatabase.FilterByRottenTomatoesRating(Movies, RTMin, RTMax);
        }
    }
}
