using UnityEngine;

public class ElectronOrbit : MonoBehaviour
{
    public Transform center;
    public float speed = 40f;

    void Update()
    {
        if (center == null) return;
        transform.RotateAround(center.position, Vector3.up, speed * Time.deltaTime);
    }
}
