/* Subclass for the bow weapon
 * Programmer: Brandon Bunting
 * Date Created: 02/18/2022
 * Date Modified: 02/28/2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    //
    public GameObject _arrow;

    //
    Transform _trans;
    int _ammo = 15;
    float _damage = 20f;
    float _arrowSpeed = 20f;
    float _timeToNextAttack = 0f;
    float _cooldown = 0.6f;
    string _weaponType = "Bow";

    //
    private void Start()
    {
        _trans = GetComponent<Transform>();
    }

    //
    private void Update()
    {
        AimAt();
    }

    //
    private void AimAt()
    {
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        Vector2 lookAtDir;

        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(_trans.position);

        lookAtDir = mousePos - new Vector2(playerScreenPoint.x, playerScreenPoint.y);

        float angle = Mathf.Atan2(-lookAtDir.x, lookAtDir.y) * Mathf.Rad2Deg;
        angle += 90; //Weapon Model faces the right, where the above math assumes it is oriented upwards

        _trans.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    //
    public override void Attack(float attackModifier)
    {
        if (CanAttack(_timeToNextAttack) && _ammo > 0)
        {
            GameObject arrow = Instantiate(_arrow, _trans.position, _trans.rotation);
            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();

            arrow.GetComponent<Arrow>().SetDamage(_damage, attackModifier);

            rb.AddForce(transform.right * _arrowSpeed, ForceMode2D.Impulse);

            _ammo -= 1;

            _timeToNextAttack = Time.realtimeSinceStartup + _cooldown;
        }
    }

    public override string GetWeaponType()
    {
        return _weaponType;
    }

    public override bool IsAmmoUsed()
    {
        return true;
    }

    public override int GetAmmo()
    {
        return _ammo;
    }

    public override void SetAmmo(int ammo)
    {
        _ammo = ammo;
    }
}
