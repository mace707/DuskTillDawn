using UnityEngine;

public class VectorMath : MonoBehaviour
{
    public static float GetAngleOfDirectionVector(Transform playerTransform, Transform enemyTransform)
    {
        var playerPositionXZ = new Vector2(playerTransform.position.x, playerTransform.position.z);
        var enemyPositionXZ = new Vector2(enemyTransform.position.x, enemyTransform.position.z);
        var directionVector = playerPositionXZ - enemyPositionXZ;

        var arcTan = directionVector.y / directionVector.x;

        var angle = Mathf.Atan(arcTan) * 180 / Mathf.PI;

        if (playerTransform.position.x > enemyTransform.position.x)
            angle += 180;

        return angle;
    }
}
