using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public static float XMax, XMin, ZMax, ZMin;

    public GameObject ZombiePrefab;

    private float CoolDownTimer = 40;
    private float CoolDown = 40;

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
        CoolDownTimer -= Time.deltaTime;

        if (CoolDownTimer <= 0)
            SpawnZombie();
    }

    void SpawnZombie()
    {
        if (GameObject.FindObjectsOfType<Zombie>().Length < 20)
        {
            foreach (var spawnBox in SpawnBoxes)
                Instantiate(ZombiePrefab, spawnBox, transform.rotation);

            if (CoolDown >= 5)
                CoolDown -= 1;            
        }

        CoolDownTimer = CoolDown;
    }
}
