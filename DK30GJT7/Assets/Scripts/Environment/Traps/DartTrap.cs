using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartTrap : MonoBehaviour
{
    [SerializeField]
    GameObject dart;

    float cooldown = 5f, currentTime = 0f;
    List<GameObject> targetObjects = new List<GameObject>();

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if(currentTime <= 0 && targetObjects.Count > 0)
        {
            ShootDart();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Health>())
        {
            targetObjects.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Health>())
        {
            targetObjects.Remove(collision.gameObject);
        }
    }

    void ShootDart()
    {
        GameObject shotDart = Instantiate(dart, new Vector3(0, 0, 0), Quaternion.identity);
        shotDart.transform.position = transform.position + new Vector3(0, -1.5f);
        shotDart.transform.Rotate(0, 0, 180);
        currentTime = cooldown;
    }
}
