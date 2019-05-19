using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;//speed too smal the character won't move
    private Rigidbody2D rigidbody2;
    public bool vertical;
    private Animator anim;
    public ParticleSystem smoke;

    public float moveTime = 2f;
    float timer;

    int direction =1;

    bool broken = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        timer = moveTime;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        
        if (!broken)
        {
            return;
        }

        if (timer < 0)
        {
            direction *= -1;
            timer = moveTime;
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody2.position;
        if (vertical)
        {
            position.y = position.y + speed * Time.fixedDeltaTime * direction;
            anim.SetFloat("MoveX", 0);
            anim.SetFloat("MoveY", direction);
        }
        else
        {
            position.x = position.x + speed * Time.fixedDeltaTime * direction;
            anim.SetFloat("MoveX", direction);
            anim.SetFloat("MoveY", 0);
        }

        rigidbody2.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CharacterControll controller = collision.gameObject.GetComponent<CharacterControll>();
        if (controller != null) {
            if (controller.health > 0)
            {
                controller.ChangeHealth(-1);
            }
        }

    }

    public void Fix()
    {
        broken = false;
        rigidbody2.simulated = false;
        anim.SetTrigger("Fixed");
        smoke.Stop();
    }
}
