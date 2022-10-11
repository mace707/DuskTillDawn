using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    public int Damage;
    public float TimeBetweenShooting;
    public float Spread;
    public float Range;
    public float ReloadTime;
    public float TimeBetweenShots;
    public int MagazineSize;
    public float BulletsPerTap;
    public bool AllowButtonHold;
    public int BulletsLeft;
    public int BulletsShot;

    bool Shooting;
    bool ReadyToShoot;
    bool Reloading;

    public Camera FPSCamera;
    public Transform AttackPoint;
    public RaycastHit RayHit;
    public LayerMask WhatIsEnemy;

    public GameObject MuzzleFlash;
    public GameObject BulletHoleGraphic;
    public GameObject BloodEffect;

    public AudioClip GunShotSound;
    public AudioClip ImpactSound;

    public TMPro.TMP_Text TxtBulletsLeft;

    private Animator GunAnimator;

    private void Awake()
    {
        BulletsLeft = MagazineSize;
        ReadyToShoot = true;
        UpdateBulletText();


    }

    private void UpdateBulletText()
    {
        TxtBulletsLeft.text = "Bullets: " + BulletsLeft.ToString() + "/" + MagazineSize.ToString();
    }

    private void MyInput()
    {
        Shooting = AllowButtonHold ? Input.GetKey(KeyCode.Mouse0) : Input.GetKeyDown(KeyCode.Mouse0);

        if ((Input.GetKeyDown(KeyCode.R) || BulletsLeft == 0) && BulletsLeft < MagazineSize && !Reloading)
            Reload();

        if (ReadyToShoot && Shooting && !Reloading && BulletsLeft > 0)
        {
            AudioSource.PlayClipAtPoint(GunShotSound, AttackPoint.transform.position);
            Shoot();
        }
    }

    private void Shoot()
    {
        GunAnimator.SetTrigger("Shoot");
        ReadyToShoot = false;
        if (Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out RayHit, Range))
        {
            var collisionTag = RayHit.collider.tag;
            if (collisionTag == "Z_Head" || collisionTag == "Z_Body")
            {
                RayHit.collider.GetComponentInParent<Zombie>().TakeDamage(collisionTag);
                var bloodEffect = Instantiate(BloodEffect, RayHit.point, Quaternion.identity);
                Destroy(bloodEffect, 0.5f);
            }
        }
        
        var muzzleFlash = Instantiate(MuzzleFlash, AttackPoint.transform.position, Quaternion.identity);        

        BulletsLeft--;
        Invoke("ResetShot", TimeBetweenShooting);
        Destroy(muzzleFlash, 0.5f);
        UpdateBulletText();        
    }

    private void ResetShot()
    {
        ReadyToShoot = true;
    }

    private void Reload()
    {
        Reloading = true;
        Invoke("ReloadFinished", ReloadTime);
    }

    private void ReloadFinished()
    {
        BulletsLeft = MagazineSize;
        Reloading = false;
        UpdateBulletText();
    }
    // Start is called before the first frame update
    void Start()
    {
        GunAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
    }
}
