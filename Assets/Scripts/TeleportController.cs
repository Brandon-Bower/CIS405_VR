using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

// Code from UNITY XR | ACTION BASED TELEPORTING! tutorial
// https://www.youtube.com/watch?v=wGvh7Suo1h4&list=WL&index=25&t=370s

public class TeleportController : MonoBehaviour
{
    public InputActionReference teleportationActivationReference;

    [Space]
    public UnityEvent onTeleportActivate;
    public UnityEvent onTeleportCancel;

    private void OnEnable()
    {
        teleportationActivationReference.action.performed += TeleportModeActivate;
        teleportationActivationReference.action.canceled += TeleportModeCancel;
    }

    private void OnDisable()
    {
        teleportationActivationReference.action.performed -= TeleportModeActivate;
        teleportationActivationReference.action.canceled -= TeleportModeCancel;
    }

    private void TeleportModeActivate(InputAction.CallbackContext obj) => onTeleportActivate.Invoke();

    private void TeleportModeCancel(InputAction.CallbackContext obj) => Invoke("DeactivateTeleporter", .1f);

    void DeactivateTeleporter() => onTeleportCancel.Invoke();

}
