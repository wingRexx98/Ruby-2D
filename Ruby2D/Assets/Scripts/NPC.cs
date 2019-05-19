using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float displayTime = 4f;
    public GameObject dialogBox;
    float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = -1f;
        dialogBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if(currentTime < 0)
            {
                dialogBox.SetActive(false);
            }
        }
    }

    public void ActivateDialog()
    {
        dialogBox.SetActive(true);
        currentTime = displayTime;
    }
}
