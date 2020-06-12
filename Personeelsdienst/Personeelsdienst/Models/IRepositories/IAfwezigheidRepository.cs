using System.Collections.Generic;

namespace Personeelsdienst.Models.IRepositories
{
    public interface IAfwezigheidRepository
    {
        public IList<Afwezigheid> GetAll();
        public Afwezigheid GetById(long id);
        public IList<Afwezigheid> GetByEntiteit(long id);
        public void VoegToe(Afwezigheid afwezigheid);
        public void Verwijder(long id);
        public void SaveChanges();
    }
}
