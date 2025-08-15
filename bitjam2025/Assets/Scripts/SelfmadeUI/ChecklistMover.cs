using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChecklistMover : MonoBehaviour
{
    public float followStrength = 0.05f; // How strongly it follows the mouse
    public float returnSpeed = 5f;       // How quickly it returns to origin
    public float maxOffset = 0.5f;       // Max distance from origin

    [SerializeField] private Transform reference;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = reference.position;
    }

    void Update()
    {
        // Get mouse position in world space
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = originalPosition.z; // Keep original Z

        // Calculate offset from original position
        Vector3 offset = (mouseWorldPos - originalPosition) * followStrength;

        // Clamp offset to max distance
        offset = Vector3.ClampMagnitude(offset, maxOffset);

        // Target position with offset
        Vector3 targetPosition = originalPosition + offset;

        // Smoothly move toward target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * returnSpeed);
    }
}
