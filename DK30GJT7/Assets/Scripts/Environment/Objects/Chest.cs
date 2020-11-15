using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    bool open = false;
    Animator anim;
    [SerializeField]
    GameObject storedObject;

    Vector2 offset = new Vector2(0, -1);

    // Start is called before the first frame update
    void Start()
    {
        GameObject vfx = transform.GetChild(0).gameObject;
        anim = vfx.GetComponent<Animator>();
    }

    public void OpenChest()
    {
        if (!open)
        {
            open = true;
            anim.SetTrigger("Open");
            FindObjectOfType<AudioManager>().Play("Chest_Open");

            if (storedObject != null)
            {
                Vector2 targetPos = (Vector2)transform.position + offset;

                RaycastHit2D hit = Physics2D.Raycast(targetPos, -Vector2.up);
                if (hit.collider != null)
                {
                    targetPos = (Vector2)transform.position;
                }


                GameObject reward = Instantiate(storedObject, targetPos, Quaternion.identity);
                reward.AddComponent<AddToInterests>();
            }
        }
    }


}
