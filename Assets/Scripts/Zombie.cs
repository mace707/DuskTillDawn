using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private int Health = 100;

    private Animator ZombieAnimator;

    public Transform Player;
    int MoveSpeed = 2;
    float MinDist = 12f;

    private PlayerMovement PlayerScript;

    bool DamagingPlayer = false;

    public float YPosition;

    public List<AudioClip> dictNew;

    public GameObject Head;

    public GameObject BloodEffect;

    void Register()
    {
        if (!DamageIndicatorSystem.CheckIfObjectInSight(this.transform))
        {
            DamageIndicatorSystem.CreateIndicator(this.transform);
        }
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

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerMovement>().transform;
        ZombieAnimator = GetComponent<Animator>();
        PlayerScript = Player.GetComponent<PlayerMovement>();

        InvokeRepeating("PlayZombieSound", Random.Range(0, 6), Random.Range(5, 11));
    }

    void PlayZombieSound()
    {
        AudioSource.PlayClipAtPoint(dictNew[Random.Range(0, dictNew.Count)], transform.position);        
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
            KillZombie();

        if (transform.position.y < -20)
            KillZombie();

        transform.LookAt(Player);
        
        if (Vector3.Distance(transform.position, Player.position) >= 2.5)
        {
            var transformStart = transform.position;
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transformStart.y, transform.position.z);
        
        
            if (Vector3.Distance(transform.position, Player.position) <= 4 && Health > 0)
            {
                ZombieAnimator.SetTrigger("Attack");
                ZombieAnimator.ResetTrigger("Run");
                if (!DamagingPlayer)
                {
                    InvokeRepeating("DamagePlayer", 4.0f, 4.0f);
                    DamagingPlayer = true;
                }

            }
            else
            {
                CancelInvoke("DamagePlayer");
                DamagingPlayer = false;
                ZombieAnimator.ResetTrigger("Attack");
                ZombieAnimator.SetTrigger("Run");
            }
        
        }                
    }

    private void KillZombie()
    {
        ZombieAnimator.SetTrigger("ZombieDied");
        Invoke("DestroyThis", 5.0f);
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }

    private void DamagePlayer()
    {
        Invoke("Register", 4.0f);
        PlayerScript.TakeDamage();
    }
}
