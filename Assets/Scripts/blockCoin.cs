using UnityEngine;
/**
* @memo 2022
* block coin class to handle coinscoming out from blocks
*/
public class blockCoin : MonoBehaviour
{
    private float timeDie = 2f;
    /**
* @memo 2022
* start method, adds coin to player, calls to do animation, then dies after timeDie
*/
    private void Start()
    {
        gameManager.instance.addCoin();
        StartCoroutine(transform.anim(timeDie));
        Destroy(gameObject,timeDie);
    }
}
