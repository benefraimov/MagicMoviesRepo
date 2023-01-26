using System;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Collections.Generic;
using Newtonsoft.Json;    
using System.Linq;

namespace MagicMoviesBackend.Models        
{
    public class MovieUpdateDto 
    {
        public string Name { get; set; }

        public DateTime Premiered { get; set; }

        public string Genres { get; set;}

        public string Image { get; set; }    

        public List<MovieSubscriber> MovieSubscribers { get; set; } 
        public List<Subscriber> Subscribers
        {
            get
            {
                return MovieSubscribers.Select(ms => ms.Subscriber).ToList();
            }
        }
    }
}