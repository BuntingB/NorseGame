using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavierWarrior : JavierEnemy
{
    Animator Anima;

    bool _animationPlayed;
    float _animationPause;
    float _attackTimer;

    public override void EnemyLife()
    {
        base.EnemyLife();
        Anima = GetComponent<Animator>();


        Movement();
        if (CanAttack == true)
        {

            Attack();

        }
        if (CanAttack == false)
        {

        }

    }

    // Start is called before the first frame update

    void Awake()
    {

    }

    // Update is called once per frame

    void Movement()
    {
        if (_realDistanceToPlayer.x < 50 && _realDistanceToPlayer.x > 10 && _realDistanceToPlayer.y < 5 && _realDistanceToPlayer.y > -5)
        {

            EnemySpeed = 10;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }


        else if (_realDistanceToPlayer.x > -50 && _realDistanceToPlayer.x < -10 && _realDistanceToPlayer.y < 5 && _realDistanceToPlayer.y > -5)
        {

            EnemySpeed = -10;
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        else
        {
            EnemySpeed = 0;
        }

    }
    void Attack()
    {


    }


}
