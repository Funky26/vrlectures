using UnityEngine;
using Oculus.Interaction; // For IInteractable
using Oculus.Interaction.HandGrab; // For HandGrabInteractor

/// <summary>
/// Enables two-handed scaling for an object using the Oculus Interaction SDK's HandGrab system.
/// Scaling is activated when the assigned Left and Right Hand Interactors are both selecting this object.
/// </summary>
public class TwoHandScale : MonoBehaviour
{
    [Tooltip("The HandGrabInteractor for the Left Hand, typically found on the Left Hand tracking object (must be assigned in Inspector).")]
    public HandGrabInteractor leftHand;

    [Tooltip("The HandGrabInteractor for the Right Hand, typically found on the Right Hand tracking object (must be assigned in Inspector).")]
    public HandGrabInteractor rightHand;

    // This object's Interactable component (e.g., HandGrabInteractable). We use the interface IInteractable.
    private IInteractable targetInteractable;

    private float initialDistance = 0f;
    private Vector3 initialScale;

    private void Awake()
    {
        // 1. Get the IInteractable component on this same GameObject (the cube).
        targetInteractable = GetComponent<IInteractable>();

        if (targetInteractable == null)
        {
            Debug.LogError(gameObject.name + ": TwoHandScale requires an IInteractable component (like HandGrabInteractable) on the same GameObject.");
            enabled = false;
        }
    }

    private void Update()
    {
        // Safety check
        if (leftHand == null || rightHand == null || targetInteractable == null)
        {
            return;
        }

        // --- 1. Check if both hands are actively grabbing THIS specific object ---

        // Check if the left hand's currently selected object is THIS object.
        bool leftGrabbingThis = leftHand.SelectedInteractable == targetInteractable;
        // Check if the right hand's currently selected object is THIS object.
        bool rightGrabbingThis = rightHand.SelectedInteractable == targetInteractable;

        // --- 2. Scaling Logic ---

        if (leftGrabbingThis && rightGrabbingThis)
        {
            // Both hands are grabbing this object simultaneously.

            // Get the precise grab positions from the interactors' transforms.
            // Replaced .GrabPointer.Pose.Position with .transform.position for broader SDK compatibility.
            Vector3 leftPos = leftHand.transform.position;
            Vector3 rightPos = rightHand.transform.position;

            if (initialDistance == 0f)
            {
                // INITIAL GRAB: Capture the starting distance and scale state
                initialDistance = Vector3.Distance(leftPos, rightPos);
                initialScale = transform.localScale;

                // Small threshold check to prevent instant scaling if hands start on top of each other.
                if (initialDistance < 0.01f)
                {
                    Debug.LogWarning("Scaling denied: Hands started too close together. Resetting.");
                    initialDistance = 0f;
                    return;
                }
            }
            else
            {
                // SCALING IN PROGRESS: Calculate and apply the new scale
                float currentDistance = Vector3.Distance(leftPos, rightPos);

                if (initialDistance > 0)
                {
                    float scaleFactor = currentDistance / initialDistance;
                    transform.localScale = initialScale * scaleFactor;
                }
            }
        }
        else
        {
            // One or both hands released, or one hand is grabbing a different object.
            // Reset the initial state for the next two-hand grab.
            initialDistance = 0f;
        }
    }
}