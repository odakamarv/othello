using BaseController;

namespace GameObjectController
{
    public class LogBoardController : BaseLogBoardController
    {
        private ConstValues constValues = new ConstValues();
        private GetPlayerConstValues getPlayerConstValues = new GetPlayerConstValues();

        public void AddPutLog(GameState gameState, int col, int row)
        {
            var turnPlayerColorString = getPlayerConstValues.GetTurnPlayerColorString(gameState);
            var turnPlayerTextColor   = getPlayerConstValues.GetTurnPlayerTextColor(gameState);

            var logText = Utilities.ReplaceTextWithColorString(constValues.TextFormatPutLog, turnPlayerColorString);
            logText = string.Format(logText, col, row);

            AddLine(logText, turnPlayerTextColor);
        }

        public void AddPathLog(GameState gameState)
        {
            var turnPlayerColorString = getPlayerConstValues.GetTurnPlayerColorString(gameState);
            var turnPlayerTextColor   = getPlayerConstValues.GetTurnPlayerTextColor(gameState);

            var logText = Utilities.ReplaceTextWithColorString(constValues.TextFormatPathLog, turnPlayerColorString);

            AddLine(logText, turnPlayerTextColor);
        }
    }
}