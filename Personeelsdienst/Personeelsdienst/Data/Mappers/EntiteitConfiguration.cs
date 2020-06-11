using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Personeelsdienst.Models;

namespace Personeelsdienst.Data.Mappers
{
    public class EntiteitConfiguration : IEntityTypeConfiguration<Entiteit>
    {
        public void Configure(EntityTypeBuilder<Entiteit> builder)
        {
            builder.ToTable("Entiteiten");

            builder.HasMany(e => e.Personeelsleden).WithOne(p => p.Entiteit);
        }
    }
}
