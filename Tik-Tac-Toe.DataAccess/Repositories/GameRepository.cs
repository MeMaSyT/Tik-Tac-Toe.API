using Microsoft.EntityFrameworkCore;
using Tik_Tac_Toe.Core.Abstractions;
using Tik_Tac_Toe.Core.Models;
using Tik_Tac_Toe.DataAccess.Entities;

namespace Tik_Tac_Toe.DataAccess.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly GameDbContext _context;
        public GameRepository(GameDbContext context)
        {
            _context = context;
        }

        public async Task<string> GetField()
        {
            var entity = await _context.GameFields
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (entity == null) return "";

            return entity.Field;
        }
        public async Task<GameField> Create(GameField gameField)
        {
            GameFieldEntity entity = new GameFieldEntity
            {
                Id = gameField.Id,
                FieldSize = gameField.FieldSize,
                Field = gameField.Field
            };
            await Delete();
            await _context.GameFields.AddAsync(entity);
            await _context.SaveChangesAsync();

            return MyMapper.ConvertToGameField(entity);
        }
        public async Task<string> Delete()
        {
            int deletedCount = await _context.GameFields.ExecuteDeleteAsync();
            if (deletedCount == 0) return "GameNotFound";
            return "";
        }
        public async Task<string> SetField(string[] field)
        {
            string oneLineString = field[0] + "/";
            for (int i = 1; i < field.Length; i++)
            {
                if (i == field.Length - 1) oneLineString += field[i];
                else oneLineString += field[i] + "/";
            }

            int updatedCount = await _context.GameFields.ExecuteUpdateAsync(s =>
                s.SetProperty(g => g.Field, oneLineString));
            if (updatedCount == 0) return "GameNotFound";
            return "";
        }
    }
}
