using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target; // The target the camera should follow (e.g., the player)
    public float smoothSpeed = 0.125f; // The smoothing speed
    public Vector2 offset; // Offset from the target in the XY plane
    private Vector3 velocity = Vector3.zero;

    private float initialZ; // Initial Z position of the camera to maintain a constant depth

    void Start()
    {


        // Store the initial Z-axis position of the camera to keep it constant
        initialZ = transform.position.z;
    }

    void Update()
    {



        // If there's no target, don't try to follow anything
        if (target == null) return;

        // Desired position has the same Z value as the camera's original Z value to maintain depth
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, initialZ);

        // Update the camera's position
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }
}
