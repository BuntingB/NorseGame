/* Superclass for enemies
 * Programmer: Brandon Bunting
 * Date Created: 02/18/2022
 * Date Modified: 03/22/2022
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //
    Rigidbody2D _rb;
    private float _maxHealth = 100;
    private float _currentHealth;
    private float _damageResilience = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        ResetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    // Kills enemy object
    private void Die()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().HealDamage(25f);
        Destroy(gameObject);
    }

    // Adds health to current health
    public void HealDamage(float healAmount)
    {
        _currentHealth += healAmount;

        if (_currentHealth > _maxHealth)
        {
            ResetHealth();
        }
    }

    // Subtracts damage from current health
    public void TakeDamage(float damage)
    {
        _currentHealth -= damage - (damage * _damageResilience);
    }

    // Resets enemy health to maximum
    private void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }

    // Returns max health value
    public float GetMaxHealth()
    {
        return _maxHealth;
    }

    // Returns current health value
    public float GetHealth()
    {
        return _currentHealth;
    }

    //Collision and Triggers
    #region Collision Detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "FriendlyArrow")
        {
            TakeDamage(collision.gameObject.GetComponent<Arrow>().GetDamage());
            _rb.velocity = Vector3.zero;
        }
        
    }

    //
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Vector2 playerPos = collision.gameObject.transform.position;
            if (playerPos.x - transform.position.x < 0)
            {
                _rb.velocity = new Vector2(-1f, _rb.velocity.y);
            }
            else
            {
                _rb.velocity = new Vector2(1f, _rb.velocity.y);
            }
        }
    }

    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FriendlySword" || collision.tag == "FriendlyMelee")
        {
            TakeDamage(collision.gameObject.GetComponent<WeaponHitbox>().GetDamage());
        }
    }
    #endregion
}
