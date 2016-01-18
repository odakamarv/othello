using UnityEngine;

public class MonitorClickEvent : MonoBehaviour
{
    //[SerializeField] private MainController mainController;
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                //mainController.ReceiveClickEvent(raycastHit.transform.gameObject);
            }
        }
    }
}