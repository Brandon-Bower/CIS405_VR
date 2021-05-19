using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

// Based on code from https://www.youtube.com/watch?v=DxKWq7z4Xao&list=WL&index=19&t=869s
// and https://www.youtube.com/watch?v=RwGIyRy-Lss&list=WL&index=19&t=123s

[RequireComponent(typeof(ActionBasedController))]
[RequireComponent(typeof(XRRayInteractor))]
public class HandControllerPhysics : MonoBehaviour
{
    ActionBasedController controller;
    public HandPhysics hand;
    public InputActionReference touchAction;
    public InputActionReference thumbStickActionL;
    public InputActionReference thumbStickActionR;

    private XRRayInteractor _Interactor;

    protected void OnEnable()
    {
        _Interactor = GetComponent<XRRayInteractor>();
        _Interactor.selectEntered.AddListener(hand.OnSelectEntered);
        _Interactor.selectExited.AddListener(hand.OnSelectExited);
    }

    protected void OnDisable()
    {
        _Interactor.selectEntered.RemoveListener(hand.OnSelectEntered);
        _Interactor.selectExited.RemoveListener(hand.OnSelectExited);
    }

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
        hand.SetMoving(thumbStickActionL.action.triggered || thumbStickActionR.action.triggered);
    }
}
