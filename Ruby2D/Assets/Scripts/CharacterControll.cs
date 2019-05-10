using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControll : MonoBehaviour
{
    public float speed = 5f;//speed too smal the character won't move
    private Rigidbody2D rigidbody2;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void FixedUpdate()
    {
        float hortizontalinput = Input.GetAxis("Horizontal");
        Debug.Log(hortizontalinput);
        rigidbody2.velocity = new Vector2(hortizontalinput * speed * Time.fixedDeltaTime, rigidbody2.velocity.y);
    }
}
