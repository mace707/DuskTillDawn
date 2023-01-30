using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public GameObject[] Guns;
    
    private Animator ActiveGunAnimator;
    private Gun ActiveGun;

    public int Active = 0;

    [SerializeField] private AudioSource StepAudioSource;
    [SerializeField] private AudioClip[] StepAudioClips;

    private void Start()
    {
        SetActiveGun(0);
    }

    public enum GUNSTATES
    {
        GUNSTATE_IDLE,
        GUNSTATE_WALK,
        GUNSTATE_RUN,
        GUNSTATE_RELOAD,
        GUNSTATE_SHOOT,
        GUNSTATE_EQUIP
    }

    public void SetState(GUNSTATES state)
    {
        ActiveGunAnimator.SetInteger("State", (int)state);
    }

    public void SetActiveGun(int index)
    {
        Guns[index].SetActive(true);

        for (var i = 0; i < Guns.Length; i++)
        {
            if (i != index)
                Guns[i].SetActive(false);
        }

        ActiveGun = Guns[index].GetComponent<Gun>();
        ActiveGunAnimator = Guns[index].GetComponent<Animator>();
    }

    public void Shoot()
    {
        ActiveGun.Shoot();
    }

    public void Reload()
    {
        ActiveGun.StartReload();
    }

    public void PlayStepSound()
    {
        if (StepAudioSource.isPlaying)
            StepAudioSource.Stop();

        var idx = Random.Range(1, StepAudioClips.Length);
        StepAudioSource.clip = StepAudioClips[idx];
        StepAudioSource.PlayOneShot(StepAudioSource.clip);

        StepAudioClips[idx] = StepAudioClips[0];
        StepAudioClips[0] = StepAudioSource.clip;

        float sphereCastDistance = 15f;
        Collider[] colliders = Physics.OverlapSphere(transform.position, sphereCastDistance, LayerMask.GetMask("Enemy"));
        foreach (Collider collider in colliders)
        {
            ZombieAnimationEvents enemy = collider.GetComponent<ZombieAnimationEvents>();
            if (enemy != null)
            {
                enemy.AttackPlayer(GetComponentInParent<FPSController>().transform);
            }
        }
    }
}
