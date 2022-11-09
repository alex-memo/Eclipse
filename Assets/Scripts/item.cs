using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
/**
* @memo 2022
* Scriptable Objact for Items
*/
public class item : ScriptableObject
{
    public string itemName;
    public GameObject itemObject;
    public bool isMove;
    public  EffectType effectType;
    public float effectDuration;
    /**
* @memo 2022
* enumerator for effect types
*/
    public enum EffectType {Coin, Small_Mushroom, Big_Mushroom, Giant_Mushroom,
        Fire_Plant, Star,  Ice_Plant, OneUP};
}
