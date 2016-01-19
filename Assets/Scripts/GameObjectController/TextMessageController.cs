using BaseController;
using BoardStruct;

namespace GameObjectController
{
    public class TextMessageController : TextController
    {
        private ConstValues constValues = new ConstValues();
        private GetPlayerConstValues getPlayerConstValues = new GetPlayerConstValues();

        public void SetTurnMessage(GameState gameState)
        {
            var turnPlayerTextColor = getPlayerConstValues.GetTurnPlayerTextColor(gameState);
            var turnPlayerColorString = getPlayerConstValues.GetTurnPlayerColorString(gameState);

            var turnText = Utilities.ReplaceTextWithColorString(constValues.TextFormatTurn, turnPlayerColorString);

            SetTextWithColor(turnText, turnPlayerTextColor);
        }

        public void SetWinMessage(BoardValues boardValue)
        {
            if(boardValue == BoardValues.Black)
            {
                var winText = Utilities.ReplaceTextWithColorString(constValues.TextFormatWin, constValues.StringBlack);
                SetTextWithColor(winText, constValues.TextColorBlack);
            }
            else
            {
                var winText = Utilities.ReplaceTextWithColorString(constValues.TextFormatWin, constValues.StringWhite);
                SetTextWithColor(winText, constValues.TextColorWhite);
            }
        }

        public void SetDrawMessage()
        {
            SetTextWithColor(constValues.TextFormatDraw, constValues.TextColorOther);
        }

        public void SetResultMessage(BoardInfo boardInfo)
        {
            var countBlack = boardInfo.CountBlack();
            var countWhite = boardInfo.CountWhite();

            if (countBlack > countWhite)
            {
                SetWinMessage(BoardValues.Black);
            }
            else if (countBlack < countWhite)
            {
                SetWinMessage(BoardValues.White);
            }
            else
            {
                SetDrawMessage();
            }
        }
    }
}