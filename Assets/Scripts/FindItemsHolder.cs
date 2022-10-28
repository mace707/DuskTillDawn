using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindItemsHolder : MonoBehaviour
{
    private static Transform PlayerTransform;
    private static Transform RelationalMarkerHolder;
    
    // Start is called before the first frame update
    public static Transform GetPlayerTransform()
    {
        if (!PlayerTransform)
            PlayerTransform = FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().transform;

        return PlayerTransform;
    }

    public static Transform GetRotionalMarkerHolder()
    {
        if (!RelationalMarkerHolder)
            RelationalMarkerHolder = FindObjectOfType<RotationalMarkerHolder>().transform;

        return RelationalMarkerHolder;
    }
}
