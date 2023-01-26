using System;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MagicMoviesBackend.Models        
{
    public class Worker 
    {
        public int WorkerId { get; set; }

        public string FullName { get; set; }
        
        public bool IsAdmin { get; set; }
        
        public string UserName { get; set; }
        
        public string Password { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public Permissions Permissions { get; set; }

        public Worker()
        {
            Permissions = new Permissions();
        }
    }
}