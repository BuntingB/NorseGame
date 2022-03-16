/* Script to pause the game
 * Programmer: Brandon Bunting
 * Date Created: 03/15/2022
 * Date Modified: 03/15/2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //
    public MenuController _menuController;
    public GameObject _pauseMenu;
    bool _isPaused = false;

    //
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
                MenuUnpause();
            }
            else
            {
                MenuPause();
            }
        }
    }

    //
    public bool GetIsActive()
    {
        if (_menuController._activeMenu == _pauseMenu && Utility.GetPause())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //
    void MenuPause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
        Utility.GetPlayerObject().GetComponent<PlayerController>().DisableControls(true);
    }

    public void MenuUnpause()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
        Utility.GetPlayerObject().GetComponent<PlayerController>().DisableControls(false);
    }
}
