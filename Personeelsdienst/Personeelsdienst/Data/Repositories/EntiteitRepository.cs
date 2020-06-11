using Microsoft.EntityFrameworkCore;
using Personeelsdienst.Models;
using Personeelsdienst.Models.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Personeelsdienst.Data.Repositories
{
    public class EntiteitRepository : IEntiteitRepository
    {
        #region Setup
        private ApplicationDbContext _context;
        private DbSet<Entiteit> _entiteiten;

        public EntiteitRepository(ApplicationDbContext context)
        {
            _context = context;
            _entiteiten = context.Entiteiten;
        }
        #endregion
        private IQueryable<Entiteit> Entiteiten => _entiteiten.Include(e => e.Personeelsleden).ThenInclude(p => p.Afwezigheden);

        public IList<Entiteit> GetAll() => Entiteiten.OrderBy(e => e.Entiteitsnaam).ToList();

        public Entiteit GetById(long id) => Entiteiten.FirstOrDefault(e => e.Id.Equals(id));

        public Entiteit GetByEmail(string email) => Entiteiten.FirstOrDefault(e => e.Email.Equals(email));

        public void SaveChanges() => _context.SaveChanges();

        public void VoegToe(Entiteit entiteit)
        {
            if (_entiteiten.Any(b => b.Email.ToLower().Equals(entiteit.Email))) throw new ArgumentException("Email van entiteit moet uniek zijn.");
            _context.Entiteiten.Add(entiteit);
            _context.SaveChanges();
        }

        public void Verwijder(long id)
        {
            Entiteit entiteit = Entiteiten.FirstOrDefault(e => e.Id.Equals(id));
            if (!(entiteit is null))
            {
                _context.Entiteiten.Remove(entiteit);
                _context.SaveChanges();
            }
        }
    }
}
