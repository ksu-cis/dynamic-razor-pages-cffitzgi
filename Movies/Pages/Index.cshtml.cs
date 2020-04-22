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
        public string SearchTerms { get; set; } = "";

        /// <summary>
        /// Filtered MPAARatings
        /// </summary>
        public string[] MPAARatings { get; set; }
        
        /// <summary>
        /// Filtered genres
        /// </summary>
        public string[] Genres { get; set; }

        /// <summary>
        /// Minimum IMDB Rating
        /// </summary>
        public double? IMDBMin { get; set; }
        /// <summary>
        /// Maximum IMDB Rating
        /// </summary>
        public double? IMDBMax { get; set; }

        /// <summary>
        /// Minimum Rotten Tomatoes
        /// </summary>
        public int? RTMin { get; set; }
        /// <summary>
        /// Maximum rotten tomatoes rating.
        /// </summary>
        public int? RTMax { get; set; }

        /// <summary>
        ///On search.
        /// </summary>
        public void OnGet(double? imdbMin, double? imdbMax, int? rtMin, int? rtMax)
        {
            SearchTerms = Request.Query["SearchTerms"];
            MPAARatings = Request.Query["MPAARatings"];
            Genres = Request.Query["Genres"];
            IMDBMin = imdbMin;
            IMDBMax = imdbMax;
            RTMin = rtMin;
            RTMax = rtMax;

            Movies = MovieDatabase.Search(SearchTerms);
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);
            Movies = MovieDatabase.FilterByGenre(Movies, Genres);
            Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
            Movies = MovieDatabase.FilterByRottenTomatoesRating(Movies, RTMin, RTMax);
        }
    }
}
