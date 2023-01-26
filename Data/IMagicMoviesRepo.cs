using System.Collections.Generic;
using MagicMoviesBackend.Models;

namespace MagicMoviesBackend.Data
{
    public interface IMagicMoviesRepo
    {
        bool SaveChanges();

        IEnumerable<Movie> GetAllMovies();
        Movie GetMovieById(int id);
        Movie CreateMovie(Movie movie);
        void UpdateMovie(Movie movie);
        bool DeleteMovie(int id);
    }
}