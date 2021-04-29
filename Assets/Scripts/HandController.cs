using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// Code from https://www.youtube.com/watch?v=DxKWq7z4Xao&list=WL&index=19&t=869s

[RequireComponent(typeof(ActionBasedController))]
public class HandController : MonoBehaviour
{
    ActionBasedController controller;
    public Hand hand;
   
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
    }
}
