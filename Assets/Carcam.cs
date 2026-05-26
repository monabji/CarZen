using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform car;

    public Vector3 offset = new Vector3(0, 1, -1);

    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        Vector3 desiredPosition = car.position + car.TransformDirection(offset);

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.LookAt(car);
    }
}