using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObject : MonoBehaviour
{
    Vector3 startPos;
    Quaternion startRot;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.position;
        startRot = gameObject.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Respawn")
        {
            gameObject.transform.position = startPos;
            gameObject.transform.rotation = startRot;
        }
    }
}
