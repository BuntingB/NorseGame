/* Script to control the player object in "Project Norse"
 * Programmer: Brandon Bunting
 * Date Created: 01/28/2022
 * Date Modified: 01/28/2022
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Player body
    public GameObject _player;
    public Rigidbody2D _rb;

    //Player health
    public float _maxHealth = 100.0f;
    float _currentHealth = 100.0f;

    //Player stats
    [Range(1.0f, 20.0f)]
    public float _playerSpeed = 10.0f;
    [Range(1.0f, 100.0f)]
    public float _playerJumpHeight = 10.0f;
    bool _isGrounded = true;

    //Player death
    public bool _isDead = false;
    public Text _deathMessage;

    //Player weapons
    //3 standard weapons, possibly some specialty weapons too
    public List<GameObject> _weaponPrefabs;
    public GameObject _weaponParent;
    public bool _isHoldingWeapon = false;
    public GameObject _weaponObjInHand = null;
    bool _attacking = false;

    // Awake is called as the script loads
    private void Awake()
    {
        if (_rb == null) {
            _rb = GetComponent<Rigidbody2D>();
        }

        //ResetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        // Update health display
        //UpdateHealthDisplay();

        // Check if dead
        CheckIfDead();

        // Attack
        if (_attacking) {
            Attack();
        }

        // Movement
        MovePlayer();

        // Check for specialty input (TBD)
        //WeaponInput();
    }

    // Fixed Update is called every fixed frame
    private void FixedUpdate()
    {
        Jump();
    }

    // Receives player input and controls player accordingly
    void MovePlayer()
    {
        Vector2 movementDir = new Vector2(0.0f, 0.0f);

        // Crouch
        if (Input.GetKey(KeyCode.S))
        {
            //Change to crouch
        }

        //Horizontal Movement
        if (Input.GetKey(KeyCode.A))
        {
            movementDir += new Vector2(-1.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementDir += new Vector2(1.0f, 0.0f);
        }

        _rb.velocity = movementDir * (_playerSpeed);
    }

    // Receives player input and jumps accordingly
    void Jump()
    {
        if (Input.GetKey(KeyCode.W) && _isGrounded) {
            _rb.AddForce(transform.up * _playerJumpHeight, ForceMode2D.Impulse);
        }
    }

    // Reset player health to max
    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }

    // Inflict damage to player health
    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
    }

    // Add health to player health
    public void HealDamage(float heal)
    {
        _currentHealth += heal;
        if (_currentHealth > _maxHealth) { ResetHealth(); }
    }

    // Checks if player health is depleted
    public void CheckIfDead()
    {
        if (_currentHealth <= 0 && !_isDead) {
            _currentHealth = 0;
            Die();
        }
    }

    // Kill player
    void Die()
    {
        //Sets player status to dead and hides body
        _isDead = true;
        _player.SetActive(false);

        //Displays death message
        _deathMessage.gameObject.SetActive(true);

        //Stops everything
        Time.timeScale = 0.0f;
    }

    // Triggers player attack
    void Attack()
    {
        if (_weaponObjInHand == null)
        {
            //Trigger unarmed melee attack
        }
        else
        {
            //use switch statement to determine weapon and trigger appropriate attack
        }
    }

    // Collision Detection
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Terrain")
        {
            for (int i = 0; i < collision.contacts.Length; i++) {
                if (collision.contacts[i].normal.y > 0.5)
                {
                    _isGrounded = true;
                }
            }
        }
    }

    // Collision Detection
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Terrain") {
            _isGrounded = false;
        }
    }
}
