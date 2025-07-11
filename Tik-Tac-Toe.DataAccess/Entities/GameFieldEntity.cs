namespace Tik_Tac_Toe.DataAccess.Entities
{
    public class GameFieldEntity
    {
        public Guid Id { get; set; }
        public int FieldSize { get; set; }
        public string Field { get; set; }
    }
}
