using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 20f;
    public Vector2 offset;

    public void FixedUpdate()
    {
        Vector2 desiredPosition = new Vector2(target.position.x, target.position.y) + offset;
        Vector2 smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, smoothSpeed *Time.deltaTime);
        transform.position = smoothedPosition;
        transform.LookAt(target);
    }

}