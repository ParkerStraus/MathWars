﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShipController : MonoBehaviour
{
    public float ShipSpeed;
    public Rigidbody2D ship;
    [SerializeField] private int ShipTarVelocity;
    [SerializeField] private float ShipVelocity;
    [SerializeField] private float Speedup;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private int currentSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Transform BulletSpawnPoint;
    public float RayDist;
    public GameObject bullet;
    public GameObject gh;
    public double FireLimit;
    [SerializeField] private double FireTimer;
    [SerializeField] private Animation animation;
    // Start is called before the first frame update
    void Start()
    {
        FireTimer = FireLimit;
        ship = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animation>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementHandler();
        MoveAnimHandler();
        BulletFire();
    }

    void FixedUpdate()
    {
        ship.velocity = (ShipVelocity * Vector2.right) * ShipSpeed;
    }

    void MovementHandler()
    {
        
        if ((Input.GetKey("left") && Input.GetKey("right")))
        {
            ShipTarVelocity = 0;
        }
        else if (Input.GetKey("left"))
        {
            ShipTarVelocity = -1;
        }
        else if (Input.GetKey("right"))
        {
            ShipTarVelocity = 1;
        }
        else
        {
           ShipTarVelocity = 0;
        }

        ShipVelocity = Mathf.Lerp(ShipVelocity, ShipTarVelocity, Speedup);
        if (ShipVelocity <= 0.001f && -0.001f <= ShipVelocity)
        {
            ShipVelocity = 0;
        }
    }

    void MoveAnimHandler()
    {
        int Spriteindex = (int)( ( ShipVelocity*(sprites.Length/2) ) + sprites.Length / 2);
        currentSprite = Spriteindex;
        spriteRenderer.sprite = sprites[Spriteindex];
    }

    void BulletFire()
    {
        FireTimer += Time.deltaTime;
        int value;
        if (Input.GetKeyDown("space") && FireTimer > FireLimit && gh.GetComponent<GameHandler>().CheckNumberReady())
        {
            value = int.Parse(gh.GetComponent<GameHandler>().GetMathNum());
            GameObject Shot = Instantiate(bullet, BulletSpawnPoint.position, Quaternion.Euler(Vector3.zero));
            Shot.GetComponent<ShipBlast>().Value = value;
            FireTimer = 0;
        }
    }
}