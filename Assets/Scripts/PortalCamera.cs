using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code by Brackeys found at https://www.youtube.com/watch?v=cuQao3hEKfs&list=WL&index=27&t=185s

public class PortalCamera : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
        transform.position = portal.position + playerOffsetFromPortal;
    }
}
