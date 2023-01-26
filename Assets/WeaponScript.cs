using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private int Damage = 0;

    private void Start()
    {
        Disable();
    }

    public void Enable()
    {
        Damage = 15;
    }

    public void Disable()
    {
        Damage = 0;
    }

    public int GetDamage()
    {
        return Damage;
    }
}
