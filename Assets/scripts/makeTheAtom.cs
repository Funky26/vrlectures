using UnityEngine;

public class makeTheAtom : MonoBehaviour
{
    public electronconfiguration config;   // reference the script
    public float challengeTime = 10f;      // time for player to solve
    private float timer;

    private int targetProtons;
    private int targetNeutrons;
    private int targetElectrons;

    void Start()
    {
        // Start the first challenge
        StartChallenge();
    }

    void Update()
    {
        // Countdown timer
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Debug.Log("⏳ Time's up! You failed.");
            StartChallenge();   // start new challenge
        }

        // Check if the player created the correct atom
        if (PlayerMadeCorrectAtom())
        {
            Debug.Log("✔ Correct atom created!");
            StartChallenge();
        }
    }

    void StartChallenge()
    {
        // Pick random element index
        int index = Random.Range(0, config.element.Count);

        // Example: proton number is just index+1
        targetProtons = index + 1;
        targetNeutrons = targetProtons + Random.Range(-1, 3);
        targetElectrons = targetProtons; // assume neutral atom

        Debug.Log($"Make the element with:");
        Debug.Log($"Protons: {targetProtons}");
        Debug.Log($"Neutrons: {targetNeutrons}");
        Debug.Log($"Electrons: {targetElectrons}");

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
