using Tik_Tac_Toe.Core.Models;
using Tik_Tac_Toe.DataAccess.Entities;

namespace Tik_Tac_Toe.DataAccess
{
    public class MyMapper
    {
        public static GameField ConvertToGameField(GameFieldEntity entity)
        {
            GameField gameField = GameField.Create(entity.Id, entity.FieldSize, entity.Field).GameField;
            return gameField;
        }
    }
}
