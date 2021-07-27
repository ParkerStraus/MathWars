using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class RangeTrigger : MonoBehaviour
{
    [SerializeField] private GameObject GH;
    [SerializeField] private GameObject effectee;
    [SerializeField] private bool isTimeline;
    [SerializeField] private bool isActivated;
    [SerializeField] private UnityEngine.Events.UnityEvent triggerEvent;
    [SerializeField] private float range;
    // Start is called before the first frame update
    void Start()
    {
        GH = GameObject.Find("GameHandler");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(gameObject.transform.position, GH.transform.position);
        if (distance <= range && !isActivated)
        {
            if (isTimeline)
            {
                effectee.GetComponent<PlayableDirector>().Play();
            }
            else
            {
                triggerEvent.Invoke();
            }
            isActivated = true;
        }
    }
}
