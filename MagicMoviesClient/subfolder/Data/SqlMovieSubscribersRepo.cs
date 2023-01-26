using System;
using System.Collections.Generic;
using System.Linq;
using MagicMoviesBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicMoviesBackend.Data
{
    public class SqlMovieSubscribersRepo : IMagicMovieSubscribersRepo
    {
        private readonly MagicMoviesContext _context;

        public SqlMovieSubscribersRepo(MagicMoviesContext context)
        {
            _context = context;
        }

        public IEnumerable<MovieSubscriber> GetAllMovieSubscribers()
        {
            return _context.MovieSubscribers.Include(ms => ms.Movie).Include(ms => ms.Subscriber).ToList();            
        // .Include(s => s.MovieSubscribers).ThenInclude(ms => ms.Movie)
        }

        // public Subscriber GetSubscriberById(int id)
        // {
        //     return _context.Subscribers.Include(s => s.MovieSubscribers).ThenInclude(ms => ms.Movie).FirstOrDefault(s => s.SubscriberId == id);
        // }

        // public Subscriber CreateSubscriber(Subscriber subscriberModel)
        // {
        //     if(subscriberModel == null)
        //     {
        //         throw new ArgumentNullException(nameof(subscriberModel));
        //     }

        //     _context.Subscribers.Add(subscriberModel);
        //     _context.SaveChanges();
        //     return subscriberModel;
        // }

        // public void UpdateSubscriber(Subscriber subscriber)
        // {
        //     // Everything is implemented in the controller, But it can also be implemented here...
        //     // _context.Subscribers.Update(subscriber);
        //     // _context.SaveChanges();
        // }

        // public bool DeleteSubscriber(int id)
        // {   
        //     var subscriber = _context.Subscribers.Include(s => s.MovieSubscribers).FirstOrDefault(s => s.SubscriberId == id);
        //     if (subscriber == null)
        //     {
        //         return false;
        //     }
        //     _context.Subscribers.Remove(subscriber);
        //     _context.SaveChanges();
        //     return true;
        // }


        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}