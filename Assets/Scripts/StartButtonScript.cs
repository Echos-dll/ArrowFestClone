
using UnityEngine;
using UnityEngine.EventSystems;

public class StartButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    GameManager Manager;
    private void Awake()
    {
        Manager = FindObjectOfType<GameManager>();
    }

    public void OnPointerDown(PointerEventData data)
    {
        //Do the thing when button pressed
        Debug.Log("Start Button pressed!");
        Manager.setIsRuning(true);
        Manager.CanMove = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        //Do the thing when button released
        Debug.Log("Button released!");
    }
}
