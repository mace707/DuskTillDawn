using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
public class Zombie : MonoBehaviour
{
    private int Health = 100;

    private Animator ZombieAnimator;

    public Transform PlayerTransform;

    private FirstPersonDamageHandler FPSDamagerHandler;
    int MoveSpeed = 2;

    bool DamagingPlayer = false;

    public float YPosition;

    public List<AudioClip> dictNew;

    public GameObject Head;

    public GameObject BloodEffect;

    public AudioSource ZombieAudioSource;

    private bool ZombieIsDead = false;
    private bool ZombieSoundInvoked = false;

    public GameObject ZombieMarkerPrefab;

    private GameObject ZombieMarkerInstance;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = Statics.GetPlayerTransform();
        FPSDamagerHandler = PlayerTransform.GetComponent<FirstPersonDamageHandler>();
        ZombieAnimator = GetComponent<Animator>();
        ZombieAudioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(string obj)
    {
        if (obj == "Z_Head")
        {
            Health -= 100;
            var bloodEffect = Instantiate(BloodEffect, Head.transform.position, Quaternion.identity);            
            Destroy(Head, 0);
            Destroy(bloodEffect, 2f);
        }
        else if (obj == "Z_Body")
        {
            Health -= Random.Range(33,50);
        }
    }

    void PlayZombieSound()
    {
        AudioSource.PlayClipAtPoint(dictNew[Random.Range(0, dictNew.Count)], transform.position);
    }

    // Update is called once per frame
    void Update()
    {

        if (Statics.IsPaused())
        {
            if (ZombieAudioSource.isPlaying)
            {
                ZombieAudioSource.Stop();
            }
            return;
        }

        if (ZombieIsDead)
            return;

        if (Health <= 0 || transform.position.y < 0)
            KillZombie();

        transform.LookAt(PlayerTransform);

        var distanceFromPlayer = Vector3.Distance(transform.position, PlayerTransform.position);

        if (distanceFromPlayer <= 35 && !ZombieSoundInvoked)
        {
            ZombieSoundInvoked = true;
            InvokeRepeating("PlayZombieSound", Random.Range(0, 10), Random.Range(20, 30));
        }
        else if (distanceFromPlayer > 35 && ZombieSoundInvoked)
        {
            ZombieSoundInvoked = false;
            CancelInvoke("PlayZombieSound");
        }

        if (distanceFromPlayer <= 35 && !ZombieAudioSource.isPlaying)
            ZombieAudioSource.Play(0);
        else if (distanceFromPlayer > 35 && ZombieAudioSource.isPlaying)
            ZombieAudioSource.Stop();

        if (distanceFromPlayer > 2)
        {
            var transformStart = transform.position;
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transformStart.y, transform.position.z);

            if (DamagingPlayer)
            {
                CancelInvoke("DamagePlayer");
                DamagingPlayer = false;
                Destroy(ZombieMarkerInstance);
            }

            ZombieAnimator.ResetTrigger("Attack");
            ZombieAnimator.SetTrigger("Run");
        }
        else if (distanceFromPlayer <= 2 && Health > 0)
        {
            ZombieAnimator.SetTrigger("Attack");
            ZombieAnimator.ResetTrigger("Run");
            if (!DamagingPlayer)
            {
                if (MoveSpeed < 6)
                    MoveSpeed += 2;

                InvokeRepeating("DamagePlayer", 1.0f, 2.0f);
                DamagingPlayer = true;

                ZombieMarkerInstance = Instantiate(ZombieMarkerPrefab, Statics.GetRotionalMarkerHolder());
                var ZombieMarker = ZombieMarkerInstance.GetComponent<Marker>();
                ZombieMarker.Construct(PlayerTransform, transform);

                if (ZombieAudioSource.isPlaying)
                    ZombieAudioSource.Stop();
            }
        }        
    }

    private void KillZombie()
    {
        CancelInvoke("DamagePlayer");
        ZombieIsDead = true;

        if (ZombieAudioSource.isPlaying)
            ZombieAudioSource.Stop();

        Destroy(ZombieMarkerInstance);

        Destroy(ZombieAudioSource);

        ZombieAnimator.SetTrigger("ZombieDied");
        Invoke("DestroyThis", 2.0f);
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }

    private void DamagePlayer()
    {
        FPSDamagerHandler.TakeDamage(Statics.EnemyType.Zombie_Normal);
    }
}
