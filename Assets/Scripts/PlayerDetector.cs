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

    private void TrackPlayer()
    {
        if (MovementTracker.Count >= 5)
            MovementTracker.Dequeue();
        
        MovementTracker.Enqueue(PlayerTransform.gameObject.transform);
    }
}
