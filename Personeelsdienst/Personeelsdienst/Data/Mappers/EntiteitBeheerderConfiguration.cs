using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Personeelsdienst.Models;

namespace Personeelsdienst.Data.Mappers
{
    public class EntiteitBeheerderConfiguration : IEntityTypeConfiguration<EntiteitBeheerder>
    {
        public void Configure(EntityTypeBuilder<EntiteitBeheerder> builder)
        {
            builder.ToTable("EntiteitBeheerders");

            builder.HasOne(eb => eb.Beheerder).WithMany(b => b.Entiteiten);
            builder.HasOne(eb => eb.Entiteit).WithMany(b => b.Beheerders);
        }
    }
}