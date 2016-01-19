using UnityEngine;
using UnityEngine.UI;
using GameObjectController.BaseController.Interface;

namespace GameObjectController.BaseController
{
    abstract class ButtonController : MonoBehaviour, IButtonInterface
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

        public abstract void PrintClickLog();
    }
}