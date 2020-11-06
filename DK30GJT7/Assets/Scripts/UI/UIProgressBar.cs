using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProgressBar : MonoBehaviour
{
    public Health target;
    public Image mask, bar;

    //bar images
    public Sprite greenBar, yellowBar, redBar;

    // Update is called once per frame
    void Update()
    {
        GetHealthPercentage();
    }

    void GetHealthPercentage()
    {
        float healthPercentage = (float)target.currentHealth / (float)target.maxHealth;
        mask.fillAmount = healthPercentage;

        //change bar colour
        if (healthPercentage > 0.6)
        {
            bar.sprite = greenBar;
        }
        else if (healthPercentage > 0.3)
        {
            bar.sprite = yellowBar;
        }
        else
        {
            bar.sprite = redBar;
        }

    }
}
