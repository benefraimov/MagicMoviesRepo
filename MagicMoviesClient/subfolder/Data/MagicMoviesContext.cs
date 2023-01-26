using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using MagicMoviesBackend.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicMoviesBackend.Data 
{
    public class MagicMoviesContext : DbContext
    {
        public MagicMoviesContext(DbContextOptions<MagicMoviesContext> opt): base(opt)
        {
            
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }     
        public DbSet<MovieSubscriber> MovieSubscribers { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Permissions> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Worker>()
                .HasOne(w => w.Permissions)
                .WithOne(p => p.Worker)
                .HasForeignKey<Permissions>(p => p.WorkerId);

            modelBuilder.Entity<MovieSubscriber>()
                .HasKey(ms => new { ms.MovieId, ms.SubscriberId });

            modelBuilder.Entity<MovieSubscriber>()
                .HasOne(ms => ms.Movie)
                .WithMany(m => m.MovieSubscribers)
                .HasForeignKey(ms => ms.MovieId);

            modelBuilder.Entity<MovieSubscriber>()
                .HasOne(ms => ms.Subscriber)
                .WithMany(s => s.MovieSubscribers)
                .HasForeignKey(ms => ms.SubscriberId); 
        }     
    }
}