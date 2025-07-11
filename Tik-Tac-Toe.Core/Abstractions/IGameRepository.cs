using Tik_Tac_Toe.Core.Models;

namespace Tik_Tac_Toe.Core.Abstractions
{
    public interface IGameRepository
    {
        Task<string> GetField();
        Task<GameField> Create(GameField gameField);
        Task<string> Delete();
        Task<string> SetField(string[] field);
    }
}