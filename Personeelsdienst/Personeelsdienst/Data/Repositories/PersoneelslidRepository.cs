using Microsoft.EntityFrameworkCore;
using Personeelsdienst.Models;
using Personeelsdienst.Models.IRepositories;
using System.Collections.Generic;
using System.Linq;

namespace Personeelsdienst.Data.Repositories
{
    public class PersoneelslidRepository : IPersoneelslidRepository
    {
        #region Setup
        private ApplicationDbContext _context;
        private DbSet<Personeelslid> _personeelsleden;

        public PersoneelslidRepository(ApplicationDbContext context)
        {
            _context = context;
            _personeelsleden = context.Personeelsleden;
        }
        #endregion
        private IQueryable<Personeelslid> Personeelsleden => _personeelsleden.Include(p => p.Afwezigheden).Include(p => p.Entiteit);
        public IList<Personeelslid> GetAll() => Personeelsleden.ToList();

        public Personeelslid GetById(long id) => Personeelsleden.FirstOrDefault(p => p.Id.Equals(id));

        public IList<Personeelslid> GetByEntiteit(long id) => Personeelsleden.Include(p => p.Entiteit).Where(p => p.Entiteit.Id.Equals(id)).ToList();

        public void SaveChanges() => _context.SaveChanges();

        public void Verwijder(long id)
        {
            Personeelslid personeelslid = _personeelsleden.FirstOrDefault(p => p.Id.Equals(id));
            if (!(personeelslid is null))
            {
                _context.Personeelsleden.Remove(personeelslid);
                _context.SaveChanges();
            }
        }

        public void VoegToe(Personeelslid personeelslid)
        {
            _context.Personeelsleden.Add(personeelslid);
            _context.SaveChanges();
        }
    }
}
