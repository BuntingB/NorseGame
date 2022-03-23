using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavierThief : JavierEnemy
{
    Player player;
    float _positioningTime;
    float _thiefSpeed;
    bool countert;
    bool _swingPosition;
    bool _rightSide;
    bool _leftSide;
    public float DamageValue;

    GameObject playerobj;
    GameObject _backPosition;
    Vector2 _distToBack;
    float _realDistanceToBack;


    //public override void EnemyLife()
    //{
    //    base.EnemyLife();
    //    ThiefMovement();
    //    BackPosition();

    //    if (_swingPosition == true)
    //    {
    //        EnemySpeed = 0;

    //        Swing();
    //    }

    //}

    void Awake()
    {
        playerobj = GameObject.FindWithTag("Player");
        _backPosition = GameObject.FindWithTag("PlayerBack");
        player = playerobj.GetComponent<Player>();
    }

    void BackPosition()
    {

        _distToBack = _backPosition.transform.position - this.transform.position;

        _realDistanceToBack = _distToBack.x;

    }

    void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.GetComponent<Collider2D>().tag == "PlayerBack")
        {

            _positioningTime += Time.deltaTime;

            if (_positioningTime >= 0.2)
            {
                _swingPosition = true;
            }

        }


    }
    void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.GetComponent<Collider2D>().tag == "PlayerBack")
        {
            countert = false;
            _positioningTime = 0;
            _swingPosition = false;

        }


    }

    //void ThiefMovement()
    //{

    //    if (_realDistanceToPlayer.x < 50 && _realDistanceToPlayer.x > 0 && _realDistanceToPlayer.y < 5 && _realDistanceToPlayer.y > -5)
    //    {
    //        this.transform.rotation = Quaternion.Euler(0, 0, 0);
    //        _leftSide = false;
    //        _rightSide = true;

    //        if (_realDistanceToPlayer.x < 30 && _realDistanceToPlayer.x > 0 && _realDistanceToPlayer.y < 5 && _realDistanceToPlayer.y > -5)
    //        {
    //            EnemySpeed = -5;
    //        }

    //        if (player._LeftBack == true && _rightSide == true)
    //        {
    //            EnemySpeed = 10;

    //        }


    //    }

    //    else if (_realDistanceToPlayer.x > -50 && _realDistanceToPlayer.x < 0 && _realDistanceToPlayer.y < 5 && _realDistanceToPlayer.y > -5)
    //    {

    //        this.transform.rotation = Quaternion.Euler(0, 180, 0);
    //        _rightSide = false;
    //        _leftSide = true;

    //        if (_realDistanceToPlayer.x > -30 && _realDistanceToPlayer.x < 0 && _realDistanceToPlayer.y < 5 && _realDistanceToPlayer.y > -5)
    //        {
    //            EnemySpeed = -10;
    //        }

    //        if (player._LeftBack == true && _leftSide == true)
    //        {
    //            EnemySpeed = 5;
    //        }



    //    }

    //    else
    //    {
    //        EnemySpeed = 0;
    //    }


    //}

    //void Swing()
    //{
    //    PLayer player = gameObject.GetComponent<PLayer>();
    //    // player.TakeDamage(DamageValue);
    //}

}
