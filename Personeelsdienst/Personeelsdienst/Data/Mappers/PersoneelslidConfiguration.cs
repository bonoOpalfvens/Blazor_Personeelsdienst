using Personeelsdienst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Personeelsdienst.Data.Mappers
{
    public class PersoneelslidConfiguration : IEntityTypeConfiguration<Personeelslid>
    {
        public void Configure(EntityTypeBuilder<Personeelslid> builder)
        {
            builder.ToTable("Personeelsleden");

            builder.HasOne(p => p.Entiteit).WithMany(e => e.Personeelsleden);
            builder.HasMany(p => p.Afwezigheden).WithOne(a => a.Personeelslid);
        }
    }
}
