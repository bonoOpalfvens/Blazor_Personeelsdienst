using Microsoft.EntityFrameworkCore;
using Personeelsdienst.Models;
using Personeelsdienst.Models.IRepositories;
using System.Collections.Generic;
using System.Linq;

namespace Personeelsdienst.Data.Repositories
{
    public class AfwezigheidRepository : IAfwezigheidRepository
    {
        #region Setup
        private ApplicationDbContext _context;
        private DbSet<Afwezigheid> _afwezigheden;

        public AfwezigheidRepository(ApplicationDbContext context)
        {
            _context = context;
            _afwezigheden = context.Afwezigheden;
        }
        #endregion
        private IQueryable<Afwezigheid> Afwezigheden => _afwezigheden.Include(a => a.Personeelslid).ThenInclude(p => p.Entiteit);
        public IList<Afwezigheid> GetAll() => Afwezigheden.ToList();

        public Afwezigheid GetById(long id) => Afwezigheden.FirstOrDefault(a => a.Id.Equals(id));

        public IList<Afwezigheid> GetByEntiteit(long id) => Afwezigheden.Where(a => a.Personeelslid.Entiteit.Id.Equals(id)).ToList();

        public void SaveChanges() => _context.SaveChanges();

        public void Verwijder(long id)
        {
            Afwezigheid afwezigheid = _afwezigheden.FirstOrDefault(a => a.Id.Equals(id));
            if(!(afwezigheid is null))
            {
                _context.Afwezigheden.Remove(afwezigheid);
                _context.SaveChanges();
            }
        }

        public void VoegToe(Afwezigheid afwezigheid)
        {
            _context.Afwezigheden.Add(afwezigheid);
            _context.SaveChanges();
        }
    }
}
