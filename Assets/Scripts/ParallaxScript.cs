using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    [SerializeField] private GameObject cameraObj;
    private Renderer rend;
    private float HoriOffset;
    public float HoriSpeed;
    private float VertOffset;
    public float VertSpeed;
    public float CamDist;
    public float Speed;

    private void Start()
    {
        HoriOffset = transform.position.x;
        VertOffset = transform.position.y;   
        cameraObj = GameObject.Find("GameHandler");
        rend = GetComponent<Renderer>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(HoriOffset - cameraObj.transform.position.x * HoriSpeed, VertOffset  - cameraObj.transform.position.y*VertSpeed, transform.position.z); 
        rend.material.mainTextureOffset = new Vector2(cameraObj.transform.position.x * Speed, 0);
        
    }
}
