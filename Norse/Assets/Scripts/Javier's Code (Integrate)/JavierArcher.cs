using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavierArcher : JavierEnemy
{

    Transform Atrans;

    bool _fleeing;
    bool counter;
    bool canMove;
    public GameObject BulletPrefab;
    public Transform BulletRespawn;
    public float bulletForce;
    float _attackTimer;


    public override void EnemyLife()
    {
        base.EnemyLife();

        if (CanAttack == true)
        {

            Attack();


        }
        FleePlayer();
        ArcherMovement();
    }

    void Awake()
    {
        Atrans = GetComponent<Transform>();
    }


    void Attack()
    {
        _attackTimer += Time.deltaTime;

        if (_attackTimer >= AttackRate)
        {

            Bullet();
            _attackTimer = 0;

        }


    }



    void Bullet()
    {

        GameObject SpawnedBullet = Instantiate(BulletPrefab, BulletRespawn.position, BulletRespawn.rotation);

        Vector3 direction = SpawnedBullet.transform.rotation * Vector3.right;
        SpawnedBullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletForce, ForceMode2D.Impulse);
        _attackTimer += Time.deltaTime;

        Destroy(SpawnedBullet, 3);


    }


    public void FleePlayer()
    {

        if (_realDistanceToPlayer.x < 20 && _realDistanceToPlayer.x > 0 && counter == false && _realDistanceToPlayer.y < 5 && _realDistanceToPlayer.y > -5)
        {
            _fleeing = true;
            EnemySpeed = -5;
            Atrans.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (_realDistanceToPlayer.x > -20 && _realDistanceToPlayer.x < 0 && counter == false && _realDistanceToPlayer.y < 5 && _realDistanceToPlayer.y > -5)
        {
            _fleeing = true;
            EnemySpeed = 5;
            Atrans.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {

            _fleeing = false;

        }


    }
    void ArcherMovement()
    {
        if (_fleeing == false)
        {
            if (_realDistanceToPlayer.x < 50 && _realDistanceToPlayer.x > 0 && _realDistanceToPlayer.y < 5 && _realDistanceToPlayer.y > -5) 
            {

                EnemySpeed = 10;
            }


            else if (_realDistanceToPlayer.x > -50 && _realDistanceToPlayer.x < 0 && _realDistanceToPlayer.y < 5 && _realDistanceToPlayer.y > -5)
            {

                EnemySpeed = -10;
            }

            else
            {
                EnemySpeed = 0;
            }
        }

        if (_realDistanceToPlayer.x > -21 && _realDistanceToPlayer.x < -20 && _realDistanceToPlayer.y < 5 && _realDistanceToPlayer.y > -5)
        {
            counter = true;
            EnemySpeed = 0;
        }
        else
        {
            counter = false;
        }

    }
}
