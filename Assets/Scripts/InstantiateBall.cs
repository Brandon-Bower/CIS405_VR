using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBall : MonoBehaviour
{
    [SerializeField] private Material[] ballMaterials;
    [SerializeField] private float minBallRadius;
    [SerializeField] private float maxBallRadius;
    [SerializeField] private float spawnRadius;
    [SerializeField] private int spawnRate = 1;


    public void SpawnBalls()
    {
        for (int i = 0; i < spawnRate; i++)
            CreateBall();
    }
    
    internal void CreateBall()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Vector3 pos = transform.position;
        sphere.transform.position = new Vector3(Random.Range(pos.x - spawnRadius, pos.x + spawnRadius), 
                                                pos.y, 
                                                Random.Range(pos.z - spawnRadius, pos.z + spawnRadius)) ;
        Rigidbody _body = sphere.AddComponent<Rigidbody>();
        float size = Random.Range(minBallRadius, maxBallRadius);
        sphere.transform.localScale = new Vector3(size, size, size);
        sphere.GetComponent<MeshRenderer>().material = ballMaterials[Random.Range(0, ballMaterials.Length)];
        sphere.AddComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>();
        
    }

}
