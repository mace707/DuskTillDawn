using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{        
    private Transform PlayerTransform;
    private Transform EnemyTransform;

    public void Construct(Transform playerTransform, Transform enemyTransform)
    {
        PlayerTransform = playerTransform;
        EnemyTransform = enemyTransform;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerTransform && EnemyTransform)
        {
            var targetAngle = VectorMath.GetAngleOfDirectionVector(PlayerTransform, EnemyTransform);
            targetAngle += 90 + (PlayerTransform.eulerAngles.y - 180);
            Quaternion target = Quaternion.Euler(0, 0, targetAngle);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5.0f);
        }
    }
}
