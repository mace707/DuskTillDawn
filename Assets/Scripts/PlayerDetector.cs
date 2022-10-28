using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public Transform PlayerTransform;
    private Queue<Transform> MovementTracker;

    // Start is called before the first frame update
    void Start()
    {        
        MovementTracker = new Queue<Transform>();
        InvokeRepeating("TrackPlayer", 5.0f, 2.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Player")
        {
            Debug.Log("POSITION 1: " + PlayerTransform.position);
            PlayerTransform.position = MovementTracker.Dequeue().position;
            Debug.Log("POSITION 2: " + PlayerTransform.position);
            Debug.Log("collided");
        }
        
    }

    private void TrackPlayer()
    {
        if (MovementTracker.Count >= 5)
            MovementTracker.Dequeue();
        
        MovementTracker.Enqueue(PlayerTransform.gameObject.transform);
    }
}
