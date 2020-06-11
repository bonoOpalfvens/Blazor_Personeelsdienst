using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Personeelsdienst.Data.Mappers;
using Personeelsdienst.Models;

namespace Personeelsdienst.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Entiteit> Entiteiten { get; set; }
        public DbSet<Personeelslid> Personeelsleden { get; set; }
        public DbSet<Afwezigheid> Afwezigheden { get; set; }
        public DbSet<Beheerder> Beheerders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AfwezigheidConfiguration());
            modelBuilder.ApplyConfiguration(new EntiteitConfiguration());
            modelBuilder.ApplyConfiguration(new PersoneelslidConfiguration());
            modelBuilder.ApplyConfiguration(new BeheerderConfiguration());
        }
    }
}
