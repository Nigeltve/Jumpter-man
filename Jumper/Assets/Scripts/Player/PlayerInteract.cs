using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    bool canInteract = false;

    private void Start()
    {
        EventHandler.curr.onDoorTriggerEnter += SetCanInteractOn;
        EventHandler.curr.onDoorTriggerExit += setCanInteractOff;
    }

    private void OnDestroy()
    {
        EventHandler.curr.onDoorTriggerEnter += SetCanInteractOn;
        EventHandler.curr.onDoorTriggerExit += setCanInteractOff;
    }

    private void Update()
    {
        if(Input.GetButtonDown("Interact") && canInteract)
        {
            EventHandler.curr.TriggerOnPlayerInteract();
        }
    }


    private void SetCanInteractOn()
    {
        canInteract = true;
    }

    private void setCanInteractOff() 
    {
        canInteract = false;
    }
}
