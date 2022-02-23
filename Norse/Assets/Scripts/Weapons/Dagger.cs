/* Subclass for the dagger weapon
 * Programmer: Brandon Bunting
 * Date Created: 02/18/2022
 * Date Modified: 02/21/2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : Weapon
{
    //
    public GameObject _hitbox;

    //
    Transform _trans;
    float _timeToNextAttack = 0f;
    float _cooldown = 0.2f;
    float _damage = 17f;
    string _weaponType = "Dagger";

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
            GameObject dagger = Instantiate(_hitbox, _trans.position, _trans.rotation);
            dagger.GetComponent<WeaponHitbox>().SetDamage(_damage, attackModifier);
            Destroy(dagger, 0.2f);

            _timeToNextAttack = Time.realtimeSinceStartup + _cooldown;
        }
    }

    //
    public override string GetWeaponType()
    {
        return _weaponType;
    }
}
