using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statics : MonoBehaviour
{
    private static bool Paused = false;
    private static Transform PlayerTransform;
    private static Transform RelationalMarkerHolder;
    
    public enum EnemyType
    {
        Zombie_Normal,
    }

    public void Start()
    {

    }

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

    public static bool IsPaused()
    {
        return Paused;
    }

    public static void SetPaused(bool paused)
    {
        Paused = paused;
        Time.timeScale = paused ? 0 : 1;
    }
}
