using UnityEngine;

public class GunController : MonoBehaviour
{
    public float sensitivity = 3.0f;
    private float rotY = 0.0f;

    void Update()
    {
        // Rotate the gun based on mouse movement on the vertical axis
        rotY -= Input.GetAxis("Mouse Y") * sensitivity;

        // Clamp the vertical rotation to a certain range
        rotY = Mathf.Clamp(rotY, -89.0f, 89.0f);

        transform.localEulerAngles = new Vector3(rotY, transform.localEulerAngles.y, 0);
    }
}