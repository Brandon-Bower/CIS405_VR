using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float offsetX;
    public float offsetY;
    public float offsetZ;

    public Transform player;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(
            player.position.x + offsetX,
            player.position.y + offsetY,
            player.position.z + offsetZ);
    }
}
