using UnityEngine;
using System.Collections; // Required for coroutines

/// <summary>
/// Extends OVRGrabbable to add functionality for snapping/docking to a designated
/// snap zone when the object is released near it.
/// </summary>
// Require a Rigidbody and a Collider for OVRGrabbable functionality
[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class GrabbableAndDockable : OVRGrabbable
{
    [Header("Docking Settings")]
    [Tooltip("The Transform of the target location this object will snap to.")]
    public Transform targetSnapZone;

    [Tooltip("The maximum distance (in meters) from the targetSnapZone at which the object will snap.")]
    public float snapDistance = 0.25f;

    [Tooltip("Controls how quickly the object animates into the final snap position.")]
    public float snapSpeed = 10f;

    private bool isDocked = false;

    // We override the GrabEnd method from the base OVRGrabbable class.
    // This method is automatically called when the OVRGrabber releases this object.
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        // 1. Call the base class method to perform standard release physics
        base.GrabEnd(linearVelocity, angularVelocity);

        // 2. Perform the docking check immediately after release
        CheckForSnapZone();
    }

    /// <summary>
    /// Checks if the object is close enough to the targetSnapZone to dock.
    /// </summary>
    private void CheckForSnapZone()
    {
        // Only check if we have a snap zone assigned
        if (targetSnapZone == null)
        {
            return;
        }

        float distanceToTarget = Vector3.Distance(transform.position, targetSnapZone.position);

        if (distanceToTarget <= snapDistance)
        {
            // Object is close enough: initiate docking animation
            StartCoroutine(SnapToZone(targetSnapZone.position, targetSnapZone.rotation));
        }
        else
        {
            // If the object was previously docked but was moved and released too far,
            // ensure it can now be moved freely by physics (if not kinematic).
            isDocked = false;
        }
    }

    /// <summary>
    /// Coroutine to smoothly move the object into the docking position.
    /// </summary>
    private IEnumerator SnapToZone(Vector3 targetPosition, Quaternion targetRotation)
    {
        isDocked = true;

        // Ensure the Rigidbody is kinematic while docked/snapping to fix its position
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        Debug.Log(gameObject.name + " is snapping to " + targetSnapZone.name);

        while (isDocked && Vector3.Distance(transform.position, targetPosition) > 0.001f)
        {
            // Smoothly move position and rotation towards the target
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * snapSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * snapSpeed);
            yield return null;
        }

        // Final snap to ensure perfect alignment
        if (isDocked)
        {
            transform.position = targetPosition;
            transform.rotation = targetRotation;
        }
    }

    // We override GrabBegin to ensure the object can be pulled out of the snap zone.
    // The previous name 'StartGrab' was incorrect for this version of the OVR SDK.
    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        // If the object was docked, unset the docked state when grabbing starts.
        if (isDocked)
        {
            isDocked = false;

            // Re-enable physics simulation if needed (though OVRGrabbable usually handles this)
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Setting isKinematic to false ensures the object can be moved freely while grabbed.
                // OVRGrabbable will typically re-set this during its internal logic.
                rb.isKinematic = false;
            }
        }

        // Call the base class method to continue with standard grabbing procedure
        base.GrabBegin(hand, grabPoint);
    }
}