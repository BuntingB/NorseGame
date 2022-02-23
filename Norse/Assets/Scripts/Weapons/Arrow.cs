using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //
    float _damage;
    
    //Destroys on collision with object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }

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
