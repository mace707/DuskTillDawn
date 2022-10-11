using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicatorSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DamageIndicator IndicatorPrefab;
    [SerializeField] private RectTransform Holder;
    [SerializeField] private new Camera Cam;
    [SerializeField] private Transform Player;

    private Dictionary<Transform, DamageIndicator> Indicators = new Dictionary<Transform, DamageIndicator>();

    #region Delegates
    public static System.Action<Transform> CreateIndicator = delegate { };
    public static System.Func<Transform, bool> CheckIfObjectInSight = null;
    #endregion

    private void OnEnable()
    {
        CreateIndicator += Create;
        CheckIfObjectInSight += InSight;
    }

    private void OnDisable()
    {
        CreateIndicator -= Create;
        CheckIfObjectInSight -= InSight;
    }


    void Create(Transform target)
    {
        if (Indicators.ContainsKey(target))
        {
            Indicators[target].Restart();
            return;
        }
        DamageIndicator newIndicator = Instantiate(IndicatorPrefab, Holder);
        newIndicator.Register(target, Player, new System.Action(() => { Indicators.Remove(target); }));

        Indicators.Add(target, newIndicator);
    }

    bool InSight(Transform t)
    {
        Vector3 screenPoint = Cam.WorldToViewportPoint(t.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }
}
