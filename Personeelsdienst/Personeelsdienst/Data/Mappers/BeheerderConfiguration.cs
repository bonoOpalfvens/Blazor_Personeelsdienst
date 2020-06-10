using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Personeelsdienst.Models;

namespace Personeelsdienst.Data.Mappers
{
    public class BeheerderConfiguration : IEntityTypeConfiguration<Beheerder>
    {
        public void Configure(EntityTypeBuilder<Beheerder> builder)
        {
            builder.ToTable("Beheerders");

            builder.HasMany(b => b.Entiteiten).WithOne();
        }
    }
}
