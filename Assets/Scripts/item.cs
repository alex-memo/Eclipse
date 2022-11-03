using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
public class item : ScriptableObject
{
    public string itemName;
    public bool isMove;
    public  EffectType effectType;
    public float effectDuration;

    public enum EffectType {Small_Mushroom, Big_Mushroom, Giant_Mushroom,
        Fire_Plant, Star,  Ice_Plant, OneUP};
}
