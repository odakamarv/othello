using UnityEngine;
using BaseController;

namespace GameObjectController
{
    public class ScoreBoardController : MonoBehaviour
    {
        [SerializeField] private TextController scoreBlack;
        [SerializeField] private TextController scoreWhite;

        private ConstValues constValues = new ConstValues();

        public void UpdateScoreBoard(BoardInfo boardInfo)
        {
            var countBlack = boardInfo.CountBlack();
            var countWhite = boardInfo.CountWhite();

            var textBlack = Utilities.ReplaceTextWithColorString(constValues.TextFormatScore, constValues.StringBlack);
            var textWhite = Utilities.ReplaceTextWithColorString(constValues.TextFormatScore, constValues.StringWhite);

            textBlack = string.Format(textBlack, countBlack);
            textWhite = string.Format(textWhite, countWhite);

            scoreBlack.SetText(textBlack);
            scoreWhite.SetText(textWhite);
        }
    }
}