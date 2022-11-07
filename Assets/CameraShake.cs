using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    #region Variables
    public bool CameraShakeActive = true;
    [Range(0, 1)] [SerializeField] float trauma;
    [SerializeField] float TraumaMultiplier = 5f;
    [SerializeField] float TraumaMagnitude = 0.8f;
    float timeCounter;
    #endregion

    #region Accessors
    public float Trauma
    {
        get
        {
            return trauma;
        }
        set
        {
            trauma = Mathf.Clamp01(value);
        }
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraShakeActive)
        {
            timeCounter += Time.deltaTime * trauma * TraumaMultiplier;
            Vector3 newPos = GetVector3() * TraumaMagnitude;
            transform.localPosition = newPos;
        }
    }

    float GetFloat(float seed)
    {
        return (Mathf.PerlinNoise(seed, timeCounter) - 0.5f) * 2;
    }

    Vector3 GetVector3()
    {
        return new Vector3(GetFloat(1), GetFloat(10), GetFloat(100));
    }
}
