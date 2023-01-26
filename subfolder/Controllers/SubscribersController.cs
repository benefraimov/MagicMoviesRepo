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
    [Route("api/subscribers")]
    [ApiController]
    public class SubscribersController : ControllerBase
    {
        private readonly IMagicSubscribersRepo _repository;
        private readonly IMapper _mapper;

        public SubscribersController(IMagicSubscribersRepo repository, IMapper mapper)
        {
            _repository = repository;  
            _mapper = mapper;
        }

        //GET api/subscribers/{id}
        [HttpGet]
        public ActionResult <IEnumerable<Subscriber>> GetAllSubscribers()
        {
            var subscriberItems = _repository.GetAllSubscribers();

            return Ok(subscriberItems);
        }

        //GET api/subscribers/1
        [HttpGet("{id}", Name="GetSubscriberById")]
        public ActionResult <Subscriber> GetSubscriberById(int id)
        {
            var subscriberItem = _repository.GetSubscriberById(id);
            if(subscriberItem != null){
                return Ok(subscriberItem);
            }
            return NotFound();
        }

        //POST api/subscribers
        [HttpPost]
        public ActionResult <Subscriber> CreateSubscriber(SubscriberCreateDto subscriberCreateDto)
        {
            var subscriberModel = _mapper.Map<Subscriber>(subscriberCreateDto);

            var newSubscriber = _repository.CreateSubscriber(subscriberModel);

            if(newSubscriber)
            {
                _repository.SaveChanges();

                return new ObjectResult(new {success = true, message = "Subscriber Created"}) { StatusCode = 201};      
            }
            else{
                return new ObjectResult(new {success = false, message = "Subscriber already exists"}) { StatusCode = 400};        
            }  
        }

        //PUT api/subscribers/{1}
        [HttpPut("{id}")]
        public ActionResult UpdateSubscriber(int id, SubscriberUpdateDto subscriberUpdateDto)
        {
            var subscriberModelFromRepo = _repository.GetSubscriberById(id);
            if(subscriberModelFromRepo == null)
            {
                return new ObjectResult(new {success = false, message = "No Subscriber with this Id"}) { StatusCode = 404};
            }

            _mapper.Map(subscriberUpdateDto, subscriberModelFromRepo);

            _repository.UpdateSubscriber(subscriberModelFromRepo);

            _repository.SaveChanges();

            return Ok(new {success = true, message = "Subscriber has been updated"});
        }

        //DELETE api/subscribers/{1}
        [HttpDelete("{id}")]
        public ActionResult DeleteSubscriber(int id)
        {
            if (!_repository.DeleteSubscriber(id))
            {
                return NotFound();
            }

            return NoContent();            
        }
    }
}

        