using System.Collections.Generic;
using AutoMapper;
using MagicMoviesBackend.Data;
using MagicMoviesBackend.Models;
using MagicMoviesBackend.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace MagicMoviesBackend.Controllers
{
    // api/workers
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/workers")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly IMagicWorkersRepo _repository;
        private readonly IMapper _mapper;

        public WorkersController(IMagicWorkersRepo repository, IMapper mapper)
        {
            _repository = repository;  
            _mapper = mapper;
        }
        
        //GET api/worker/{id}
        [HttpGet]
        public ActionResult <IEnumerable<WorkerReadDto>> GetAllWorkers()
        {
            var workerItems = _repository.GetAllWorkers();
            return Ok(_mapper.Map<IEnumerable<WorkerReadDto>>(workerItems));
        }

        //GET api/workers/1
        [HttpGet("{id}", Name="GetWorkerById")]
        public ActionResult<WorkerReadDto> GetWorkerById(int id)
        {
            var workerItem = _repository.GetWorkerById(id);
            if(workerItem != null){
                return Ok(_mapper.Map<WorkerReadDto>(workerItem));
            }
            return new ObjectResult(new {success = false, message = "No User with this Id"}) { StatusCode = 404};
        }

        //POST api/workers
        [HttpPost]
        public ActionResult <WorkerReadDto> CreateWorker(WorkerCreateDto workerCreateDto)
        {
            var workerModel = _mapper.Map<Worker>(workerCreateDto);
 
            var workerIsOkForCreation = _repository.CreateWorker(workerModel);

            if(workerIsOkForCreation)
            {
                _repository.SaveChanges();

                var workerReadDto = _mapper.Map<WorkerReadDto>(workerModel);

                // return CreatedAtRoute(nameof(GetWorkerById), new {id = workerReadDto.WorkerId}, workerReadDto);
                return new ObjectResult(new {success = true, message = "Username Created"}) { StatusCode = 201};      
            }
            else{
                return new ObjectResult(new {success = false, message = "Username already exists"}) { StatusCode = 400};        
            }        
        }

        //PUT api/workers/{1}
        [HttpPut("{id}")]
        public ActionResult UpdateWorker(int id, WorkerUpdateDto workerUpdateDto)
        {
            var workerModelFromRepo = _repository.GetWorkerById(id);
            if(workerModelFromRepo == null)
            {
                return new ObjectResult(new {success = false, message = "No User with this Id"}) { StatusCode = 404};
            }

            _mapper.Map(workerUpdateDto, workerModelFromRepo);

            _repository.UpdateWorker(workerModelFromRepo);

            _repository.SaveChanges();

            return Ok(new {success = true, message = "Worker has been updated"});
        }

        //DELETE api/workers/{1}
        [HttpDelete("{id}")]
        public ActionResult DeleteWorker(int id)
        {
            var workerModelFromRepo = _repository.GetWorkerById(id);
            if(workerModelFromRepo == null)
            {
                return new ObjectResult(new {success = false, message = "No User with this Id"}) { StatusCode = 404};
            }

            _repository.DeleteWorker(workerModelFromRepo);
            
            _repository.SaveChanges();

            return new ObjectResult(new {success = true, message = "User Deleted"}) { StatusCode = 204};      
        }
    }
}

        