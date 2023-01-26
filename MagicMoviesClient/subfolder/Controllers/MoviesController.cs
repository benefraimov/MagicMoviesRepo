using System.Collections.Generic;
using AutoMapper;
using MagicMoviesBackend.Data;
using MagicMoviesBackend.Models;
using MagicMoviesBackend.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicMoviesBackend.Controllers
{
    // api/movies
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMagicMoviesRepo _repository;
        private readonly IMapper _mapper;

        public MoviesController(IMagicMoviesRepo repository, IMapper mapper)
        {
            _repository = repository;  
            _mapper = mapper;
        }
        
        //GET api/movies
        [HttpGet]
        public ActionResult <IEnumerable<Movie>> GetAllMovies()
        {
            var movieItems = _repository.GetAllMovies();
            return Ok(movieItems);
        }

        //GET api/movies/1
        [HttpGet("{id}", Name="GetMovieById")]
        public ActionResult <Movie> GetMovieById(int id)
        {
            var movieItem = _repository.GetMovieById(id);
            if(movieItem != null){
                return Ok(movieItem);
            }
            return NotFound();
        }

        //POST api/movies
        [HttpPost]
        public ActionResult <Movie> CreateMovie(Movie movie)
        {
            var newMovie = _repository.CreateMovie(movie);

            return CreatedAtRoute(nameof(GetMovieById), new {id = newMovie.MovieId}, newMovie);
        }

        //PUT api/movies/{1}
        [HttpPut("{id}")]
        public ActionResult UpdateMovie(int id, MovieUpdateDto movieUpdateDto)
        {
            var movieModelFromRepo = _repository.GetMovieById(id);
            if(movieModelFromRepo == null)
            {
                return new ObjectResult(new {success = false, message = "No Movie with this Id"}) { StatusCode = 404};
            }

            _mapper.Map(movieUpdateDto, movieModelFromRepo);

            _repository.UpdateMovie(movieModelFromRepo);

            _repository.SaveChanges();

            return Ok(new {success = true, message = "Movie has been updated"});
        }

        //DELETE api/movies/{1}
        [HttpDelete("{id}")]
        public ActionResult DeleteMovie(int id)
        {
            if (!_repository.DeleteMovie(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

        