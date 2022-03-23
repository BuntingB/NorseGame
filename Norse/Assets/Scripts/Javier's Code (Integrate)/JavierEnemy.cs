using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavierEnemy : MonoBehaviour
{

    Rigidbody2D _rb;
    Transform trans;
    GameObject enemyObj;

    public bool CanAttack;

    public float AttackDistance;
    public float EnemySpeed;
    public float AttackRate;
    public float MaxHealth;
    public float xDir;
    public Vector2 _realDistanceToPlayer;

    float currentHealth;

    GameObject _playerPosition;
    Vector2 _distanceToPlayer;


    void Start()
    {
        //enemyProperties = enemyObj.GetComponent<Enemy>();
        _playerPosition = GameObject.FindWithTag("Player");
        trans = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        currentHealth = MaxHealth;

    }


    void Update()
    {

        Movement();
        PlayerPosition();
        EnemyLife();
        AttackDist();
    }

    void PlayerPosition()
    {

        _distanceToPlayer = _playerPosition.transform.position - trans.transform.position;

        _realDistanceToPlayer = _distanceToPlayer;

    }

    public virtual void EnemyLife()
    {
        KillIfDead();

        //float fillAmount = enemyProperties.currentHealth / enemyProperties.MaxHealth;

        //mask.fillAmount = fillAmount;
    }

    void KillIfDead()
    {
        if (IsDead())
        {
            Destroy(gameObject);
        }
    }

    bool IsDead()
    {
        if (currentHealth <= 0.0)
        {
            return true;

        }
        else
        {
            return false;
        }
    }


    public void TakeDamage(float Damage)
    {
        currentHealth = -Damage;

    }

    public void AttackDist()
    {

        if (_realDistanceToPlayer.x < AttackDistance && _realDistanceToPlayer.x > 0 && _realDistanceToPlayer.y < 5 && _realDistanceToPlayer.y > -5) //30
        {
            trans.rotation = Quaternion.Euler(0, 0, 0);
            CanAttack = true;
        }
        else if (_realDistanceToPlayer.x > -AttackDistance && _realDistanceToPlayer.x < 0 && _realDistanceToPlayer.y < 5 && _realDistanceToPlayer.y > -5) //-30
        {
            trans.rotation = Quaternion.Euler(0, 180, 0);
            CanAttack = true;
        }
        else
        {
            CanAttack = false;
        }


    }

    void Movement()
    {
        Vector2 enemyVelocity = new Vector2(EnemySpeed, _rb.velocity.y);
        _rb.velocity = enemyVelocity;
        EnemySpeed *= xDir * Time.fixedDeltaTime;
    }



}
