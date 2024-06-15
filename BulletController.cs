using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;
    Rigidbody2D myRibidbody;
    PlayerMovement player;
    float xSpeed;
    // Start is called before the first frame update
    void Start()
    {
        myRibidbody = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        myRibidbody.velocity = new Vector2(xSpeed, 0f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
