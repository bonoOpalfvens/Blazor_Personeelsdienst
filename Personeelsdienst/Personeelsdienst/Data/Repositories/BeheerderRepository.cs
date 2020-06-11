using Microsoft.EntityFrameworkCore;
using Personeelsdienst.Models;
using Personeelsdienst.Models.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Personeelsdienst.Data.Repositories
{
    public class BeheerderRepository : IBeheerderRepository
    {
        #region Setup
        private ApplicationDbContext _context;
        private DbSet<Beheerder> _beheerders;

        public BeheerderRepository(ApplicationDbContext context)
        {
            _context = context;
            _beheerders = context.Beheerders;
        }
        #endregion
        private IQueryable<Beheerder> Beheerders => _beheerders.Include(b => b.Entiteiten).ThenInclude(e => e.Entiteit);
        public IList<Beheerder> GetAll() => Beheerders.OrderBy(b => b.Email).ToList();

        public Beheerder GetByEmail(string email) => Beheerders.FirstOrDefault(b => b.Email.Equals(email));

        public Beheerder GetById(long id) => Beheerders.FirstOrDefault(b => b.Id.Equals(id));

        public void SaveChanges() => _context.SaveChanges();

        public void Verwijder(long id)
        {
            Beheerder beheerder = Beheerders.FirstOrDefault(e => e.Id.Equals(id));
            if (!(beheerder is null))
            {
                _context.Beheerders.Remove(beheerder);
                _context.SaveChanges();
            }
        }

        public void VoegToe(Beheerder beheerder)
        {
            if (_beheerders.Any(b => b.Email.ToLower().Equals(beheerder.Email))) throw new ArgumentException("Email van beheerder moet uniek zijn.");
            _context.Beheerders.Add(beheerder);
            _context.SaveChanges();
        }
    }
}
