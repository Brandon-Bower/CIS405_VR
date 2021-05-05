using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Based on code from https://www.youtube.com/watch?v=DxKWq7z4Xao&list=WL&index=19&t=869s

[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{   
    public float speed;
    Animator animator;
    SkinnedMeshRenderer mesh;
    private float gripTarget, triggerTarget, thumbTarget;
    private float gripCurrent, triggerCurrent, thumbCurrent;
    private string animatorGripParam = "Grip";
    private string animatorTriggerParam = "Trigger";
    private string animatorThumbParam = "Thumb";


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();
    }

    internal void SetThumb(float v)
    {
        thumbTarget = v;
    }

    internal void SetGrip(float v)
    {
        gripTarget = v;
    }

    internal void SetTrigger(float v)
    {
        triggerTarget = v;
    }

    void AnimateHand()
    {
        if(gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorGripParam, gripCurrent);
        }        
        if(triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorTriggerParam, triggerCurrent);
        }
        if (thumbCurrent != thumbTarget)
        {
            thumbCurrent = Mathf.MoveTowards(thumbCurrent, thumbTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorThumbParam, thumbCurrent);
        }
    }

    public void ToggleVisibility()
    {
        mesh.enabled = !mesh.enabled;
    }
}
