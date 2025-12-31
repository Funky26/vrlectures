using UnityEngine;

public class buttonProgressTr : MonoBehaviour
{
    public int checkpointIndex;

    public void Complete()
    {
        progressTracking.Instance.CompleteCheckpoint(checkpointIndex);
    }
}
