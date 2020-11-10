using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    Health squire, knight;
    [SerializeField]
    GameObject panel;
    [SerializeField]
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        GameObject knightObj = GameObject.Find("Knight");
        if (knightObj)
        {
            knight = knightObj.GetComponent<Health>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(squire.currentHealth <= 0 ||  knight.currentHealth <= 0)
        {
            panel.SetActive(true);
            anim.SetTrigger("ShowMenu");

            //disable player movement
            squire.gameObject.GetComponent<Movement>().enabled = false;
        }
    }
}
