using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject[] Guns;

    public int ActiveGun = 0;
    bool WeaponsChaning = false;

    private GunManager PlayerAnimationController;
    // Start is called before the first frame update
    void Start()
    {
        PlayerAnimationController = GetComponent<GunManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && !WeaponsChaning)
        {
            WeaponsChaning = true;
            Invoke("ChangeWeapon", 0.5f);
        }
    }

    void ChangeWeapon()
    {
        if (ActiveGun < 2)
            ActiveGun++;
        else
            ActiveGun = 0;

        for (var i = 0; i < 3; i++)
        {
            if (ActiveGun != i)
                Guns[i].SetActive(false);
        }

        Guns[ActiveGun].SetActive(true);
        PlayerAnimationController.Active = ActiveGun;
        WeaponsChaning = false;
    }
}
