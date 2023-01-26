using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightManager : MonoBehaviour
{
    [SerializeField] private Animator ZombieAnimator;
    [SerializeField] private ZombieAnimationEvents ZombieMain;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRunState()
    {
        ZombieMain.CurrentState = ZombieAnimationEvents.STATE.STATE_RUN;
    }

    public void SetAlertAndChaseAnimations()
    {
        ZombieMain.CurrentState = ZombieAnimationEvents.STATE.STATE_SCREAM;
        Invoke("SetRunState", 0.25f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {            
            ZombieMain.StopAllCoroutines();

            Vector3 dir = other.transform.position - ZombieMain.transform.position;
            dir.y = 0; // keep the direction strictly horizontal             
            ZombieMain.TargetRotation = Quaternion.LookRotation(dir, Vector3.up);

            if (ZombieMain.CurrentState != ZombieAnimationEvents.STATE.STATE_SCREAM)
                SetAlertAndChaseAnimations();
        }
    }
}
