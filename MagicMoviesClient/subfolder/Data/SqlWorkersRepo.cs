using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using MagicMoviesBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicMoviesBackend.Data
{
    public class SqlWorkersRepo : IMagicWorkersRepo
    {
        // The Dbcontext use..
        private readonly MagicMoviesContext _context;

        // default ctor
        public SqlWorkersRepo(MagicMoviesContext context)
        {
            _context = context;
        }

        public bool CreateWorker(Worker worker)
        {
            if(worker == null)
            {
                throw new ArgumentNullException(nameof(worker));
            }

            var workers = GetAllWorkers();

            foreach (var workerElement in workers)
            {
                if (workerElement.UserName == worker.UserName )
                {
                    return false;
                }
            }
            
            _context.Workers.Add(worker);

            return true;
        }

        public void DeleteWorker(Worker worker)
        {   
            if(worker == null)
            {
                throw new ArgumentNullException(nameof(worker));
            }
            _context.Workers.Remove(worker);
        }

        public IEnumerable<Worker> GetAllWorkers()
        {
            return _context.Workers.Include(w => w.Permissions).ToList();
        }

        public Worker GetWorkerById(int id)
        {
            return _context.Workers.Include(w => w.Permissions).FirstOrDefault(w => w.WorkerId == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateWorker(Worker worker)
        {
            // Everything is implemented in the controller, But it can also be implemented here...
        }
    }
}