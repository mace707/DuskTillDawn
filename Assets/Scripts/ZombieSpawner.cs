using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public float XMax, XMin, ZMax, ZMin;

    public GameObject ZombiePrefab;

    private float CoolDownTimer = 20;
    private float CoolDown = 20;  

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
        for (var i = 0; i < 10; i++)
        {
            Vector3 spawnBox0 = new Vector3(-50, 20, Random.Range(-50, 50));
            Vector3 spawnBox1 = new Vector3(50, 20, Random.Range(-50, -50));
            Vector3 spawnBox2 = new Vector3(Random.Range(-50, 50), 20, 50);
            Vector3 spawnBox3 = new Vector3(Random.Range(-50, 50), 20, -50);

            int boxToUse = Random.Range(0, 4);            

            if (boxToUse == 0)
                Instantiate(ZombiePrefab, spawnBox0, transform.rotation);
            else if (boxToUse == 1)
                Instantiate(ZombiePrefab, spawnBox1, transform.rotation);
            else if (boxToUse == 2)
                Instantiate(ZombiePrefab, spawnBox2, transform.rotation);
            else
                Instantiate(ZombiePrefab, spawnBox3, transform.rotation);

        }        

        if (CoolDown >= 5)        
            CoolDown -= 1;
        
        CoolDownTimer = CoolDown;
    }
}
