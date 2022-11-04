using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * @memo 2022
 * static file for extensions for properties built in unity
 */
public static class Extensions
{
    private static LayerMask defaultMask = LayerMask.GetMask("Default");
    /**
     * @memo 2022
     * this creates an extension for rigidbodies which you can now call from any rigidbody
     * returns if is grounded esencially, creates a circle beneath you and if colliding
     * with anything that is on default mask then means you're grounded
     */
    public static bool Raycast(this Rigidbody2D rb, Vector2 dir)
    {
        if (rb.isKinematic)//you dont handle physics, then return false 
        {
            return false;
        }
        float radius = .25f;//radius of the raycast circle to create
        float distance = .375f;//distance from below player to check, currently hardcoded could be collider *-.375
        RaycastHit2D hit = Physics2D.CircleCast(rb.position, radius, dir.normalized, distance, defaultMask);//creates raycast circle

        //return hit.collider != null && hit.rigidbody != rb;
        if (hit.collider != null&&hit.rigidbody!=rb)//after && is safety measure for code not crash
        {
            return true;
        }
        return false;
    }
    /**
     * @memo 2022
     * gets the dot product from 2 vectors, if the angle is >.6 of 90 then means colliding in this case with your head
     */
    public static bool dotProduct(this Transform transform, Transform other, Vector2 direction)
    {
        //Vector2 dir = other.position - transform.position;//gets dir pointing from other to transform, most case transform is player
        return Vector2.Dot(direction.normalized, direction)>.6f;
    }
}
