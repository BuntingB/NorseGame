/* Superclass for all weapons
 * Programmer: Brandon Bunting
 * Date Created: 02/18/2022
 * Date Modified: 02/21/2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    //

    // Maybe change to proper function, if possible
    public abstract void Attack(float attackModifier);

    // Change to proper function for subclasses to use
    public bool CanAttack(float _timeToNextAttack)
    {
        if (Time.realtimeSinceStartup >= _timeToNextAttack)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public abstract string GetWeaponType();
}
