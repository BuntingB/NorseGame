/* Script to hold utility functions
 * Programmer: Brandon Bunting
 * Date Created: 03/15/2022
 * Date Modified: 03/15/2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    //Static reference to player object
    static GameObject _player;
    public static bool _debug = false;

    //
    public static GameObject GetPlayerObject()
    {
        //If we don't have player object reference
        if (_player == null)
        {
            //Find player object
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        return _player;
    }

    //
    public static bool GetPause()
    {
        bool temp = Time.timeScale == 0f ? true : false;
        return temp;
    }

    //Increment must be -1 or 1
    public static int WrapAround(int max, int current, int increment, int min=0)
    {
        int temp = current + increment;

        if (temp >= max)
        {
            temp = min; //Wrap around to first value
        }
        else if (temp < min)
        {
            temp = max - 1; //Wrap around to last value
        }

        return temp;
    }
}
