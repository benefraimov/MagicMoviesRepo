using System.Collections.Generic;
using MagicMoviesBackend.Models;

namespace MagicMoviesBackend.Data
{
    public interface IMagicWorkersRepo
    {
        bool SaveChanges();

        IEnumerable<Worker> GetAllWorkers();
        Worker GetWorkerById(int id);
        bool CreateWorker(Worker worker);
        void UpdateWorker(Worker worker);
        void DeleteWorker(Worker worker);
    }
}