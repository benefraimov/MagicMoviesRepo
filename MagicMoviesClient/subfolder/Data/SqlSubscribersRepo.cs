using System;
using System.Collections.Generic;
using System.Linq;
using MagicMoviesBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicMoviesBackend.Data
{
    public class SqlSubscribersRepo : IMagicSubscribersRepo
    {
        private readonly MagicMoviesContext _context;

        public SqlSubscribersRepo(MagicMoviesContext context)
        {
            _context = context;
        }

        public IEnumerable<Subscriber> GetAllSubscribers()
        {
            return _context.Subscribers.Include(s => s.MovieSubscribers).ThenInclude(ms => ms.Movie).ToList();            
        }

        public Subscriber GetSubscriberById(int id)
        {
            return _context.Subscribers.Include(s => s.MovieSubscribers).ThenInclude(ms => ms.Movie).FirstOrDefault(s => s.SubscriberId == id);
        }

        public bool CreateSubscriber(Subscriber subscriber)
        {
            if(subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }

            var subscribers = GetAllSubscribers();

            foreach (var subscriberElement in subscribers)
            {
                if (subscriberElement.Email == subscriber.Email )
                {
                    return false;
                }
            }
            
            _context.Subscribers.Add(subscriber);

            return true;
        }

        public void UpdateSubscriber(Subscriber subscriber)
        {
            // Everything is implemented in the controller, But it can also be implemented here...
        }

        public bool DeleteSubscriber(int id)
        {   
            var subscriber = _context.Subscribers.Include(s => s.MovieSubscribers).FirstOrDefault(s => s.SubscriberId == id);
            if (subscriber == null)
            {
                return false;
            }
            _context.Subscribers.Remove(subscriber);
            _context.SaveChanges();
            return true;
        }


        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}