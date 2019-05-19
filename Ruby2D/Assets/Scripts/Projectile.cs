using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rigid;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude > 1000f)
        {
            Destroy(gameObject);
        }
    }

    public void Shoot(Vector2 direction, float force)
    {
        rigid.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
        if (collision.gameObject.tag != "player")
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Enemy")
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            enemy.Fix();
            Destroy(gameObject);
        }
    }
}
