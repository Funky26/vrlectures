using UnityEngine;
using Oculus.Interaction;

public class SnapZone : MonoBehaviour
{
    [Tooltip("How close the grabbable must be to snap")]
    public float snapDistance = 0.15f;

    [Tooltip("The transform where the object should snap (use this GameObject if null)")]
    public Transform snapPoint;

    [Tooltip("Optional tag filter for allowed objects")]
    public string grabbableTag = "Grabbable";

    private void Start()
    {
        if (snapPoint == null)
            snapPoint = transform;
    }

    private void OnTriggerStay(Collider other)
    {
        var grabbable = other.GetComponent<Grabbable>();
        if (grabbable == null) return;

        // In Oculus.Interaction.Grabbable, check SelectingPointsCount
        if (grabbable.SelectingPointsCount > 0) return;

        float dist = Vector3.Distance(other.transform.position, snapPoint.position);
        if (dist <= snapDistance)
        {
            // Snap to point
            other.transform.position = snapPoint.position;
            other.transform.rotation = snapPoint.rotation;

            // Disable physics for stability
            var rb = other.attachedRigidbody;
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
            }
        }
    }
}
