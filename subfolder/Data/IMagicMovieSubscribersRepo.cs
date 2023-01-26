using System.Collections.Generic;
using MagicMoviesBackend.Models;

namespace MagicMoviesBackend.Data
{
    public interface IMagicMovieSubscribersRepo
    {
        bool SaveChanges();

        IEnumerable<MovieSubscriber> GetAllMovieSubscribers();
        // Subscriber GetSubscriberById(int id);
        // Subscriber CreateSubscriber(Subscriber subscriber);
        // void UpdateSubscriber(Subscriber subscriber);
        // bool DeleteSubscriber(int id);
    }
}