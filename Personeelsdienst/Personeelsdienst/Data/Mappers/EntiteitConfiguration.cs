using Personeelsdienst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
