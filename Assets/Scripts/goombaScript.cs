/**
 * @memo 2022
 * goomba script, sub from enemyScript
 */
public class goombaScript : enemyScript
{
    /**
 * @memo 2022
 * when goomba die then destroy this obj after .5 sec
 */
    protected override void onDie()
    {
        base.onDie();
        Destroy(gameObject, .5f);
    }
}
