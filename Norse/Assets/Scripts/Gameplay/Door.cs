/* Script to open doors
 * Programmer: Brandon Bunting
 * Date Created: 03/21/2022
 * Date Modified: 03/21/2022
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //
    public GameObject _closedDoorObj;
    public GameObject _openDoorObj;

    public static float _timeToClose = 1.0f;

    bool _isOpen;

    //
    public void OpenDoor()
    {
        if (!_isOpen) 
        {
            _closedDoorObj.SetActive(false);
            _openDoorObj.SetActive(true);
            _isOpen = true;

            StartCoroutine(CloseDoor());
        }
    }

    //
    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(_timeToClose);
        
        _closedDoorObj.SetActive(true);
        _openDoorObj.SetActive(false);
        _isOpen = false;
    }
}
