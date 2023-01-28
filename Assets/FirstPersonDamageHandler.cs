using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FirstPersonDamageHandler : MonoBehaviour
{
    [SerializeField] private int Health = 100;
    [SerializeField] private float MaxStamina = 25;
    [SerializeField] private float CurrentStamina = 25f;
    [SerializeField] private float StaminaDecay = 2f;
    [SerializeField] private TMP_Text TXTHealth;
    [SerializeField] private TMP_Text TXTStamina;

    bool IsRunning = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HealthPack")
        {
            Health = 100;
            TXTHealth.SetText("HEALTH: " + Health.ToString());
        }
    }

    private void Start()
    {
        TXTHealth.SetText("HEALTH: " + Health.ToString());
        TXTStamina.SetText("STAM: " + CurrentStamina.ToString("F0"));
    }

    private void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CurrentStamina >= 10)
                CurrentStamina -= 10;
        }
        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            if (CurrentStamina > 0)
            {
                IsRunning = true;
                CurrentStamina -= StaminaDecay * Time.deltaTime;
            }
            
            TXTStamina.SetText("STAM: " + CurrentStamina.ToString("F0"));
        }
        else
        {
            if (IsRunning)
                Invoke("RebuildStamina", 2.0f);
        }

        if (!IsRunning && CurrentStamina < MaxStamina)
        {
            if (CurrentStamina/MaxStamina < 0.5)
                CurrentStamina += Time.deltaTime;
            else
                CurrentStamina += 3 * Time.deltaTime;

            TXTStamina.SetText("STAM: " + CurrentStamina.ToString("F0"));
        }
    }

    private void RebuildStamina()
    {
        IsRunning = false;
    }

    public float GetStamina()
    {
        return CurrentStamina;
    }

    public void TakeDamage(int damage = 0)
    {
        Health -= damage;

        if (Health <= 0)
            SceneManager.LoadScene(2);

        TXTHealth.SetText("HEALTH: " + Health.ToString());
    }

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.collider.tag == "Weapon")
        {
            var weapon = collision.collider.gameObject.GetComponent<WeaponScript>();            
            TakeDamage(weapon.GetDamage());
            weapon.Disable();
        }
    }
}
