/* Subclass for the dagger weapon
 * Programmer: Brandon Bunting
 * Date Created: 02/18/2022
 * Date Modified: 03/23/2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : Weapon
{
    //
    public GameObject _hitbox;
    public GameObject _enemyHitbox;
    public bool _isEnemyWeapon = false;

    //
    Transform _trans;
    float _timeToNextAttack = 0f;
    float _cooldown = 0.5f;
    float _enemyCooldown = 1f;
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
            if (!_isEnemyWeapon)
            {
                GameObject dagger = Instantiate(_hitbox, _trans.position, _trans.rotation);
                dagger.GetComponent<WeaponHitbox>().SetDamage(_damage, attackModifier);
                Destroy(dagger, 0.2f);

                _timeToNextAttack = Time.realtimeSinceStartup + _cooldown;
            }
            else
            {
                GameObject dagger = Instantiate(_enemyHitbox, _trans.position, _trans.rotation);
                dagger.GetComponent<WeaponHitbox>().SetDamage(_damage, attackModifier);
                Destroy(dagger, 0.2f);

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
