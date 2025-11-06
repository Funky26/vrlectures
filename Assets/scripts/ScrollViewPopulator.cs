using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

// 1. Define a custom class to hold the data for each button (label and scene name)
[System.Serializable]
public class ButtonData
{
    public string label = "New Menu Item";
    public string sceneName = "DefaultScene";
}

// Attach this script to the '02_ContentSection' GameObject

public class ScrollViewPopulator : MonoBehaviour
{
    // Assign these in the Inspector
    [Header("UI References")]
    public ScrollRect targetScrollRect;
    public RectTransform contentContainer; // Drag the 'Content' GameObject here
    public Button buttonPrefab; // Drag a pre-configured Button prefab here

    // 2. Use a list of the new structured data instead of just names
    [Header("Dynamic Menu Items")]
    [Tooltip("Define the label and the target scene for each button.")]
    public List<ButtonData> menuItems = new List<ButtonData>
    {
        // Default examples - You will configure these in the Inspector
        new ButtonData { label = "Load Level 1", sceneName = "Level_01" },
        new ButtonData { label = "Start Tutorial", sceneName = "Tutorial_Scene" },
        new ButtonData { label = "Open Settings Menu", sceneName = "Settings_VR" },
    };

    void Start()
    {
        // Safety check
        if (contentContainer == null || buttonPrefab == null)
        {
            Debug.LogError("Content Container or Button Prefab not assigned in the Inspector!");
            return;
        }

        PopulateScrollView();
    }

    private void PopulateScrollView()
    {
        // Clear any existing children before populating
        foreach (Transform child in contentContainer)
        {
            Destroy(child.gameObject);
        }

        // Iterate over the structured list of menu items
        foreach (ButtonData item in menuItems)
        {
            // 1. Instantiate the button from the prefab
            Button newButton = Instantiate(buttonPrefab, contentContainer);
            newButton.name = $"Button_{item.sceneName}";

            // 2. Set the text label
            Text buttonText = newButton.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = item.label;
            }

            // 3. Add a click event listener that passes the specific scene name.
            // Using a lambda (anonymous function) to capture the current item's scene name.
            string sceneToLoad = item.sceneName; // Capture variable for safety
            newButton.onClick.AddListener(() => LoadSceneByName(sceneToLoad));
        }
    }

    /// <summary>
    /// Public function called when a button in the scroll view is clicked.
    /// It loads the scene specified by the passed argument.
    /// </summary>
    /// <param name="sceneToLoad">The name of the scene file to load.</param>
    public void LoadSceneByName(string sceneToLoad)
    {
        if (string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.LogError("Scene name passed to LoadSceneByName is empty!");
            return;
        }

        // Final safety check: Scenes MUST be added to File -> Build Settings...
        if (SceneUtility.GetBuildIndexByScenePath(sceneToLoad) == -1)
        {
            Debug.LogError($"Scene '{sceneToLoad}' not found in Build Settings! Cannot load.");
            return;
        }

        Debug.Log($"Loading scene: {sceneToLoad}...");
        SceneManager.LoadScene(sceneToLoad);
    }
}
