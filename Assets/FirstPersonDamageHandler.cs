using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FirstPersonDamageHandler : MonoBehaviour
{
    [SerializeField] private int Health = 100;
    [SerializeField] private TMP_Text TXTHealth;

    private void Start()
    {
        TXTHealth.SetText("HEALTH: " + Health.ToString());
    }

    public void TakeDamage(Statics.EnemyType enemyType)
    {
        switch (enemyType)
        {
            case Statics.EnemyType.Zombie_Normal:
                Health -= 10;
                break;
            default:
                break;
        }

        if (Health <= 0)
            SceneManager.LoadScene(2);

        TXTHealth.SetText("HEALTH: " + Health.ToString());
    }
}
