using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float BulletSpeed;
    // Start is called before the first frame update

    private void Awake()
    {
        transform.name = transform.name.Replace("(Clone)", "").Trim();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "asteroid")
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * BulletSpeed;
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
