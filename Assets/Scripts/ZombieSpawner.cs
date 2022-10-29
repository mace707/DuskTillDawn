using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public static float XMax, XMin, ZMax, ZMin;

    public GameObject ZombiePrefab;

    private float CoolDownTimer = 20;
    private float CoolDown = 20;

    private List<Vector3> SpawnBoxes = new List<Vector3>
    {
        new Vector3(-45, 50, -45),
        new Vector3(-45, 50, 45),
        new Vector3(45, 50, 45),
        new Vector3(45, 50, -45)
    };

    private void Start()
    {
        SpawnZombie();
    }

    // Update is called once per frame
    void Update()
    {
        if (Statics.IsPaused())
            return;

        CoolDownTimer -= Time.deltaTime;

        if (CoolDownTimer <= 0)
            SpawnZombie();
    }

    void SpawnZombie()
    {
        if (GameObject.FindObjectsOfType<Zombie>().Length < 25)
        {
            foreach (var spawnBox in SpawnBoxes)
                Instantiate(ZombiePrefab, spawnBox, transform.rotation);

            if (CoolDown >= 2)
                CoolDown -= 1;            
        }

        CoolDownTimer = CoolDown;
    }
}
