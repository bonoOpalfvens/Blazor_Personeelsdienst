using Personeelsdienst.Data.Mappers;
using Personeelsdienst.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Personeelsdienst.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Entiteit> Entiteiten { get; set; }
        public DbSet<Personeelslid> Personeelsleden { get; set; }
        public DbSet<Afwezigheid> Afwezigheden { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AfwezigheidConfiguration());
            modelBuilder.ApplyConfiguration(new EntiteitConfiguration());
            modelBuilder.ApplyConfiguration(new PersoneelslidConfiguration());
        }
    }
}
