using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PointAndClickObject
{
    public GameObject[]  Instances;
    public bool ActiveSetting;

    public PointAndClickObject(GameObject[] Instances, bool ActiveSetting)
    {
        this.Instances = Instances;
        this.ActiveSetting = ActiveSetting;
    }

    public bool GetActiveSetting()
    {
        return ActiveSetting;
    }

    public void SwapActiveSetting()
    {
        ActiveSetting = !ActiveSetting;
        foreach (GameObject instance in Instances)
        {
            instance.SetActive(ActiveSetting);
        }
    }

    public GameObject[] GetInstances()
    {
        return Instances;
    }

    public void SetInstances(GameObject[] newinstances)
    {
        Instances = newinstances;
    }
}
