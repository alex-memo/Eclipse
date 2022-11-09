using UnityEngine;
/**
* @memo 2022
* item script class, handles the item which script this is attached
*/
public class itemScript : MonoBehaviour
{
    private item item;
    itemEnemyMovement movement;
    /**
* @memo 2022
* start method, if the item is supposed to mode, adds component to move and sets direction and speed for it
*/
    private void Start()
    {
        if (item.isMove)
        {
            movement=gameObject.AddComponent(typeof(itemEnemyMovement))as itemEnemyMovement;
            movement.setDir(new Vector2(1, 0));
            movement.setSpeed(3);
        }        
    }
    /**
* @memo 2022
* onTrigger enter if the player enters collision then pick up
*/
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            pickUp();
        }
    }
    /**
* @memo 2022
* setter for this script item
*/
    public void setItem(item i)
    {
        item = i;
    }
    /**
* @memo 2022
* pick up switch statement based on item interacted 
*/
    private void pickUp()
    {
        switch (item.effectType)
        {
            case item.EffectType.Coin:
                gameManager.instance.addCoin();
                break;
            case item.EffectType.Small_Mushroom://turns into micro player

                break;
            case item.EffectType.Big_Mushroom:
                Controller.instance.grow();
                break;
            case item.EffectType.Giant_Mushroom:

                break;
            case item.EffectType.Fire_Plant:

                break;
            case item.EffectType.Star:
                Controller.instance.star();
                break;
            case item.EffectType.Ice_Plant:

                break;
            case item.EffectType.OneUP:
                gameManager.instance.addLife();
                break;
        }
        Destroy(gameObject);
    }
}
