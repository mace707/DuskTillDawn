using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSkeletonAnimationEvents : MonoBehaviour
{
    public float speed = 50.0f;

    void TriggerMovement()
    {
        Debug.Log("Movement Triggered");
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
