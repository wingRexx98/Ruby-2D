using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControll : MonoBehaviour
{
    public float speed = 5f;//speed too smal the character won't move
    private Rigidbody2D rigidbody2;
    private Animator anim;
    Vector2 direction = new Vector2(1, 0);

    bool isWalking = false;

    public float maxHealth = 5f;
    public float invicibilityTime = 2f;
    public float health { get { return currentHealth; } }//get the current health without making it pucbic
    float currentHealth;
    float timer;
    bool isInvincible;

    AudioSource audio;

    public AudioClip shoot;
    public AudioClip getHit;

    public GameObject projectile;
    public float shootSpeed = 200f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;

        audio = GetComponent<AudioSource>();

        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;//make sure that 10 frame is rendered
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvincible)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                isInvincible = false;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            //Raycast hit store result from Physic2D.raycast
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2.position + Vector2.up * 0.5f,//Ruby;s position from the center of the body
                                                direction,//the direction for the ray to go
                                                1.5f, // the ray's max lenght
                                                LayerMask.GetMask("NPC")//the targeted layers to return result
                                                );
            if(hit.collider != null)
            {
                Debug.Log("Cast hit: " + hit.collider.gameObject);
                NPC npc = hit.collider.GetComponent<NPC>();
                if(npc != null)
                {
                    npc.ActivateDialog();
                }
            }
        }
        
    }

    private void FixedUpdate()
    {
        float hortizontalinput = Input.GetAxis("Horizontal");
        float verticalinput = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(hortizontalinput, verticalinput);//store the input

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))//check if the input is !=0 so the direction of the spirte match
        {
            direction = move;
            direction.Normalize();
            isWalking = true;
        }

        anim.SetFloat("Look X", direction.x);
        anim.SetFloat("Look Y", direction.y);
        anim.SetFloat("Speed", move.magnitude);

        //move by changing the position//smoother
        Vector2 position = rigidbody2.position;
        position = position + speed * move * Time.fixedDeltaTime;
        rigidbody2.MovePosition(position);
    }

    public void ChangeHealth(float health)
    {
        if(health < 0)//<0 means losing life
        {
            anim.SetTrigger("Hit");
            if (isInvincible)
                return;//return or existing function ince is already being hurt
            isInvincible = true;
            timer = invicibilityTime;
            PlaySound(getHit);
        }
        currentHealth = Mathf.Clamp(currentHealth + health, 0, maxHealth); //Clamp is to make sure the current heath stays between 0 and max health
        HealthBar.instance.SetValue(currentHealth/maxHealth);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(projectile, rigidbody2.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile cog = bullet.GetComponent<Projectile>();
        cog.Shoot(direction, shootSpeed);
        PlaySound(shoot);
        anim.SetTrigger("Launch");
    }

    public void PlaySound(AudioClip clip)
    {
        audio.PlayOneShot(clip);
    }
}
