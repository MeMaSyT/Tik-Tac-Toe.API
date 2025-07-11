using Microsoft.EntityFrameworkCore;
using Tik_Tac_Toe.DataAccess.Configurations;
using Tik_Tac_Toe.DataAccess.Entities;

namespace Tik_Tac_Toe.DataAccess
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GameFieldConfiguration());

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<GameFieldEntity> GameFields { get; set; }
    }
}