/* Superclass for enemies
 * Programmer: Brandon Bunting
 * Date Created: 02/18/2022
 * Date Modified: 04/01/2022
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //
    Rigidbody2D _rb;
    ScoreManager _scoreManager;
    private float _maxHealth = 100;
    private float _currentHealth;
    private float _damageResilience = 0f;
    public int _pointsUponDeath = 10;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _scoreManager = GameObject.Find("HUD").GetComponent<ScoreManager>();
        ResetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentHealth <= 0)
        {
            if (TryGetComponent<Warrior>(out Warrior warrior))
            {
                warrior.DropWeapon();
            }
            else if (TryGetComponent<Thief>(out Thief thief))
            {
                thief.DropWeapon();
            }
            else if (TryGetComponent<Archer>(out Archer archer))
            {
                archer.DropWeapon();
            }

            Die();
        }
    }

    // Kills enemy object
    private void Die()
    {
        Utility.GetPlayerObject().GetComponent<Player>().HealDamage(25f);
        _scoreManager.AddScore(_pointsUponDeath);
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
