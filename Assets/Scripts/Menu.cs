using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Menu : MonoBehaviour
{
    public InputActionReference OpenMenu;
    bool open = false;
    bool releaseCheck = true;

    [Space]
    public UnityEvent menuOpen;
    public UnityEvent menuClose;

    // Start is called before the first frame update
    private void Start()
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
