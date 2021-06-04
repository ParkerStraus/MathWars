using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBlast : MonoBehaviour
{
    public int Value;
    [SerializeField] private float Movespeed;

    // Update is called once per frame
    void Update()
    {
        transform.position += (Movespeed * Time.deltaTime)*Vector3.up;
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }
}
