using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Interest
{
    public float InitInterest = 50;
    public string InterestType;
    public float CurrentInterest;
    public GameObject Object;
    public bool IsActive;
}
