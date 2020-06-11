using System.Collections.Generic;
using System.Threading.Tasks;

namespace Personeelsdienst.Models.IRepositories
{
    public interface IBeheerderRepository
    {
        public IList<Beheerder> GetAll();
        public Beheerder GetById(long id);
        public Beheerder GetByEmail(string email);
        public Task VoegToe(Beheerder beheerder, string password);
        public void Verwijder(long id);
        public void SaveChanges();
    }
}
