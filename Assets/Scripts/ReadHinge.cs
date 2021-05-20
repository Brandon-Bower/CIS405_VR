using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadHinge : MonoBehaviour
{
    public HingeJoint _joint;

    public Color colorA;
    public Color colorB;
    
    
    // Update is called once per frame
    void Update()
    {
        float t = (_joint.angle - _joint.limits.min) / (_joint.limits.max - _joint.limits.min);
        Debug.Log(t);
        GetComponent<MeshRenderer>().material.SetColor("_Color", Color.Lerp(colorA, colorB, t));

    }

}
