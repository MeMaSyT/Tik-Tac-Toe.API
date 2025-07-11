using Microsoft.Extensions.Configuration;

namespace Tik_Tac_Toe.Buisnes
{
    public class DataField
    {
        public DataField(IConfiguration configuration)
        {
            var config_dataFieldSize = configuration.GetSection("GameSettings")["GameFieldSize"];
            var config_comboToWin = configuration.GetSection("GameSettings")["ComboToWin"];
            if (config_dataFieldSize != "${GAME_FIELD_SIZE}" && config_comboToWin != "${GAME_WIN_COMBO}") {
                dataFieldSize = int.Parse(config_dataFieldSize);
                comboToWin = int.Parse(config_comboToWin);
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
