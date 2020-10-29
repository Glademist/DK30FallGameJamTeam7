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

            if(storedObject != null)
            {
                GameObject reward = Instantiate(storedObject, (Vector2)transform.position + offset, Quaternion.identity);
            }
        }
    }


}
