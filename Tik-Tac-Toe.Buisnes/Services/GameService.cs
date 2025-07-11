using Tik_Tac_Toe.Core.Abstractions;
using Tik_Tac_Toe.Core.Models;

namespace Tik_Tac_Toe.Buisnes.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly DataField _dataField;
        public GameService(IGameRepository gameRepository, DataField dataField)
        {
            _gameRepository = gameRepository;
            _dataField = dataField;
        }
        public async Task<string> GetFieldFromDatabase()
        {
            string strField = await _gameRepository.GetField();
            _dataField.dataField = strField.Split('/');
            _dataField.dataFieldSize = _dataField.dataField.Length;
            if (_dataField.dataField.Length == 0) return "GameNotFound";
            return strField;
        }
        public async Task<GameField> CreateNewGame()
        {
            string field = "";
            for (int i = 0; i < _dataField.dataFieldSize; i++)
            {
                field += new string('?', _dataField.dataFieldSize);
                if (i != _dataField.dataFieldSize - 1) field += "/";
            }
            
            var gameField = GameField.Create(
                Guid.NewGuid(),
                _dataField.dataFieldSize,
                field);
            if (gameField.Error != "") return null;

            _dataField.dataField = field.Split("/");

            _dataField.ResetGame();
            return await _gameRepository.Create(gameField.GameField);
        }
        public async Task<string> Move(int x, int y, char value)
        {
            if (value.ToString() != _dataField.GetWhoMove()) return "Its not your move now";

            if (x < 0 || x >= _dataField.dataField.Length) return "X pos is out of bounds of field";
            if (y < 0 || y >= _dataField.dataField.Length) return "Y pos is out of bounds of field";

            char[] charStr = _dataField.dataField[y].ToCharArray();
            if (charStr[x] != '?') return "Cell is busy";
            if (_dataField.GetMoveCounter() % 3 == 0) value = GetRandomValue(value);
            charStr[x] = value;
            _dataField.dataField[y] = new string(charStr);

            await _gameRepository.SetField(_dataField.dataField);
            string gameState = CheckGameState(x, y, value);
            _dataField.SwitchPlayer();
            return gameState;
        }
        private string CheckGameState(int lastMoveX, int lastMoveY, char lastMoveValue)
        {
            if (CheckDraw()) return "_Draw";

            bool isWin =
            //Lines
               CheckWin(lastMoveX, lastMoveY, 1, 0, lastMoveValue) //>
            || CheckWin(lastMoveX, lastMoveY, -1, 0, lastMoveValue) //<
            || CheckWin(lastMoveX, lastMoveY, 0, 1, lastMoveValue) //^
            || CheckWin(lastMoveX, lastMoveY, 0, -1, lastMoveValue) //V
            // Diagonals
            || CheckWin(lastMoveX, lastMoveY, 1, 1, lastMoveValue) //>^
            || CheckWin(lastMoveX, lastMoveY, -1, 1, lastMoveValue) //<^
            || CheckWin(lastMoveX, lastMoveY, -1, -1, lastMoveValue) //<V
            || CheckWin(lastMoveX, lastMoveY, 1, -1, lastMoveValue); //>V

            if (isWin) return $"_Win {lastMoveValue}";

            return "_InProcess";
        }
        private bool CheckWin(int x, int y, int dirX, int dirY, char value)
        {
            int count = 1;
            int currentX = x + dirX;
            int currentY = y + dirY;
            while (
                (currentX >= 0 && currentX < _dataField.dataFieldSize) && 
                (currentY >= 0 && currentY < _dataField.dataFieldSize) &&
                _dataField.dataField[currentY][currentX] == value)
            {
                count++;
                currentX += dirX;
                currentY += dirY;
                if (count == _dataField.comboToWin) break;
            }
            return count >= _dataField.comboToWin ? true : false;
        }
        private bool CheckDraw()
        {
            for (int i = 0; i < _dataField.dataFieldSize; i++)
            {
                if (_dataField.dataField[i].Contains("?")) return false;
            }
            return true;
        }
        private char GetRandomValue(char playerValue) { 
            Random rd = new Random();
            if (rd.Next(0, 10) == 5) return GetEnemyValue(playerValue);
            else return playerValue;
        }
        private char GetEnemyValue(char playerValue) {
            if (playerValue == 'x') return 'o';
            else return 'x';
        }
    }
}
