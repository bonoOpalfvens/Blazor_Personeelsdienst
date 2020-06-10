using System.Collections.Generic;

namespace Personeelsdienst.Models.IRepositories
{
    public interface IBeheerderRepository
    {
        public IList<Beheerder> GetAll();
        public Beheerder GetById(long id);
        public Beheerder GetByEmail(string email);
        public void VoegToe(Beheerder beheerder, string password);
        public void SaveChanges();
    }
}
