using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tik_Tac_Toe.DataAccess.Entities;

namespace Tik_Tac_Toe.DataAccess.Configurations
{
    public class GameFieldConfiguration : IEntityTypeConfiguration<GameFieldEntity>
    {
        public void Configure(EntityTypeBuilder<GameFieldEntity> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.FieldSize)
                .IsRequired();

            builder.Property(f => f.Field)
                .IsRequired();
        }
    }
}
