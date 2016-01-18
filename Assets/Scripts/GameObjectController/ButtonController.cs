using UnityEngine;
using UnityEngine.UI;

namespace GameObjectController
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private Button button;

        public void SetEnabled()
        {
            button.interactable = true;
        }

        public void SetDisabled()
        {
            button.interactable = false;
        }
    }
}