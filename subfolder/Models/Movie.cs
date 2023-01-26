using System;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Linq;

namespace MagicMoviesBackend.Models        
{
    public class Movie 
    {
        public int MovieId { get; set; }

        public string Name { get; set; }

        public DateTime Premiered { get; set; }

        public string Genres { get; set;}

        public string Image { get; set; }    
        [JsonIgnore]
        public List<MovieSubscriber> MovieSubscribers { get; set; }
        [JsonIgnore]        
        public List<Subscriber> Subscribers
        {
            get
            {
                return MovieSubscribers.Select(ms => ms.Subscriber).ToList();
            }
        }
    }
}