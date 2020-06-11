using Microsoft.AspNetCore.Identity;
using Personeelsdienst.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Personeelsdienst.Data
{
    public class DataInitialiser
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DataInitialiser(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task InitialiseData()
        {
            _context.Database.EnsureDeleted();
            if(_context.Database.EnsureCreated())
            {
                // Gebruikers
                // Admin
                IdentityUser admin = new IdentityUser { UserName = "admin@hetleercollectief.be", Email = "admin@hetleercollectief.be" };
                await _userManager.CreateAsync(admin, "test");
                await _userManager.AddClaimAsync(admin, new Claim(ClaimTypes.Role, "admin"));

                // TestEntiteiten
                Entiteit entiteit1 = new Entiteit("KA Dendermonde", "kadendermonde@hetleercollectief.be");
                Entiteit entiteit2 = new Entiteit("MS Atheneum", "msatheneum@hetleercollectief.be");
                _context.Entiteiten.AddRange(entiteit1, entiteit2);

                IdentityUser entiteitUser1 = new IdentityUser { UserName = "kadendermonde@hetleercollectief.be", Email = "kadendermonde@hetleercollectief.be" };
                await _userManager.CreateAsync(entiteitUser1, "test");
                await _userManager.AddClaimAsync(entiteitUser1, new Claim(ClaimTypes.Role, "entiteit"));

                IdentityUser entiteitUser2 = new IdentityUser { UserName = "msatheneum@hetleercollectief.be", Email = "msatheneum@hetleercollectief.be" };
                await _userManager.CreateAsync(entiteitUser2, "test");
                await _userManager.AddClaimAsync(entiteitUser2, new Claim(ClaimTypes.Role, "entiteit"));

                // TestBeheerders
                Beheerder beheerder1 = new Beheerder("beheerder1@hetleercollectief.be");
                Beheerder beheerder2 = new Beheerder("beheerder2@hetleercollectief.be");
                beheerder1.Entiteiten.Add(new EntiteitBeheerder { EntiteitId = entiteit1.Id, BeheerderId = beheerder1.Id });
                beheerder1.Entiteiten.Add(new EntiteitBeheerder { EntiteitId = entiteit2.Id, BeheerderId = beheerder2.Id });

                IdentityUser beheerderUser1 = new IdentityUser { UserName = "beheerder1@hetleercollectief.be", Email = "beheerder1@hetleercollectief.be" };
                await _userManager.CreateAsync(beheerderUser1, "test");
                await _userManager.AddClaimAsync(beheerderUser1, new Claim(ClaimTypes.Role, "beheerder"));

                IdentityUser beheerderUser2 = new IdentityUser { UserName = "beheerder2@hetleercollectief.be", Email = "beheerder2@hetleercollectief.be" };
                await _userManager.CreateAsync(beheerderUser2, "test");
                await _userManager.AddClaimAsync(beheerderUser2, new Claim(ClaimTypes.Role, "beheerder"));

                _context.Beheerders.AddRange(beheerder1, beheerder2);
                // Finish transaction
                _context.SaveChanges();
            }
        }
    }
}
