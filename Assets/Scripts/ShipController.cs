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
        Debug.DrawRay(rays[0].transform.position, Vector2.up, Color.red);
        Debug.DrawRay(rays[1].transform.position, Vector2.up, Color.green);
        Debug.DrawRay(rays[2].transform.position, Vector2.up, Color.white);
        /*RaycastHit2D Left, Right, Mid;
        Left = Physics2D.Raycast(rays[0].transform.position, Vector2.up, BackupDistance);
        Right = Physics2D.Raycast(rays[1].transform.position, Vector2.up, BackupDistance);
        Mid = Physics2D.Raycast(rays[2].transform.position, Vector2.up, BackupDistance);
        if (Left || Right || Mid)
        {
            if (Left.distance < BackupDistance || Right.distance < BackupDistance || Mid.distance < BackupDistance)
            {
                bool BulletSensed = false;
                if (Left.transform.tag == "Bullet")
                {
                    BulletSensed = true;
                }
                else if (Right.transform.tag == "Bullet")
                {
                    BulletSensed = true;
                }
                else if (Mid.transform.tag == "Bullet")
                {
                    BulletSensed = true;
                }

                if (!BulletSensed)
                {
                    gh.GetComponent<AutoScroll>().Backup(Left.distance, Right.distance, Mid.distance);
                    Debug.Log("runningshit " + Left.distance + " " + Right.distance + " " + Mid.distance);
                }
            }
        }*/
        RaycastHit2D hit;
        hit = Physics2D.BoxCast(rays[1].transform.position, new Vector2(this.gameObject.GetComponent<BoxCollider2D>().size.x, 0.01f), 0.0f, Vector2.up,RayDist);

        if (hit.collider != null)
        {
            Debug.Log("now sensing " + hit.collider.name+ " .... runningshit " + hit.distance);

            if (hit.distance < BackupDistance)
            {
                    gh.GetComponent<AutoScroll>().Backup(hit.distance);
            }
        }
        else
        {
            gh.GetComponent<AutoScroll>().BackupReset();
        }
    }
}
