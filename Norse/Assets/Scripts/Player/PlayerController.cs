/* Script to control the player object in "Project Norse"
 * Programmer: Brandon Bunting
 * Date Created: 01/28/2022
 * Date Modified: 02/21/2022
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Variables
    #region Player body & stats
    //Player body
    public GameObject _playerObject;
    public Rigidbody2D _rb;
    public Transform _trans;
    private Player _player;

    //Player stats
    [Range(1.0f, 10.0f)]
    public float _playerSpeed = 4.0f;
    [Range(1.0f, 10.0f)]
    public float _playerJumpHeight = 5.0f;
    private bool _isGrounded = false;
    private bool _isClimbing = false;

    //Class modifiers
    private float _damageRes = 0f;
    private float _attackModifer = 1f;
    private float _speedModifier = 1f;
    #endregion

    #region Combat-related
    //Player death
    public bool _isDead = false;
    public Text _deathMessage;

    //Player weapons
    //3 standard weapons, possibly some specialty weapons too
    public List<GameObject> _weaponPrefabs;
    public GameObject _punchHitbox;
    public GameObject _weaponParent;
    public bool _isHoldingWeapon = false;
    public Weapon _weaponObjInHand = null;
    private bool _attacking = false;
    private GameObject weapon;
    private float _timeToNextAttack = 0f;
    private float _cooldown = 0.2f;
    #endregion

    //
    private bool _interactablePresent = false;
    private bool _isInteracting = false;

    // Awake is called as the script loads
    private void Awake()
    {
        #region Startup
        if (_player == null)
        {
            _player = GetComponent<Player>();
        }
        if (_rb == null) {
            _rb = GetComponent<Rigidbody2D>();
        }
        if (_trans == null) {
            _trans = GetComponent<Transform>();
        }
        #endregion
    }

    // Update is called once per frame
    private void Update()
    {
        #region Core Checks
        // Attack
        if (_attacking) {
            Attack();
        }

        //Interact with other objects
        if (Input.GetKeyDown(KeyCode.E))
        {
            _isInteracting = true;
        }
        else
        {
            _isInteracting = false;
        }

        //Check for weapon-related input
        WeaponInput();
        #endregion
    }

    //
    private void FixedUpdate()
    {
        MovePlayer(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Climb();
    }

    // Receives player input and controls player accordingly
    private void MovePlayer(float xDir, float yDir)
    {
        #region Movement & Jumping
        if (_isGrounded && yDir == 1)
        {
            _rb.AddForce(new Vector2(0f, _playerJumpHeight), ForceMode2D.Impulse);
            _isGrounded = false;
        }

        xDir *= _playerSpeed * 100 * Time.fixedDeltaTime;

        Vector2 playerVelocity = new Vector2(xDir, _rb.velocity.y);

        //Player Rotation for Movement
        if (playerVelocity.x < 0)
        {
            _trans.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (playerVelocity.x > 0)
        {
            _trans.rotation = Quaternion.Euler(0, 0, 0);
        }

        _rb.velocity = playerVelocity;
        #endregion
    }

    //
    private void Climb()
    {
        #region Climbing
        if (_isClimbing)
        {
            if (Input.GetAxisRaw("Vertical") != 0) 
            {
                _rb.velocity = new Vector2(_rb.velocity.x, Input.GetAxisRaw("Vertical") * 2f);
            }
            else
            {
                _rb.velocity = new Vector2(_rb.velocity.x, 0f);
            }
        }
        #endregion
    }

    // Kill player
    public void Die()
    {
        #region Death Handling
        //Sets player status to dead and hides body
        _isDead = true;
        _playerObject.SetActive(false);

        //Displays death message
        _deathMessage.gameObject.SetActive(true);

        //Stops everything
        Time.timeScale = 0.0f;
        #endregion
    }

    // Controls weapon-related input
    private void WeaponInput()
    {
        #region Weapons
        //Attacking check, will put in own method
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _attacking = true;
        }
        else
        {
            _attacking = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (_weaponObjInHand != null)
            {
                DequipWeapon();
            }
            EquipWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (_weaponObjInHand != null)
            {
                DequipWeapon();
            }
            EquipWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (_weaponObjInHand != null)
            {
                DequipWeapon();
            }
            EquipWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (_weaponObjInHand != null)
            {
                DequipWeapon();
            }
        }
        #endregion
    }

    // Equips weapon model and sets weapon type to that model's type
    #region Equip/Dequip Weapons
    private void EquipWeapon(int weaponNum)
    {
        weapon = Instantiate(_weaponPrefabs[weaponNum], _weaponParent.transform);
        _weaponObjInHand = weapon.GetComponent<Weapon>();
    }

    // Dequips weapon
    private void DequipWeapon()
    {
        Destroy(weapon);
        _weaponObjInHand = null;
    }
    #endregion

    // Triggers player attack
    private void Attack()
    {
        #region Weapon Checks & Attack
        if (_weaponObjInHand == null)
        {
            if (CanAttack(_timeToNextAttack)) {
                //Trigger unarmed melee attack
                GameObject punch = Instantiate(_punchHitbox, _weaponParent.transform);
                punch.GetComponent<WeaponHitbox>().SetDamage(15f, _attackModifer);

                Destroy(punch, 0.2f);
                _timeToNextAttack = Time.realtimeSinceStartup + _cooldown;
            }
        }
        else
        {
            //Override to use Attack() in inherited Weapon class on weapon
            _weaponObjInHand.Attack(_attackModifer);

        }
        #endregion
    }

    //
    public bool CanAttack(float _timeToNextAttack)
    {
        if (Time.realtimeSinceStartup >= _timeToNextAttack)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Collision Detection
    #region Collision/Triggers
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Terrain")
        {
            for (int i = 0; i < collision.contacts.Length; i++)
            {
                if (collision.contacts[i].normal.y > 0.5)
                {
                    _isGrounded = true;
                }
                if (collision.contacts[i].normal.x < 1.1)
                {
                    _isClimbing = true;
                }
            }
        }
    }

    // Collision Detection
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Terrain") {
            _isGrounded = false;
            if (_isClimbing)
            {
                _isGrounded = true;
            }
            _isClimbing = false;
        }
        if (collision.collider.tag == "EnemyArrow")
        {
            float damage = collision.gameObject.GetComponent<Arrow>().GetDamage();
            damage -= damage * _damageRes;
            _player.TakeDamage(damage);
        }
    }

    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            _interactablePresent = true;
        }
        if (collision.tag == "EnemySword" || collision.tag == "EnemyMelee")
        {
            float damage = collision.gameObject.GetComponent<WeaponHitbox>().GetDamage();
            damage -= damage * _damageRes;
            _player.TakeDamage(damage);
        }
    }

    // Possibly unnecessary
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Interactable" && _isInteracting)
        {
            //collision.gameObject. Whatever function in the object
            Debug.Log("Interacting with Object");
        }
    }

    //
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            _interactablePresent = false;
        }
    }
    #endregion
}
