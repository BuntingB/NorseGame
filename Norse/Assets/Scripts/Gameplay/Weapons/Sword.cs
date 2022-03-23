/* Subclass for the sword weapon
 * Programmer: Brandon Bunting
 * Date Created: 02/18/2022
 * Date Modified: 03/23/2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    //
    public GameObject _hitbox;
    public GameObject _enemyHitbox;
    public bool _isEnemyWeapon = false;

    //
    Transform _trans;
    float _timeToNextAttack = 0f;
    float _cooldown = 0.8f;
    float _enemyCooldown = 1.6f;
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
            if (!_isEnemyWeapon) 
            {
                GameObject sword = Instantiate(_hitbox, _trans.position, _trans.rotation);
                sword.GetComponent<WeaponHitbox>().SetDamage(_damage, attackModifier);
                Destroy(sword, 0.2f);

                _timeToNextAttack = Time.realtimeSinceStartup + _cooldown;
            }
            else
            {
                GameObject sword = Instantiate(_enemyHitbox, _trans.position, _trans.rotation);
                sword.GetComponent<WeaponHitbox>().SetDamage(_damage, attackModifier);
                Destroy(sword, 0.2f);

                _timeToNextAttack = Time.realtimeSinceStartup + _enemyCooldown;
            }
        }
    }

    //
    public override string GetWeaponType()
    {
        return _weaponType;
    }

    public override bool IsAmmoUsed()
    {
        return false;
    }

    public override int GetAmmo()
    {
        return 0;
    }

    public override void SetAmmo(int ammo)
    {
        return;
    }
}
