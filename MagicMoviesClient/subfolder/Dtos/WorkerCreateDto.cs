using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MagicMoviesBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicMoviesBackend.Dtos
{
    public class WorkerCreateDto
    {        
        public string FullName { get; set; }

        public bool IsAdmin { get; set; }

        public string UserName { get; set; }
        
        public string Password { get; set; }    

        public DateTime CreatedDate { get; set; }

        public Permissions Permissions { get; set; } 

        public WorkerCreateDto()
        {
            Permissions = new Permissions();
        }        
    }
}