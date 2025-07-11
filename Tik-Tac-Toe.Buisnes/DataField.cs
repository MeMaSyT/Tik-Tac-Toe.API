using Microsoft.Extensions.Configuration;

namespace Tik_Tac_Toe.Buisnes
{
    public class DataField
    {
        public DataField(IConfiguration configuration)
        {
            try
            {
                dataFieldSize = int.Parse(configuration.GetSection("GameSettings")["GameFieldSize"]);
                comboToWin = int.Parse(configuration.GetSection("GameSettings")["ComboToWin"]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("dataFieldSize = " + dataFieldSize);
            Console.WriteLine("comboToWin = " + comboToWin);
        }
        public string[] dataField { get; set; } = [];
        public int dataFieldSize { get; set; } = 3;
        private string whoMove = "x";
        private int moveCounter = 1;
        public int comboToWin { get; set; } = 3;

        public void SwitchPlayer() {
            if (whoMove == "x") whoMove = "o";
            else whoMove = "x";
            moveCounter++;
        }
        public void ResetGame()
        {
            whoMove = "x";
            moveCounter = 1;
        }
        public string GetWhoMove() => whoMove;
        public int GetMoveCounter() => moveCounter;
    }
}
