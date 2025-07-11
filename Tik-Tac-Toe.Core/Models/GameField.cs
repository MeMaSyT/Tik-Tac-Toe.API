namespace Tik_Tac_Toe.Core.Models
{
    public class GameField
    {
        public const int MIN_FIELD_SIZE = 3;
        private GameField(Guid id, int fieldSize, string field)
        {
            Id = id;
            FieldSize = fieldSize;
            Field = field;
        }
        public Guid Id { get; }
        public int FieldSize { get; } = MIN_FIELD_SIZE;
        public string Field { get; } = String.Empty;

        public static (GameField GameField, string Error) Create(Guid id, int fieldSize, string field)
        {
            string error = string.Empty;

            if (fieldSize < MIN_FIELD_SIZE) error = $"The field size is less than {MIN_FIELD_SIZE}";
            return (new GameField(id, fieldSize, field), error);
        }
    }
}
