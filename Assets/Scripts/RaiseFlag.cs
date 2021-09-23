using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseFlag : MonoBehaviour
{
    [SerializeField] private Transform flagStart;
    [SerializeField] private Transform flagEnd;

    [SerializeField] private HingeJoint _joint;
    [SerializeField] private  float rotationsNeeded;
    // turning crank clockwise raises flag by default
    [SerializeField] private bool counterClockwise = false;

    private float lastAngle;
    private float goal;
    private float current;
    private float threshold = 20;


    // Start is called before the first frame update
    void Start()
    {
        // Put flag/object in starting position
        transform.position = flagStart.position;
        transform.rotation = flagStart.rotation;
        lastAngle = _joint.angle;
        if (rotationsNeeded == 0)
            rotationsNeeded = 1;
        // Degrees of rotation needed to get from flagStart to flagEnd
        goal = rotationsNeeded * 360;

    }

    // Update is called once per frame
    void Update()
    {
        float distance = GetDistance(_joint.angle);
        current += distance;
        // Can't move flag/object past start or end
        if (current > goal)
            current = goal;
        else if (current < 0)
            current = 0;
        float t = current / goal;
        transform.position = Vector3.Lerp(flagStart.position, flagEnd.position, t);
        transform.rotation = Quaternion.Slerp(flagStart.rotation, flagEnd.rotation, t);
    }

    // Function to find distance traveled since last frame update
    private float GetDistance(float angle)
    {
        if (angle < 0)
            // angle vaules go 0 -> 180 -> -180 -> 0
            // convert so they instead go 0 -> 360
            angle = 360 + angle;
        float distance = angle - lastAngle; // calculate distance tavelend since last frame
        if (angle < threshold && lastAngle > 360 - threshold) // check going from 360 to 0
        {
            distance = 360 - lastAngle + angle;
        }
        else if ((lastAngle < threshold && angle > 360 - threshold)) // check going from 0 to 360
        {
            distance = angle - 360 - lastAngle;
        }
        
        lastAngle = angle;
        if (counterClockwise) distance *= -1;
        return distance;
    }
}
