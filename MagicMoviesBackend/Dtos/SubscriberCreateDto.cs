using System;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Collections.Generic;
using Newtonsoft.Json;    
using System.Linq;

namespace MagicMoviesBackend.Models        
{
    public class SubscriberCreateDto 
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}