using UnityEngine;
using UnityEngine.UI;

namespace GameObjectController
{
    public class LogBoardController : MonoBehaviour
    {
        [SerializeField] private GameObject content;
        [SerializeField] private Text logBoardOneLine;

        public void AddLine(string text, Color color)
        {
            var addTo = content.transform;
            var addLine = GameObject.Instantiate(logBoardOneLine);

            addLine.text = text;
            addLine.color = color;
            addLine.GetComponent<RectTransform>().SetParent(addTo, false);
        }

        public void RemoveLastLine()
        {
            var removeFrom = content.transform;
            var removeLine = removeFrom.GetChild(removeFrom.childCount - 1);

            removeLine.gameObject.GetComponent<RectTransform>().SetParent(null);
            GameObject.Destroy(removeLine.gameObject);
        }
    }
}