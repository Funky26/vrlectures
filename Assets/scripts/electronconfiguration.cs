using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class electronconfiguration : MonoBehaviour
{
    private int electrons =0;
    private int protons =0;
    private int neutrons=0;

    private bool stability = true;

    public TextMeshProUGUI stabilityTxt;
    public TextMeshProUGUI neutronsTxt;
    public TextMeshProUGUI protonsTxt;
    public TextMeshProUGUI electronsTxt;

    public bool addElectron = false;
    public bool takeElectron = false;
    public bool addProton = false;
    public bool takeProton = false;
    public bool addNeutron = false;
    public bool takeNeutron = false;

    public GameObject highlighterObj;

    public List<GameObject> element;

    public int GetProtons() { return protons; }
    public int GetNeutrons() { return neutrons; }
    public int GetElectrons() { return electrons; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update values
        protons = particleCount(addProton, takeProton, protons, protonsTxt);
        neutrons = particleCount(addNeutron, takeNeutron, neutrons, neutronsTxt);
        electrons = particleCount(addElectron, takeElectron, electrons, electronsTxt);

        // Reset input triggers
        addProton = takeProton = false;
        addNeutron = takeNeutron = false;
        addElectron = takeElectron = false;

        // Update stability
        stability = !(neutrons - 1 == protons && neutrons > protons);

        // Highlight
        highlighter(highlighterObj);
    }
    public int particleCount(bool give, bool take, int particleCount, TextMeshProUGUI particleText)
    {
        if (give)
        {
            particleCount++;
        }
        if (take)
        {
            particleCount--;
        }
        particleText.text = particleCount.ToString();
        return particleCount;
    }

    public void highlighter(GameObject highlighter)
    {
        if (protons <= 0 || protons > element.Count) return;

        element[protons - 1].transform.position = highlighter.transform.position;
    }

}
