using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
public class item : ScriptableObject
{
    public string itemName;
    public bool isMove;
    public int hasEffect;
    public float effectDuration;
}
