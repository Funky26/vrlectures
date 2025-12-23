using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneButton : MonoBehaviour
{
    [Header("Scene Settings")]
    [Tooltip("Name of the scene to load")]
    public string sceneToLoad;

    public Slider buttonSlider;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(LoadScene);
        }
        else
        {
            Debug.LogError("LoadSceneButton script requires a UI Button component.");
        }
    }

    private void Update()
    {
        buttonSlider.value = 0f;
    }

    public void sliderValue()
    {

    }


    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Scene name is empty. Please set the sceneToLoad field.");
        }
    }
}
