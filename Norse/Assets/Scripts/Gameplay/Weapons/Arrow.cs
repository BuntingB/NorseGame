using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //
    float _damage;

    //
    private void FixedUpdate()
    {
        Vector2 dir = GetComponent<Rigidbody2D>().velocity;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

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
