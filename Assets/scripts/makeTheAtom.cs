using TMPro;
using UnityEngine;

public class makeTheAtom : MonoBehaviour
{
    public electronconfiguration config;   // reference the script
    private string elementName;
    public float challengeTime = 15f;      // time for player to solve
    private float timer;

    private int targetProtons;
    private int targetNeutrons;
    private int targetElectrons;

    public TextMeshProUGUI makeThisElementTxt;
    public TextMeshProUGUI timerText;

    void Start()
    {
        //button
        StartChallenge();
    }

    void Update()
    {
        // Countdown timer
        timer -= Time.deltaTime;
        timerText.text = timer.ToString();

        if (timer<=0)
        {
            Debug.Log("⏳ Time's up! You failed.");
            endOfChallenge();
        }
    }


    //put it on an end button
    void endOfChallenge()
    {
        if (timer <= 0f)
        {
            Debug.Log("⏳ Time's up! You failed.");
            StartChallenge();   // start new challenge
        }



        // Check if the player created the correct atom
        if (PlayerMadeCorrectAtom())
        {
            Debug.Log("✔ Correct atom created!");
        }
        else if (!PlayerMadeCorrectAtom())
        {
            Debug.Log("You have done it incorrectly. Try again");
        }
    }

    void StartChallenge()
    {
        // Pick random element index
        int index = Random.Range(0, 21);
        GameObject elementObj = config.element[index];
        elementName = elementObj.name;  

        // Example: proton number is just index+1
        targetProtons = index + 1;
        targetNeutrons = targetProtons;
        targetElectrons = targetProtons; // assume neutral atom

        Debug.Log($"Make this element: "+elementName);
        makeThisElementTxt.text = $"Make this element: " + elementName;

        timer = challengeTime;
    }

    bool PlayerMadeCorrectAtom()
    {
        return
            config.GetProtons() == targetProtons &&
            config.GetNeutrons() == targetNeutrons &&
            config.GetElectrons() == targetElectrons;
    }
}