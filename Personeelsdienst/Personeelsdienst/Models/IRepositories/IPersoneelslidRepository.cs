using System.Collections.Generic;

namespace Personeelsdienst.Models.IRepositories
{
    public interface IPersoneelslidRepository
    {
        public IList<Personeelslid> GetAll();
        public Personeelslid GetById(long id);
        public IList<Personeelslid> GetByEntiteit(long id);
        public void VoegToe(Personeelslid personeelslid);
        public void Verwijder(long id);
        public void SaveChanges();
    }
}
