using System.Collections.Generic;

namespace Personeelsdienst.Models.IRepositories
{
    interface IAfwezigheidRepository
    {
        public IList<Afwezigheid> GetAll();
        public Afwezigheid GetById(long id);
        public Afwezigheid GetByEntiteit(long id);
        public void VoegToe(Afwezigheid afwezigheid);
        public void Verwijder(long id);
        public void SaveChanges();
    }
}
