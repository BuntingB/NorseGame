/* Script to control the player object's health
 * Programmer: Brandon Bunting
 * Date Created: 01/28/2022
 * Date Modified: 03/30/2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //
    PlayerController playerObj;

    private float _maxHealth = 100;
    private float _currentHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        playerObj = GetComponent<PlayerController>();
        ResetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentHealth <= 0)
        {
            playerObj.Die();
        }
    }

    //
    public void HealDamage(float healAmount)
    {
        _currentHealth += healAmount;

        if (_currentHealth > _maxHealth)
        {
            ResetHealth();
        }
    }

    //
    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
    }

    //
    private void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }

    //
    public float GetMaxHealth()
    {
        return _maxHealth;
    }

    //
    public float GetHealth()
    {
        return _currentHealth;
    }
}
