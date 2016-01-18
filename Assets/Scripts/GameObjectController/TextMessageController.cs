using UnityEngine;
using UnityEngine.UI;

namespace GameObjectController
{
    public class TextMessageController : MonoBehaviour
    {
        [SerializeField] private Text textMessage;

        public void SetTextMessage(string text, Color color)
        {
            textMessage.text = text;
            textMessage.color = color;
        }
    }
}