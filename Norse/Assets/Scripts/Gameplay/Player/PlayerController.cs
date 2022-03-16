/* Script to control the player object in "Project Norse"
 * Programmer: Brandon Bunting
 * Date Created: 01/28/2022
 * Date Modified: 03/15/2022
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Variables
    #region Player body & stat variables
    //Player body
    public GameObject _playerObject;
    public Rigidbody2D _rb;
    public Transform _trans;
    private Player _player;

    //Player stats
    [Range(1.0f, 10.0f)]
    public float _playerSpeed = 3.0f;
    [Range(1.0f, 10.0f)]
    public float _playerJumpHeight = 5.0f;
    private bool _isGrounded = false;
    private bool _isClimbing = false;
    private float _accelerationSpeed;
    private float _decelerationSpeed;

    private bool _controlsDisabled = false;

    //Class modifiers. Indexes are: 0 = unarmed, 1 = thief, 2 = warrior, 3 = archer
    private float[] _damageRes = { 0f, 0.3f, 0.6f, 0.4f }; // 0 == 0% resistance, 1 == 100% resistance
    private float[] _attackModifer = { 1f, 1.25f, 1.75f, 1.5f }; // Multiplier (i.e., 1.25f == damage * 1.25)
    private float[] _speedModifier = { 1f, 1.25f, 0.75f, 1f }; // ^
    #endregion

    #region Combat-related variables
    //Player death
    public bool _isDead = false;
    public Text _deathMessage;

    //Player weapons
    //3 standard weapons, possibly some specialty weapons too
    public List<GameObject> _weaponPrefabs;
    public List<GameObject> _weaponDropPrefabs;
    public GameObject _punchHitbox;
    public GameObject _weaponParent;
    public GameObject _itemParent;
    public GameObject _ammoDisplay;
    public bool _isHoldingWeapon = false;
    public Weapon _weaponObjInHand = null;
    private int _currentPrefab;
    private bool _attacking = false;
    private GameObject weapon;
    private float _timeToNextAttack = 0f;
    private float _cooldown = 0.2f;
    #endregion

    #region Item interaction variables
    //
    private bool _isInteracting = false;

    private Interactables item;
    #endregion

    // Awake is called as the script loads
    private void Awake()
    {
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
    }

    // Update is called once per frame
    private void Update()
    {
        //Interact with other objects
        Interact();

        //Check for weapon-related input
        WeaponInput();

        //Updates Ammo Display
        UpdateAmmoDisplay();
    }

    // Fixed Update loop
    private void FixedUpdate()
    {
        MovePlayer(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Climb();
    }

    // Receives player input and controls player accordingly
    private void MovePlayer(float xDir, float yDir)
    {
        if (!_controlsDisabled)
        {
            if (_isGrounded && yDir == 1)
            {
                _rb.AddForce(new Vector2(0f, _playerJumpHeight * _speedModifier[_currentPrefab + 1]), ForceMode2D.Impulse);
                _isGrounded = false;
            }

            xDir *= _playerSpeed * _speedModifier[_currentPrefab + 1] * 100 * Time.fixedDeltaTime;

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
        }
    }

    // Receives player input for climbing and controls player accordingly
    private void Climb()
    {
        if (_isClimbing && !_controlsDisabled)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, Input.GetAxisRaw("Vertical") * 4f);
        }
    }

    // Kill player, display death message, and stop time
    public void Die()
    {
        //Sets player status to dead and hides body
        _isDead = true;
        _playerObject.SetActive(false);

        //Displays death message
        _deathMessage.gameObject.SetActive(true);

        //Stops everything
        Time.timeScale = 0.0f;
    }

    // Controls weapon-related input
    private void WeaponInput()
    {
        if (!_controlsDisabled)
        {
            //Attacking check, will put in own method
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _attacking = true;
            }
            else
            {
                _attacking = false;
            }

            if (_attacking)
            {
                Attack();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (_weaponObjInHand != null)
                {
                    DequipWeapon();
                }
            }
        }
    }

    // Handles player interaction with objects
    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E) && !_controlsDisabled)
        {
            _isInteracting = true;
        }
        else
        {
            _isInteracting = false;
        }

        if (_isInteracting && item != null)
        {
            int ammo = item.GetAmmo();
            switch (item.GetItemType())
            {
                case "Dagger":
                    EquipWeapon(0,ammo);
                    break;
                case "Sword":
                    EquipWeapon(1,ammo);
                    break;
                case "Bow":
                    EquipWeapon(2,ammo);
                    break;
            }
            item.DestroyOnEquip();
        }
    }

    // Equips weapon model and sets weapon type to that model's type
    #region Equip/Dequip Weapons
    private void EquipWeapon(int weaponNum, int ammo)
    {
        if (_weaponObjInHand != null)
        {
            DequipWeapon();
        }
        weapon = Instantiate(_weaponPrefabs[weaponNum], _weaponParent.transform);
        _weaponObjInHand = weapon.GetComponent<Weapon>();
        _weaponObjInHand.SetAmmo(ammo);
        _currentPrefab = weaponNum;
    }

    // Dequips weapon
    private void DequipWeapon()
    {
        Destroy(weapon);
        GameObject droppedItem = Instantiate(_weaponDropPrefabs[_currentPrefab], transform.position, transform.rotation, _itemParent.transform);
        droppedItem.GetComponentInChildren<Interactables>().SetItemType(_weaponObjInHand.GetWeaponType());
        droppedItem.GetComponentInChildren<Interactables>().SetAmmo(_weaponObjInHand.GetAmmo());
        droppedItem.GetComponent<Rigidbody2D>().velocity += new Vector2(1,0);
        _weaponObjInHand = null;
        _currentPrefab = -1;
        Destroy(droppedItem, 15f);
    }
    #endregion

    // Triggers player attack
    private void Attack()
    {
        if (!_controlsDisabled)
        {
            if (_weaponObjInHand == null)
            {
                if (CanAttack(_timeToNextAttack))
                {
                    //Trigger unarmed melee attack
                    GameObject punch = Instantiate(_punchHitbox, _weaponParent.transform);
                    punch.GetComponent<WeaponHitbox>().SetDamage(15f, _attackModifer[0]);

                    Destroy(punch, 0.2f);
                    _timeToNextAttack = Time.realtimeSinceStartup + _cooldown;
                }
            }
            else
            {
                //Override to use Attack() in inherited Weapon class on weapon
                _weaponObjInHand.Attack(_attackModifer[_currentPrefab + 1]);

            }
        }
    }

    // Updates Ammo Display
    private void UpdateAmmoDisplay()
    {
        if (_weaponObjInHand == null) 
        {
            _ammoDisplay.SetActive(false);
            return;
        }

        if (_weaponObjInHand.IsAmmoUsed())
        {
            _ammoDisplay.SetActive(true);
            Text ammoCount = _ammoDisplay.GetComponent<Text>();
            ammoCount.text = _weaponObjInHand.GetAmmo().ToString();
        }
        else
        {
            _ammoDisplay.SetActive(false);
        }
    }

    // Checks if player can use the punch attack
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

    public void DisableControls(bool controlsDisabled)
    {
        _controlsDisabled = controlsDisabled;
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
            damage -= damage * _damageRes[_currentPrefab + 1];
            _player.TakeDamage(damage);
        }
    }

    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            item = collision.gameObject.GetComponent<Interactables>();
        }
        if (collision.tag == "EnemySword" || collision.tag == "EnemyMelee")
        {
            float damage = collision.gameObject.GetComponent<WeaponHitbox>().GetDamage();
            damage -= damage * _damageRes[_currentPrefab + 1];
            _player.TakeDamage(damage);
        }
    }

    //
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            item = null;
        }
    }
    #endregion
}
