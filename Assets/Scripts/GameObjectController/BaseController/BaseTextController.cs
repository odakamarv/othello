using UnityEngine;
using UnityEngine.UI;

namespace BaseController
{
    public class TextController : MonoBehaviour
    {
        [SerializeField] private Text text;

        public void SetText(string text)
        {
            this.text.text = text;
        }

        public void SetColor(Color color)
        {
            this.text.color = color;
        }

        public void SetTextWithColor(string text, Color color)
        {
            SetText(text);
            SetColor(color);
        }
    }
}