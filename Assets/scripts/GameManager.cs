using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform firstStageSpawn;
    public Transform secondStageSpawn;
    public bool readyToSpawn = false;

    public Transform playerTransfrom;

    public GameObject apple;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (apple == null && readyToSpawn == true)
        {
            playerTransfrom = secondStageSpawn.transform;
            readyToSpawn = false;
        }
    }
}
