/* Script to detect climbable surfaces for the player
 * Programmer: Brandon Bunting
 * Date Created: 03/22/2022
 * Date Modified: 03/22/2022
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetector : MonoBehaviour
{
    //
    public bool _isOnWall = false;
    GameObject _surface;

    //
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Terrain")
        {
            Collider2D collisionObject;
            if (collision.gameObject.TryGetComponent<Collider2D>(out collisionObject))
            {
                _surface = collision.gameObject;
                _isOnWall = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Terrain")
        {
            if (collision.gameObject == _surface)
            {
                _surface = null;
                _isOnWall = false;
            }
        }
    }
}
