/* Script for the Thief enemy
 * Programmer: Brandon Bunting
 * Date Created: 03/22/2022
 * Date Modified: 03/23/2022
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : MonoBehaviour
{
    //
    public Transform _playerPos;
    private Enemy _enemy;
    private Rigidbody2D _rb;
    public Renderer _renderer;
    public Weapon _weaponObjInHand;
    public Vector2 _attackRange = new Vector2(5, 5);
    float _attackModifier = 0.5f;
    public float _speed = 3.5f;
    public float _fleeDistance = 1.75f;

    // Called when object is enabled
    void OnEnable()
    {
        _playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_renderer.isVisible)
        {
            Move();
            Attack();
        }
    }

    //
    void Move()
    {
        float xDir = 0;

        if (_playerPos.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            xDir = 1f;
        }
        else if (_playerPos.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            xDir = -1f;
        }

        float playerDistance = Mathf.Abs(_playerPos.position.x - transform.position.x);

        if (playerDistance > _fleeDistance)
        {
            _rb.velocity = new Vector2(xDir * _speed, _rb.velocity.y);
        }
        else if (playerDistance < _fleeDistance && playerDistance > 1f)
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }
        else
        {
            _rb.velocity = new Vector2(-xDir * _speed, _rb.velocity.y);
        }
    }

    //
    void Attack()
    {
        if (Mathf.Abs(_playerPos.position.x - transform.position.x) < _attackRange.x &&
            Mathf.Abs(_playerPos.position.y - transform.position.y) < _attackRange.y)
        {
            _weaponObjInHand.Attack(_attackModifier);
        }
    }
}
