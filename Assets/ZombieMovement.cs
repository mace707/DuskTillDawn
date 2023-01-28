using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private float Speed = 5f;    
    [SerializeField] private bool MovementEnbaled = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EnableMovement()
    {
        MovementEnbaled = true;
    }

    public void DisableMovement()
    {
        MovementEnbaled = false;
    }

    public void MovingForward()
    {
        if (Speed > 0)
            return;

        Speed *= -1;        
    }

    public void MovingBackwards()
    {
        if (Speed < 0)
            return;

        Speed *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (MovementEnbaled)        
            transform.position += transform.forward * Speed * Time.deltaTime;        
    }
}
