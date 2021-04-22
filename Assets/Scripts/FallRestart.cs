using UnityEngine;
using UnityEngine.SceneManagement;

public class FallRestart : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
