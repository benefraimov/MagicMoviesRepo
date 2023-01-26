using System;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MagicMoviesBackend.Models        
{
    public class Permissions
    {        
        public int PermissionsId { get; set; }

        public bool WatchSubs { get; set; }

        public bool CreateSubs { get; set; }

        public bool UpdateSubs { get; set; }

        public bool DeleteSubs { get; set; }

        public bool WatchMovies { get; set; }

        public bool CreateMovies { get; set; }

        public bool UpdateMovies { get; set; }

        public bool DeleteMovies { get; set; }


        public int WorkerId { get; set; }
        [JsonIgnore]
        public Worker Worker { get; set; }
    }
}