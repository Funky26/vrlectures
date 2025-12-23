using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class uimanager : MonoBehaviour
{
    public List<GameObject> lectureButton;
    public List<GameObject> lectureOverview;
    public GameObject functioningOverview;
    public GameObject basicOverview;


    public void showFuncOverview()
    {
        functioningOverview.SetActive(true);
        basicOverview.SetActive(false);
    }
    public void hideFuncOverview()
    {
        functioningOverview.SetActive(false);
        basicOverview.SetActive(true);
    }
}
