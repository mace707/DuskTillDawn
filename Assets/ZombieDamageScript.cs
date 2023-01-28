using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDamageScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DeleteZombie()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.tag == "Bullet")
        {
            GetComponent<Animator>().SetTrigger("Death");
            Invoke("DeleteZombie", 3.00f);
        }
    }
}
