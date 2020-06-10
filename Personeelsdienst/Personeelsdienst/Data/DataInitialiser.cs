using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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

                _context.SaveChanges();
            }
        }
    }
}
