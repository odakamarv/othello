using UnityEngine;

namespace GameObjectController.BaseController
{
    public class BaseStoneController : MonoBehaviour
    {
        public void Turn()
        {
            GetComponentInChildren<Animator>().SetTrigger("doTurn");
        }

        public void setBlack()
        {
            GetComponentInChildren<Animator>().SetTrigger("setBlack");
        }

        public void setWhite()
        {
            GetComponentInChildren<Animator>().SetTrigger("setWhite");
        }
    }
}