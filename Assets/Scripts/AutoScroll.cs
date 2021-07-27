using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScroll : MonoBehaviour
{

    [SerializeField] private float ScrollSpeed;
    [SerializeField] private float Distance;
    [SerializeField] private bool BackupActive;
    [SerializeField] private bool Stop = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Stop == false)
        {
            if (!BackupActive)
            {
                transform.position += Vector3.up * ScrollSpeed * Time.deltaTime;
            }
        }
    }

    public void StopAutoScroll()
    {
        Stop = !Stop;
    }

    public void Backup(float dis)
    {
        if (dis > 0)
        {
            BackupActive = true;
            transform.position += Vector3.down * ScrollSpeed * Time.deltaTime;
        }
    }
    
    public void BackupReset() { BackupActive = false; }
}
