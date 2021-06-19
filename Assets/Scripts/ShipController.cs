using System.Collections;
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
    [SerializeField] private GameObject[] rays;
    [SerializeField] private float BackupDistance;
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
        PositionDetection();
    }

    void FixedUpdate()
    {
        ship.velocity = (ShipVelocity * Vector2.right) * ShipSpeed;
    }

    void MovementHandler()
    {
        
        if ((Input.GetAxis("Horizontal") == 0))
        {
            ShipTarVelocity = 0;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            ShipTarVelocity = -1;
        }
        else if (Input.GetAxis("Horizontal") > 0)
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
    
    void PositionDetection()
    {
        Debug.Log("runningshit");
        Debug.DrawRay(rays[0].transform.position, Vector2.up, Color.red);
        Debug.DrawRay(rays[1].transform.position, Vector2.up, Color.green);
        RaycastHit2D Left, Right, Mid;
        Left = Physics2D.Raycast(rays[0].transform.position, Vector2.up);
        Right = Physics2D.Raycast(rays[1].transform.position, Vector2.up);
        Mid = Physics2D.Raycast(rays[1].transform.position, Vector2.up);
        if (Left.distance < BackupDistance || Right.distance < BackupDistance || Mid.distance < BackupDistance)
        {
            gh.GetComponent<AutoScroll>().Backup(BackupDistance-Left.distance, BackupDistance-Right.distance, BackupDistance-Mid.distance);
        }
        else
        {
            gh.GetComponent<AutoScroll>().BackupReset();
        }
    }
}
