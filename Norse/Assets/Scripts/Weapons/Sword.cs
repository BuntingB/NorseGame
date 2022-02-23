/* Subclass for the sword weapon
 * Programmer: Brandon Bunting
 * Date Created: 02/18/2022
 * Date Modified: 02/21/2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    //
    public GameObject _hitbox;

    //
    Transform _trans;
    float _timeToNextAttack = 0f;
    float _cooldown = 0.5f;
    float _damage = 25f;
    string _weaponType = "Sword";

    //
    private void Start()
    {
        _trans = GetComponent<Transform>();
    }

    //
    public override void Attack(float attackModifier)
    {
        if (CanAttack(_timeToNextAttack))
        {
            GameObject sword = Instantiate(_hitbox, _trans.position, _trans.rotation);
            sword.GetComponent<WeaponHitbox>().SetDamage(_damage, attackModifier);
            Destroy(sword, 0.2f);

            _timeToNextAttack = Time.realtimeSinceStartup + _cooldown;
        }
    }

    //
    public override string GetWeaponType()
    {
        return _weaponType;
    }
}
