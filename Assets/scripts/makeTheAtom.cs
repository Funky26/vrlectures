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
    public TextMeshProUGUI challengeFinishedText;

    public GameObject makeAtomUI;

    public AudioSource correctAudio;
    public AudioSource inccorrectAudio;

    private bool challengeRunning = false;

    void Start()
    {

        challengeFinishedText.text = string.Empty;
        makeAtomUI.SetActive(false);
    }

    void Update()
    {
        if (!challengeRunning)
            return;

        timer -= Time.deltaTime;
        timerText.text = timer.ToString("F1");

        if (timer <= 0f)
        {
            EndOfChallenge();
            return;
        }

        if (PlayerMadeCorrectAtom())
        {
            EndOfChallenge();
        }
    }
    void StartTime()
    {
        timer += Time.deltaTime;

        timerText.text = timer.ToString("F1"); // 1 decimal place

        if (timer >= challengeTime)
        {
            Debug.Log("⏳ Time's up! You failed.");
            EndOfChallenge();
        }
    }


    //put it on an end button
    void EndOfChallenge()
    {
        challengeRunning = false;

        if (PlayerMadeCorrectAtom())
        {
            challengeFinishedText.text = "✔ Correct atom created!";
            correctAudio.Play();
        }
        else
        {
            challengeFinishedText.text = "Try again";
            inccorrectAudio.Play();
        }

        makeAtomUI.SetActive(false);
    }

    public void StartChallenge()
    {
        challengeFinishedText.text = string.Empty;
        makeAtomUI.SetActive(true);

        int index = Random.Range(0, 17);
        GameObject elementObj = config.element[index];
        elementName = elementObj.name;

        targetProtons = index + 1;
        targetNeutrons = targetProtons;
        targetElectrons = targetProtons;

        makeThisElementTxt.text = elementName;

        timer = challengeTime;
        challengeRunning = true;
    }

    bool PlayerMadeCorrectAtom()
    {
        return
            config.GetProtons() == targetProtons &&
            config.GetNeutrons() == targetNeutrons &&
            config.GetElectrons() == targetElectrons;
    }
}