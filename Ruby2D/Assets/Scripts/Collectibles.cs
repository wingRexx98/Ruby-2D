using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    float changeHealth = 1f;

    AudioClip collection;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterControll controller = collision.GetComponent<CharacterControll>();
        

        if(controller != null)
        {
            if(controller.health < controller.maxHealth)
            {
                controller.ChangeHealth(changeHealth);
                Destroy(gameObject);
                controller.PlaySound(collection);
            }
        }
        
    }
}
