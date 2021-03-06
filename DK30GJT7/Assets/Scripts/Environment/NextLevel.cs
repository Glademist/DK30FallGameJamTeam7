﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public GameObject player;
    public bool lastLevel = false;

    //Win panel
    public GameObject panel;
    public Animator anim;

    public static bool win = false;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.name != "Player")
        {
            return;
        }

        GameObject knight = GameObject.Find("Knight");

        if (knight)
        {
            int knightHealth = knight.GetComponent<Health>().currentHealth;
            UpdateKnightHealth.knightHealth = knightHealth;
            if (knightHealth <= 0)
            {
                return;
            }
        }

        if (!lastLevel)
        {
            player.transform.position = new Vector3(0, 0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            panel.SetActive(true);
            anim.SetTrigger("ShowMenu");

            //disable player movement
            player.gameObject.GetComponent<Movement>().enabled = false;
            win = true;
        }
    }
}
