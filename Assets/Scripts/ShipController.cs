using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float ShipSpeed;
    public Rigidbody2D ship;
    [SerializeField] private AudioClip bulsound;
    private int ShipVelocity;
    public bool GameFinished;
    public Transform BulletSpawnPoint;
    public Transform RCLeft;
    public Transform RCRight;
    public float RayDist;
    public GameObject bullet;
    public GameObject gh;
    public double FireLimit;
    [SerializeField]private double FireTimer;
    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
            MovementHandler();
            BulletFire();
    }

    void FixedUpdate()
    {
        ship.velocity = (ShipVelocity * Vector2.right) * ShipSpeed;
    }

    void MovementHandler()
    {
        RaycastHit2D Rayleft = Physics2D.Raycast(RCLeft.position,Vector2.left, RayDist);
        RaycastHit2D Rayright = Physics2D.Raycast(RCRight.position, Vector2.right, RayDist);
        Debug.DrawRay(RCLeft.position, Vector3.left, Color.red);
        Debug.DrawRay(RCRight.position, Vector3.right, Color.green);
        if (GameFinished == false)
        {
                if ((Input.GetKey("left") && Input.GetKey("right")))
                {
                    ShipVelocity = 0;
                }
                else if (Input.GetKey("left") && Rayleft.collider == null)
                {
                    ShipVelocity = -1;
                }
                else if (Input.GetKey("right") && Rayright.collider == null)
                {
                    ShipVelocity = 1;
                }
                else
                {
                    ShipVelocity = 0;
                }
            
            
        }
        else ShipVelocity = 0;
    }
    void BulletFire()
    {
        FireTimer += Time.deltaTime;
        if (Input.GetKeyDown("space") && FireTimer > FireLimit)
        {
            gh.GetComponent<GameHandler>().PlaySound(bulsound);
            Instantiate(bullet, BulletSpawnPoint.position, Quaternion.Euler(Vector3.zero));
            FireTimer = 0;
        }
    }
}
