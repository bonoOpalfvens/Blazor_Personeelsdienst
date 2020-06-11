using System.Collections.Generic;

namespace Personeelsdienst.Models.IRepositories
{
    public interface IEntiteitRepository
    {
        public IList<Entiteit> GetAll();
        public Entiteit GetById(long id);
        public Entiteit GetByEmail(string email);
        public void VoegToe(Entiteit entiteit);
        public void Verwijder(long id);
        public void SaveChanges();
    }
}
