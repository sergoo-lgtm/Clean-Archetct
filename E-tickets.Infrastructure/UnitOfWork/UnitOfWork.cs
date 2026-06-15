using E_tickets.Infrastructure.Data;
using eTickets.Models;
using eTickets.Services;

namespace eTickets.UnitOfWork
{
    
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(
            IGenericRepository<Actor> actors,
            IGenericRepository<Cinema> cinemas,
            IGenericRepository<Producer> producers,
            IGenericRepository<Movie> movies,
            AppDbContext context)
        {
            _context = context;

            Actors = actors;
            Cinemas = cinemas;
            Producers = producers;
            Movies = movies;
        }

        public IGenericRepository<Actor> Actors { get; }
        public IGenericRepository<Cinema> Cinemas { get; }
        public IGenericRepository<Producer> Producers { get; }
        public IGenericRepository<Movie> Movies { get; }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}