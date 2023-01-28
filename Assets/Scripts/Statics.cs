using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statics : MonoBehaviour
{
    private static bool Paused = false;
    private static Transform PlayerTransform;
    private static Transform RelationalMarkerHolder;

    static private int RandomValue = 10;
    private static int StartRandomValue = 10;

    public enum EnemyType
    {
        Zombie_Normal,
    }

    public void Start()
    {        
    }

    public static int GetRandomValueRange()
    {
        return RandomValue + 1;
    }

    public static bool CheckRandomValue(int value)
    {
        var equal = value == RandomValue;
        if (!equal && RandomValue > 1)
            RandomValue--;
        else if (equal)
            RandomValue = StartRandomValue;
        
        return equal;
    }

    // Start is called before the first frame update
    public static Transform GetPlayerTransform()
    {
        if (!PlayerTransform)
            PlayerTransform = FindObjectOfType<FPSController>().transform;

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
