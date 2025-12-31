using UnityEngine;

public class progressTracking : MonoBehaviour
{
    public static progressTracking Instance;

    // Example: one lecture with 4 steps
    public bool[] lecture1Steps = new bool[4];

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float GetLecture1Progress()
    {
        int completed = 0;

        foreach (bool step in lecture1Steps)
        {
            if (step) completed++;
        }

        return completed / 4f; // returns 0–1
    }

    public void firstCheckpoint()
    {
        progressTracking.Instance.lecture1Steps[0] = true;
    }
    public void secondCheckpoint()
    {
        progressTracking.Instance.lecture1Steps[1] = true;
    }
    public void thirdCheckpoint()
    {
        progressTracking.Instance.lecture1Steps[2] = true;
    }
    public void fourthCheckpoint()
    {
        progressTracking.Instance.lecture1Steps[3] = true;
    }
    public void CompleteCheckpoint(int index)
    {
        if (index < 0 || index >= lecture1Steps.Length) return;
        lecture1Steps[index] = true;
    }
}

