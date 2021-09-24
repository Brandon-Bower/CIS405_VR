using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// Based on code from https://www.youtube.com/watch?v=DxKWq7z4Xao&list=WL&index=19&t=869s
// and https://www.youtube.com/watch?v=RwGIyRy-Lss&list=WL&index=19&t=123s

[RequireComponent(typeof(Animator))]
public class HandPhysics : MonoBehaviour
{   
    // Animation
    public float speed;
    Animator animator;
    SkinnedMeshRenderer mesh;
    private float gripTarget, triggerTarget, thumbTarget;
    private float gripCurrent, triggerCurrent, thumbCurrent;
    private string animatorGripParam = "Grip";
    private string animatorTriggerParam = "Trigger";
    private string animatorThumbParam = "Thumb";

    // Physics Movement
    [SerializeField] private GameObject followObject;
    [SerializeField] private float followSpeed = 30f;
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;
    private Transform _followTarget;
    private Rigidbody _body;
    private bool moving = false;
    private bool teleporting = false;

    private int origLayer;

    // Start is called before the first frame update
    void Start()
    {
        // Animation
        animator = GetComponent<Animator>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();

        // Physics Movement
        _followTarget = followObject.transform;
        _body = GetComponent<Rigidbody>();
        _body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        _body.interpolation = RigidbodyInterpolation.Interpolate;
        _body.mass = 20f;

        // Teleport hands
        _body.position = _followTarget.position;
        _body.rotation = _followTarget.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();

        if (moving || teleporting)
        {
            // Lock hands so they don't float/fly around when player moves or teleports
            _body.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            _body.isKinematic = true;
            gameObject.transform.position = _followTarget.position;
            gameObject.transform.rotation = _followTarget.rotation;
        }
        else
        {
            // Have hands interact with environment when player is not moving
            _body.isKinematic = false;
            _body.collisionDetectionMode = CollisionDetectionMode.Continuous;
            PhysicsMove();
        }
    }

    private void PhysicsMove()
    {
        // Position
        var positionWithOffset = _followTarget.position + positionOffset;
        float distance = Vector3.Distance(positionWithOffset, transform.position);
        _body.velocity = (positionWithOffset - transform.position).normalized * followSpeed * distance;

        // Rotation
        var rotationWithOffset = _followTarget.rotation * Quaternion.Euler(rotationOffset);
        var q = rotationWithOffset * Quaternion.Inverse(_body.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        _body.angularVelocity = angle * axis * Mathf.Deg2Rad * rotateSpeed;
    }

    internal void SetThumb(float v) => thumbTarget = v; // Thumb animation driver value

    internal void SetGrip(float v) => gripTarget = v; // Grip animation driver value

    internal void SetTrigger(float v) => triggerTarget = v; // Index finger animation driver value

    internal void SetMoving(bool v) => moving = v;

    public void SetTeleportation(bool v) => teleporting = v;

    internal void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Make it so held objects do not collide with the player
        GetComponent<BoxCollider>().enabled = false;
        origLayer = args.interactable.gameObject.layer;
        args.interactable.gameObject.layer = LayerMask.NameToLayer("Player");
    }
    internal void OnSelectExited(SelectExitEventArgs args)
    {
        // Return objects to original layer when released
        Invoke(nameof(ResetCollider), .2f); // Give a little bit of time before turning the collider back on
        args.interactable.gameObject.layer = origLayer;
    }

    internal void ResetCollider()
    {
        GetComponent<BoxCollider>().enabled = true;
    }


    void AnimateHand()
    {
        if(gripCurrent != gripTarget) // Update grip animation
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorGripParam, gripCurrent);
        }        
        if(triggerCurrent != triggerTarget) // Update index animation
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorTriggerParam, triggerCurrent);
        }
        if (thumbCurrent != thumbTarget) // Update thumb animation
        {
            thumbCurrent = Mathf.MoveTowards(thumbCurrent, thumbTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorThumbParam, thumbCurrent);
        }
    }

    public void ToggleVisibility() // Hide hands when something is held
    {
        mesh.enabled = !mesh.enabled;
    }
}
