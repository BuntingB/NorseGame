/* Script for fading objects (mainly walls) in and out
 * Programmer: Brandon Bunting
 * Date Created: 03/22/2022
 * Date Modified: 03/31/2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    //
    bool _playerPresent = false;
    public float _fadeSpeed = 1f;
    public bool _onByDefault = true;

    private void Start()
    {
        if (!_onByDefault)
        {
            Fade(-1);
        }
    }

    //
    void FixedUpdate()
    {
        if ((_playerPresent && _onByDefault) || (!_playerPresent && !_onByDefault))
        {
            Fade(-1);
        }
        else if ((!_playerPresent && _onByDefault) || (_playerPresent && !_onByDefault))
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
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _playerPresent = false;
        }
    }
}
