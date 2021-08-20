using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PrerequisiteChecker : MonoBehaviour
{
    private GameObject gh;
    [SerializeField] private UnityEvent GrantedEvent;
    [SerializeField] private PointAndClickInventoryItem requireItem;
    [SerializeField] private UnityEvent DeniedEvent;

    public void Start()
    {
        gh = GameObject.Find("Main Camera");
            
    }

    public void HeldItemChecker()
    {
        if(gh.GetComponent<PointAndClickHandler>().HeldItem == requireItem)
        {
            GrantedEvent.Invoke();
        }
        else
        {
            DeniedEvent.Invoke();
        }
    }
}
