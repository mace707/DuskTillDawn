using UnityEngine;
using UnityEngine.Audio;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab of the bullet
    public Transform firePoint; // The point from which the bullets are spawned
    public float bulletForce = 20.0f; // The force with which the bullets are shot
    public float firingRate = 0.5f; // The firing rate of the gun, in shots per second
    public float spread = 1.0f; // The spread of the bullets, in degrees
    public int magazineSize = 10; // The number of bullets in the magazine
    public int maxAmmo = 30; // The maximum amount of ammo the player can carry
    public float reloadTime = 1.0f; // The time it takes to reload the gun, in seconds

    private float timer; // Timer for the firing rate
    private int ammo; // The current amount of ammo the player has
    private bool isReloading; // Whether the gun is currently being reloaded

    public GunManager PlayerAnimationController;
    private AudioSource GunShotAudioSource;

    public RaycastHit RayHit;
    public Transform AttackPoint;
    public GameObject MuzzleFlash;
    public GameObject BloodEffect;
    public Camera Cam;    

    void Start()
    {
        // Initialize the timer and ammo
        timer = 0.0f;
        ammo = magazineSize;
        GunShotAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    public void StartReload()
    {
        if (ammo < maxAmmo)
        {
            // Set the reloading flag
            isReloading = true;

            // Reload the gun after the reload time has elapsed
            Invoke("Reload", reloadTime);

            PlayerAnimationController.SetState(GunManager.GUNSTATES.GUNSTATE_RELOAD);
        }
    }

    void Reload()
    {
        // Check if there is enough ammo to fill the magazine
        if (maxAmmo >= magazineSize)
        {
            // Fill the magazine and decrement the ammo count
            ammo = magazineSize;
            maxAmmo -= (magazineSize - ammo);
        }
        else
        {
            // Fill the magazine with the remaining ammo
            ammo = maxAmmo;
            maxAmmo = 0;
        }

        // Clear the reloading flag
        isReloading = false;
    }

    public void Shoot()
    {
        if (timer >= firingRate && !isReloading)
        {
            // Reset the timer
            timer = 0.0f;

            // Check if there is ammo in the magazine
            if (ammo > 0)
            {
                if (GunShotAudioSource.isPlaying)
                    GunShotAudioSource.Stop();

                GunShotAudioSource.PlayOneShot(GunShotAudioSource.clip);

                if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out RayHit, 5000))
                {
                    var collisionTag = RayHit.collider.tag;
                    if (collisionTag == "Z_Head" || collisionTag == "Z_Body")
                    {
                        //RayHit.collider.GetComponentInParent<Zombie>().TakeDamage(collisionTag, GunType);
                        RayHit.collider.GetComponentInParent<ZombieAnimationEvents>().Kill();
                        var bloodEffect = Instantiate(BloodEffect, RayHit.point, Quaternion.identity);
                        Destroy(bloodEffect, 0.5f);
                    }
                }

                var muzzleFlash = Instantiate(MuzzleFlash, AttackPoint.transform.position, Quaternion.identity);
                Destroy(muzzleFlash, 0.5f);

                // Decrement the ammo count
                ammo--;

                PlayerAnimationController.SetState(GunManager.GUNSTATES.GUNSTATE_SHOOT);
            }
            else
            {
                StartReload();
            }
        }
    }
}