using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{        
    [SerializeField]
    public GameObject MarkerObj;

    private Transform MarkerHandlerTransform;
    private Transform PlayerTransfrom;

    // Start is called before the first frame update
    void Start()
    {        
        PlayerTransfrom = FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().transform;
        MarkerHandlerTransform = FindObjectOfType<RotationalMarkerHolder>().transform;
        MarkerObj = Instantiate(MarkerObj, MarkerHandlerTransform);
    }

    // Update is called once per frame
    void Update()
    {
        var targetAngle = VectorMath.GetAngleOfDirectionVector(PlayerTransfrom, transform);
        targetAngle += 90 + (PlayerTransfrom.eulerAngles.y - 180);        
        Quaternion target = Quaternion.Euler(0, 0, targetAngle);
        MarkerObj.transform.rotation = Quaternion.Slerp(MarkerObj.transform.rotation, target, Time.deltaTime * 5.0f);
    }
}
