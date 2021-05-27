using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseFlag : MonoBehaviour
{
    [SerializeField]private Transform flagStart;
    [SerializeField] private Transform flagEnd;

    [SerializeField] private HingeJoint _joint;
    [SerializeField] private  float rotationsNeeded;
    
    private float lastAngle;
    private float goal;
    private float current;
    private float threshold = 20;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = flagStart.position;
        transform.rotation = flagStart.rotation;
        lastAngle = _joint.angle;
        if (rotationsNeeded == 0)
            rotationsNeeded = 1;
        goal = rotationsNeeded * 360;

    }

    // Update is called once per frame
    void Update()
    {
        float distance = GetDistance(_joint.angle);
        current += distance;
        if (current > goal)
            current = goal;
        else if (current < 0)
            current = 0;
        float t = current / goal;
        transform.position = Vector3.Lerp(flagStart.position, flagEnd.position, t);
        transform.rotation = Quaternion.Slerp(flagStart.rotation, flagEnd.rotation, t);
    }

    private float GetDistance(float angle)
    {
        if (angle < 0)
            angle = 360 + angle;
        float distance = angle - lastAngle;
        if (angle < threshold && lastAngle > 360 - threshold)
        {
            distance = 360 - lastAngle + angle;
        }
        else if ((lastAngle < threshold && angle > 360 - threshold))
        {
            distance = angle - 360 - lastAngle;
        }
        
        lastAngle = angle;
        return distance;
    }
}
