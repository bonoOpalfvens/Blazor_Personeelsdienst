using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Personeelsdienst.Models;
using Personeelsdienst.Models.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Personeelsdienst.Data.Repositories
{
    public class BeheerderRepository : IBeheerderRepository
    {
        #region Setup
        private ApplicationDbContext _context;
        private DbSet<Beheerder> _beheerders;
        private UserManager<IdentityUser> _userManager;

        public BeheerderRepository(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _beheerders = context.Beheerders;
            _userManager = userManager;
        }
        #endregion
        private IQueryable<Beheerder> Beheerders => _beheerders.Include(b => b.Entiteiten);
        public IList<Beheerder> GetAll() => Beheerders.OrderBy(b => b.Email).ToList();

        public Beheerder GetByEmail(string email) => Beheerders.FirstOrDefault(b => b.Email.Equals(email));

        public Beheerder GetById(long id) => Beheerders.FirstOrDefault(b => b.Id.Equals(id));

        public void SaveChanges() => _context.SaveChanges();

        public void Verwijder(long id)
        {
            Beheerder beheerder = Beheerders.FirstOrDefault(b => b.Id.Equals(id));
            if(!(beheerder is null)) _context.Beheerders.Remove(beheerder);
            _context.SaveChanges();
        }

        public async Task VoegToe(Beheerder beheerder, string password)
        {
            if (_beheerders.Any(b => b.Email.ToLower().Equals(beheerder.Email))) throw new ArgumentException("Email van beheerder moet uniek zijn.");

            IdentityUser beheerderUser = new IdentityUser { UserName = beheerder.Email, Email = beheerder.Email };
            await _userManager.CreateAsync(beheerderUser, password);
            await _userManager.AddClaimAsync(beheerderUser, new Claim(ClaimTypes.Role, "beheerder"));

            _context.Beheerders.Add(beheerder);
            _context.SaveChanges();
        }
    }
}
