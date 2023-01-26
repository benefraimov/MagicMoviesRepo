using System;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Collections.Generic;
using Newtonsoft.Json;    
using System.Linq;

namespace MagicMoviesBackend.Models        
{
    public class SubscriberUpdateDto 
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<MovieSubscriber> MovieSubscribers { get; set; }
        public List<Movie> Movies
        {
            get
            {
                return MovieSubscribers.Select(ms => ms.Movie).ToList();
            }
        }
    }
}