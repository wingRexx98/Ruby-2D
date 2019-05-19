using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagearea : MonoBehaviour
{
    float changeHealth = -1f;

    private void OnTriggerStay2D(Collider2D collision)//OntriggerStays means as long as the player stays in the damage area they will lose health
    {
        CharacterControll controller = collision.GetComponent<CharacterControll>();

        if (controller != null)
        {
            if (controller.health > 0)
            {
                controller.ChangeHealth(changeHealth);
            }
        }

    }
}
