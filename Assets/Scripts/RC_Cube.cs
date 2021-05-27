using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RC_Cube : MonoBehaviour
{
    [SerializeField] private HingeJoint forwardBackJoint;
    [SerializeField] private HingeJoint sideToSideJoint;
    [SerializeField] private float speed = 10;
    [SerializeField] private bool invertX = true;
    [SerializeField] private bool invertZ = true;

    private int iX = 1, iZ = 1;

    private void Start()
    {
        if (invertX)
            iX = -1;
        if (invertZ)
            iZ = -1; 
    }


    // Update is called once per frame
    void Update()
    {
        float xMove = transform.position.x + (iX * sideToSideJoint.angle * speed * Time.deltaTime);
        float zMove = transform.position.z + (iZ * forwardBackJoint.angle * speed * Time.deltaTime);
        transform.position = new Vector3(xMove, transform.position.y, zMove);
    }
}
