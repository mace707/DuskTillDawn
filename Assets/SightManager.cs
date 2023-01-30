using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightManager : MonoBehaviour
{
    [SerializeField] private ZombieAnimationEvents ZombieMain;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ZombieMain.AttackPlayer(other.transform);
        }
    }
}
