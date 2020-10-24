using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class ProgressBar
{
    GameObject backgroundHealthBar, currentHealthBar, attachedObject;
    Vector3 healthBarScale;
    bool recolourHealth;
    Color currentColour;
    SpriteRenderer barRend, bgRend;
    Image barImage, bgImage;

    float localOffsetX = 0f;

    //  Progress/health bar created by Sciath
     
     
    public ProgressBar(GameObject obj, Vector3 backgroundScale, Vector3 healthBarScale, Vector2 localPos, Color bgColur, Color fgColour, bool recolourHealth)
    {
        this.recolourHealth = recolourHealth;
        currentColour = fgColour;
        this.attachedObject = obj;
        this.healthBarScale = healthBarScale;
        this.localOffsetX = localPos.x;
        backgroundHealthBar = InitHealthBar("Background_bar", backgroundScale, new Vector3(localPos.x, localPos.y, -1), bgColur);
        currentHealthBar = InitHealthBar("Health_bar", healthBarScale, new Vector3(localPos.x, localPos.y, -2), fgColour);
        barRend = currentHealthBar.GetComponent<SpriteRenderer>();
        bgRend = backgroundHealthBar.GetComponent<SpriteRenderer>();
    }

    public ProgressBar(GameObject obj, Vector3 backgroundScale, Vector3 healthBarScale, Vector2 localPos, Color bgColur, Color fgColour)
    {
        //creates UI progress bar
        currentColour = fgColour;
        this.attachedObject = obj;
        this.healthBarScale = healthBarScale;
        this.localOffsetX = localPos.x;
        backgroundHealthBar = InitHealthBarImage("Background_bar", backgroundScale, new Vector3(localPos.x, localPos.y, -2), bgColur);
        currentHealthBar = InitHealthBarImage("Health_bar", healthBarScale, new Vector3(localPos.x, localPos.y, -1), fgColour);
        barImage = currentHealthBar.GetComponent<Image>();
        bgImage = backgroundHealthBar.GetComponent<Image>();
    }

    public void UpdateHealthbar(float currentValue, float maxValue)
    {
        float percentHealth = currentValue / maxValue;
        ToggleVisible(true);
        if (percentHealth == 1)
        {
            ToggleVisible(false);
        }

        float scale = 200f;
        if (barImage)
        {
            //ui scale much smaller?
            scale = 0.02f;
        }
        if (percentHealth > 0)
        {
            currentHealthBar.transform.localScale = new Vector3(healthBarScale.x * percentHealth, healthBarScale.y, healthBarScale.z);

            float barOffset = (healthBarScale.x - (healthBarScale.x * percentHealth)) / scale;
            currentHealthBar.transform.localPosition = new Vector3(-barOffset + localOffsetX, currentHealthBar.transform.localPosition.y, -2);
        }

        //recolour healthbar for units
        if (recolourHealth) //assume green>yellow>red
        {

            if (percentHealth > 0.6)
            {
                if (currentColour != Color.green)
                {
                    SetBarColour(Color.green);
                }
            }
            else if (percentHealth > 0.3)
            {
                if (currentColour != Color.yellow)
                {
                    SetBarColour(Color.yellow);
                }
            }
            else
            {
                if (currentColour != Color.red)
                {
                    SetBarColour(Color.red);
                }
            }
        }
    }

    void SetBarColour(Color colour)
    {
        if (barRend != null)
        {
            barRend.color = colour;
        }
        else if (barImage != null)
        {
            barImage.color = colour;
        }
    }

    GameObject InitHealthBar(string name, Vector3 size, Vector3 localPosition, Color colour)
    {
        GameObject bar = new GameObject(name);
        bar.transform.parent = attachedObject.transform;
        bar.transform.localPosition = localPosition;
        bar.transform.localScale = size;
        SpriteRenderer rend = bar.AddComponent<SpriteRenderer>();
        rend.sprite = Resources.Load<Sprite>("pixel_1x1");
        rend.sortingLayerName = "Foreground";
        rend.color = colour;
        return bar;
    }

    GameObject InitHealthBarImage(string name, Vector3 size, Vector3 localPosition, Color colour)
    {
        GameObject bar = new GameObject(name);
        bar.transform.parent = attachedObject.transform;
        bar.transform.localPosition = localPosition;
        bar.transform.localScale = size;
        Image img = bar.AddComponent<Image>();
        img.sprite = Resources.Load<Sprite>("pixel_1x1");
        img.color = colour;
        return bar;
    }

    public void ToggleVisible(bool visible)
    {
        if (barRend != null)
        {
            barRend.enabled = visible;
            bgRend.enabled = visible;
        }
        else if (barImage != null)
        {
            barImage.enabled = visible;
            bgImage.enabled = visible;
        }
    }

    public void SetLocalPos(Vector2 localPos)
    {
        this.localOffsetX = localPos.x;
        backgroundHealthBar.transform.localPosition = localPos;
        currentHealthBar.transform.localPosition = localPos;
    }
}
