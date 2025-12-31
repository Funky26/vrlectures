using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class electronconfiguration : MonoBehaviour
{
    private int electrons = 0;
    private int protons = 0;
    private int neutrons = 0;

    private bool stability = true;

    [Header("UI")]
    public TextMeshProUGUI stabilityTxt;
    public TextMeshProUGUI neutronsTxt;
    public TextMeshProUGUI protonsTxt;
    public TextMeshProUGUI electronsTxt;

    [Header("Highlighter")]
    public GameObject highlighterObj;

    [Header("Elements List")]
    public List<GameObject> element;

    // --- PUBLIC GETTERS ---
    public int GetProtons() { return protons; }
    public int GetNeutrons() { return neutrons; }
    public int GetElectrons() { return electrons; }

    // --- PRIVATE INPUT FLAGS ---
    private bool addElectron = false;
    private bool takeElectron = false;
    private bool addProton = false;
    private bool takeProton = false;
    private bool addNeutron = false;
    private bool takeNeutron = false;

    void Update()
    {
        // Update particle amounts
        protons = particleCount(addProton, takeProton, protons, protonsTxt);
        neutrons = particleCount(addNeutron, takeNeutron, neutrons, neutronsTxt);
        electrons = particleCount(addElectron, takeElectron, electrons, electronsTxt);

        // Reset flags each frame
        addProton = takeProton = false;
        addNeutron = takeNeutron = false;
        addElectron = takeElectron = false;

        // Stability logic
        stability = IsStable(protons, neutrons);
        stabilityTxt.text = stability ? "Stable" : "Unstable";

        // Move highlighter
        highlighter(highlighterObj);

        Debug.Log("Neutrons: " + neutrons + "   Protons: "+protons+"    Electrons: "+electrons );
    }

    // Increase/Decrease particle counts
    public int particleCount(bool give, bool take, int particleCount, TextMeshProUGUI particleText)
    {
        if (give) particleCount++;
        if (take) particleCount--;

        if (particleCount <= 0)
        {
            particleCount = 0;
        }

        particleText.text = particleCount.ToString();
        return particleCount;
    }

    // Highlight current element
    public void highlighter(GameObject highlighter)
    {
        if (protons <= 0 || protons > element.Count)
        {
            highlighter.SetActive(false);
            return;
        }

        highlighter.SetActive(true);
        highlighter.transform.position =
            element[protons - 1].transform.position;
    }
    bool IsStable(int protons, int neutrons)
    {
        if (protons == 0) return false;

        // Light elements
        if (protons <= 20)
        {
            return Mathf.Abs(neutrons - protons) <= 1;
        }

        // Heavy elements
        float ratio = (float)neutrons / protons;
        return ratio >= 1.0f && ratio <= 1.5f;
    }

    // --- PUBLIC FUNCTIONS FOR BUTTONS ---
    public void AddElectron() { addElectron = true; }
    public void TakeElectron() { takeElectron = true; }

    public void AddProton() { addProton = true; }
    public void TakeProton() { takeProton = true; }

    public void AddNeutron() { addNeutron = true; }
    public void TakeNeutron() { takeNeutron = true; }
}
