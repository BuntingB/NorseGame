using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    //
    float _damage;

    //
    public void SetDamage(float damage, float attackModifier)
    {
        _damage = damage * attackModifier;
    }

    //
    public float GetDamage()
    {
        return _damage;
    }
}
