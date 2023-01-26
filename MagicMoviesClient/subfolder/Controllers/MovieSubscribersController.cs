using System.Collections.Generic;
using AutoMapper;
using MagicMoviesBackend.Data;
using MagicMoviesBackend.Models;
using MagicMoviesBackend.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicMoviesBackend.Controllers
{
    // api/subscribers
    [Route("api/moviesubscribers")]
    [ApiController]
    public class MovieSubscribersController : ControllerBase
    {
        private readonly IMagicMovieSubscribersRepo _repository;
        private readonly IMapper _mapper;

        public MovieSubscribersController(IMagicMovieSubscribersRepo repository, IMapper mapper)
        {
            _repository = repository;  
            _mapper = mapper;
        }

        //GET api/moviesubscribers/{id}
        [HttpGet]
        public ActionResult <IEnumerable<MovieSubscriber>> GetAllMovieSubscribers()
        {
            var moviesubscriberItems = _repository.GetAllMovieSubscribers();

            return Ok(moviesubscriberItems);
        }

        // //GET api/subscribers/1
        // [HttpGet("{id}", Name="GetMovieSubscriberById")]
        // public ActionResult <Subscriber> GetSubscriberById(int id)
        // {
        //     var subscriberItem = _repository.GetSubscriberById(id);
        //     if(subscriberItem != null){
        //         return Ok(subscriberItem);
        //     }
        //     return NotFound();
        // }

        // //POST api/subscribers
        // [HttpPost]
        // public ActionResult <Subscriber> CreateSubscriber(Subscriber subscriber)
        // {
        //     var newSubscriber = _repository.CreateSubscriber(subscriber);

        //     return CreatedAtRoute(nameof(GetSubscriberById), new {id = newSubscriber.SubscriberId}, newSubscriber);
        // }

        // //PUT api/subscribers/{1}
        // [HttpPut("{id}")]
        // public ActionResult UpdateSubscriber(int id, SubscriberUpdateDto subscriberUpdateDto)
        // {
        //     var subscriberModelFromRepo = _repository.GetSubscriberById(id);
        //     if(subscriberModelFromRepo == null)
        //     {
        //         return new ObjectResult(new {success = false, message = "No Subscriber with this Id"}) { StatusCode = 404};
        //     }

        //     _mapper.Map(subscriberUpdateDto, subscriberModelFromRepo);

        //     _repository.UpdateSubscriber(subscriberModelFromRepo);

        //     _repository.SaveChanges();

        //     return Ok(new {success = true, message = "Subscriber has been updated"});
        // }

        // //DELETE api/subscribers/{1}
        // [HttpDelete("{id}")]
        // public ActionResult DeleteSubscriber(int id)
        // {
        //     if (!_repository.DeleteSubscriber(id))
        //     {
        //         return NotFound();
        //     }

        //     return NoContent();            
        // }
    }
}

        