using UnityEngine;
using UnityEngine.UI;

public class slideProgressTracking : MonoBehaviour
{
    public Slider progressSlider;

    void Start()
    {
        UpdateProgress();
    }

    public void UpdateProgress()
    {
        if (progressTracking.Instance == null) return;

        progressSlider.value =
            progressTracking.Instance.GetLecture1Progress();
    }
}
