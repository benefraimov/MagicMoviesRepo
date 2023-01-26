using System.Collections.Generic;
using MagicMoviesBackend.Models;

namespace MagicMoviesBackend.Data
{
    public interface IMagicSubscribersRepo
    {
        bool SaveChanges();

        IEnumerable<Subscriber> GetAllSubscribers();
        Subscriber GetSubscriberById(int id);
        bool CreateSubscriber(Subscriber subscriber);
        void UpdateSubscriber(Subscriber subscriber);
        bool DeleteSubscriber(int id);
    }
}