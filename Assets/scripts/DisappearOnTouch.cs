using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction.HandGrab;   // <-- Needed for HandGrabInteractor
using Oculus.Interaction.Input;

public class DisappearOnTouch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object touching the cube is a hand (TouchHand or Interactor)
        if (other.GetComponentInParent<Hand>() != null ||
            other.GetComponentInParent<HandGrabInteractor>() != null)
        {
            // Make the cube disappear
            gameObject.SetActive(false);
        }
    }
}