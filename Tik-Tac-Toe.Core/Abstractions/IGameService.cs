using Tik_Tac_Toe.Core.Models;

namespace Tik_Tac_Toe.Core.Abstractions
{
    public interface IGameService
    {
        Task<GameField> CreateNewGame();
        Task<string> GetFieldFromDatabase();
        Task<string> Move(int x, int y, char value);
    }
}