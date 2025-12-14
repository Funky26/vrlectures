using System.Collections.Generic;
using UnityEngine;

public class atomScript : MonoBehaviour
{
    public electronconfiguration configScript;

    [Header("Prefabs")]
    public GameObject protonPrefab;
    public GameObject neutronPrefab;
    public GameObject electronPrefab;

    [Header("Parents")]
    public Transform nucleusParent;
    public Transform electronParent;

    private int protons;
    private int neutrons;
    private int electrons;

    private int lastProtons;
    private int lastNeutrons;
    private int lastElectrons;

    // Bohr shell capacities
    private int[] maxValenceAtomCount = { 2, 8, 18, 32 };

    private readonly List<GameObject> spawnedProtons = new();
    private readonly List<GameObject> spawnedNeutrons = new();
    private readonly List<GameObject> spawnedElectrons = new();

    void Update()
    {
        protons = configScript.GetProtons();
        neutrons = configScript.GetNeutrons();
        electrons = configScript.GetElectrons();

        // Rebuild atom only if something changed
        if (protons != lastProtons ||
            neutrons != lastNeutrons ||
            electrons != lastElectrons)
        {
            BuildAtom();

            lastProtons = protons;
            lastNeutrons = neutrons;
            lastElectrons = electrons;
        }
    }

    void BuildAtom()
    {
        ClearAtom();
        BuildNucleus();
        BuildElectrons();
    }

    void ClearAtom()
    {
        spawnedProtons.ForEach(Destroy);
        spawnedNeutrons.ForEach(Destroy);
        spawnedElectrons.ForEach(Destroy);

        spawnedProtons.Clear();
        spawnedNeutrons.Clear();
        spawnedElectrons.Clear();
    }

    void BuildNucleus()
    {
        float nucleusRadius = 0.12f;

        for (int i = 0; i < protons; i++)
            SpawnParticle(protonPrefab, nucleusParent, nucleusRadius, spawnedProtons);

        for (int i = 0; i < neutrons; i++)
            SpawnParticle(neutronPrefab, nucleusParent, nucleusRadius, spawnedNeutrons);
    }

    void BuildElectrons()
    {
        int electronsLeft = electrons;

        for (int shell = 0; shell < maxValenceAtomCount.Length; shell++)
        {
            if (electronsLeft <= 0)
                break;

            int capacity = maxValenceAtomCount[shell];
            int count = Mathf.Min(capacity, electronsLeft);

            SpawnElectronShell(shell + 1, count);

            electronsLeft -= count;
        }
    }

    void SpawnElectronShell(int shellIndex, int count)
    {
        float radius = 0.35f + shellIndex * 0.25f;

        for (int i = 0; i < count; i++)
        {
            float angle = i * Mathf.PI * 2f / count;

            Vector3 pos = new Vector3(
                Mathf.Cos(angle) * radius,
                0f,
                Mathf.Sin(angle) * radius
            );

            GameObject e = Instantiate(electronPrefab, electronParent);
            e.transform.localPosition = pos;

            // Optional: orbital motion
            ElectronOrbit orbit = e.GetComponent<ElectronOrbit>();
            if (orbit != null)
            {
                orbit.center = nucleusParent;
            }

            spawnedElectrons.Add(e);
        }
    }

    void SpawnParticle(
        GameObject prefab,
        Transform parent,
        float radius,
        List<GameObject> list)
    {
        GameObject p = Instantiate(prefab, parent);
        p.transform.localPosition = Random.insideUnitSphere * radius;
        list.Add(p);
    }
}
