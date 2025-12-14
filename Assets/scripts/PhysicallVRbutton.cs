using UnityEngine;
using UnityEngine.Events;

public class ButtonVR : MonoBehaviour
{
    public GameObject button;             // The moving part
    public UnityEvent onPress;
    public UnityEvent onRelease;

    private Collider presser;
    private AudioSource sound;
    private bool isPressed;

    // Button positions
    private Vector3 upPos = new Vector3(0, 0.015f, 0);
    private Vector3 downPos = new Vector3(0, 0.003f, 0);

    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPressed) return;

        presser = other;
        button.transform.localPosition = downPos;
        onPress.Invoke();
        if (sound) sound.Play();
        isPressed = true;
    }

    private void OnTriggerExit(Collider other)
    {
        // Ignore stray exits
        if (other != presser) return;

        ReleaseButton();
    }

    private void OnTriggerStay(Collider other)
    {
        // If something else enters or hand stays inside — fine.
        // BUT if the presser is null or disabled → release
        if (isPressed && presser != null)
        {
            // If the presser collider is no longer really touching
            if (!other.bounds.Intersects(GetComponent<Collider>().bounds))
            {
                ReleaseButton();
            }
        }
    }

    private void ReleaseButton()
    {
        button.transform.localPosition = upPos;
        onRelease.Invoke();
        isPressed = false;
        presser = null;
    }
}
