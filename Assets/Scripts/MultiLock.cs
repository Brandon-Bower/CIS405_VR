using UnityEngine;
using UnityEngine.Events;

public class MultiLock : MonoBehaviour
{
    public bool[] locks;
    [Space]
    public UnityEvent locked;
    public UnityEvent unlocked;
    
    // Start is called before the first frame update
    void Start()
    {
        locked.Invoke();
        
        // Make sure all locks are locked
        for (int i = 0; i < locks.Length; i++)
            locks[i] = false;
    }

    // Unlock a lock
    public void unlock()
    {
        for (int i = 0; i < locks.Length; i++)
        {
            // Unlock first unlocked lock
            if (locks[i] == false)
            {
                locks[i] = true;
                
                // All locks are unlocked
                if(i == locks.Length - 1)
                    unlocked.Invoke();
                return;
            }
        }
        return;
    }

    // Lock a lock
    public void relock(){
        // Lock first unlocked lock
        for (int i = 0; i < locks.Length; i++)
        {
            if (locks[i] == true)
            {
                locks[i] = false;
                return;
            }
        }
        return;
    }
}
