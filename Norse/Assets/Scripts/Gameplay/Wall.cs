using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    //
    bool _playerPresent = false;
    public float _fadeSpeed = 1f;

    //
    void FixedUpdate()
    {
        if (_playerPresent)
        {
            Fade(-1);
        }
        else
        {
            Fade(1);
        }
    }

    void Fade(int dir)
    {
        Color wallColour = gameObject.GetComponent<Renderer>().material.color;

        if ((_playerPresent && wallColour.a > 0) || (!_playerPresent && wallColour.a < 1)) 
        {
            float fadeVal = wallColour.a + (dir * (_fadeSpeed * Time.fixedDeltaTime));

            wallColour = new Color(wallColour.r, wallColour.g, wallColour.b, fadeVal);
            gameObject.GetComponent<Renderer>().material.color = wallColour;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _playerPresent = true;
            Debug.Log("Player Present");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _playerPresent = false;
            Debug.Log("Player Not Present");
        }
    }
}
