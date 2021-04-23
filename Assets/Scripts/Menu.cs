using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Menu : MonoBehaviour
{
    public InputActionReference OpenMenu;
    public Transform player;

    [Space]
    public UnityEvent menuOpen;
    public UnityEvent menuClose;

    bool open = false;
    bool releaseCheck = true;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position;
        this.transform.rotation = player.transform.rotation;
    }

    private void OnEnable()
    {
        OpenMenu.action.performed += toggle;
        OpenMenu.action.canceled += buttonReleased;
    }

    private void OnDisable()
    {
        OpenMenu.action.performed -= toggle;
        OpenMenu.action.canceled -= buttonReleased;
    }
    private void toggle(InputAction.CallbackContext obj)
    {
        if (releaseCheck)
        {
            if (open)
                menuClose.Invoke();
            else
                menuOpen.Invoke();
            open = !open;
            releaseCheck = false;
        }

    }
    private void buttonReleased(InputAction.CallbackContext obj) => releaseCheck = true;
}
