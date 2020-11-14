using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToInterests : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        KnightDecisionTree knight = FindObjectOfType<KnightDecisionTree>();
        //knight.Interests.Add(GetComponent<Interest>());
        //knight.KnownInterests.Add(GetComponent<Interest>());
        knight.GetInterests();
    }

}
