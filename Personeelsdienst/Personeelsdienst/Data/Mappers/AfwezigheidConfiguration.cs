using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Personeelsdienst.Models;

namespace Personeelsdienst.Data.Mappers
{
    public class AfwezigheidConfiguration : IEntityTypeConfiguration<Afwezigheid>
    {
        public void Configure(EntityTypeBuilder<Afwezigheid> builder)
        {
            builder.ToTable("Afwezigheden");

            builder.HasOne(a => a.Personeelslid).WithMany(p => p.Afwezigheden);
        }
    }
}
