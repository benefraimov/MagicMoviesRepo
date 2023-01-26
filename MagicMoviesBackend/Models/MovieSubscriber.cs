using System;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MagicMoviesBackend.Models        
{
    public class MovieSubscriber 
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int SubscriberId { get; set; }
        public Subscriber Subscriber { get; set; }
    }
}