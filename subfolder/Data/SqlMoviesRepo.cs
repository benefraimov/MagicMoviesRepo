using System;
using System.Collections.Generic;
using System.Linq;
using MagicMoviesBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicMoviesBackend.Data
{
    public class SqlMoviesRepo : IMagicMoviesRepo
    {
        private readonly MagicMoviesContext _context;

        public SqlMoviesRepo(MagicMoviesContext context)
        {
            _context = context;
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _context.Movies.Include(m => m.MovieSubscribers).ThenInclude(ms => ms.Subscriber).ToList();            
        }

        public Movie GetMovieById(int id)
        {
            return _context.Movies.Include(m => m.MovieSubscribers).ThenInclude(ms => ms.Subscriber).FirstOrDefault(m => m.MovieId == id);
        }        

        public Movie CreateMovie(Movie movieModel)
        {
            if(movieModel == null)
            {
                throw new ArgumentNullException(nameof(movieModel));
            }

            _context.Movies.Add(movieModel);
            _context.SaveChanges();
            return movieModel;
        }

        public void UpdateMovie(Movie movie)
        {
            // Everything is implemented in the controller, But it can also be implemented here...
        }

        public bool DeleteMovie(int id)
        {   
            var movie = _context.Movies.Include(m => m.MovieSubscribers).FirstOrDefault(m => m.MovieId == id);
            if (movie == null)
            {
                return false;
            }
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return true;
        }

        

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}