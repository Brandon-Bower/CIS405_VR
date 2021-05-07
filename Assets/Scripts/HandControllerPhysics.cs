using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

// Based on code from https://www.youtube.com/watch?v=DxKWq7z4Xao&list=WL&index=19&t=869s
// and https://www.youtube.com/watch?v=RwGIyRy-Lss&list=WL&index=19&t=123s

[RequireComponent(typeof(ActionBasedController))]
public class HandControllerPhysics : MonoBehaviour
{
    ActionBasedController controller;
    public HandPhysics hand;
    public InputActionReference touchAction;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ActionBasedController>();
    }

    // Update is called once per frame
    void Update()
    {
        hand.SetGrip(controller.selectAction.action.ReadValue<float>());
        hand.SetTrigger(controller.activateAction.action.ReadValue<float>());
        hand.SetThumb(touchAction.action.ReadValue<float>());
    }
}
