/* Script to manage several menus
 * Programmer: Brandon Bunting
 * Date Created: 03/15/2022
 * Date Modified: 03/15/2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    //
    public GameObject _activeMenu;
    public AudioSource _backgroundAudio;

    public List<KeyCode> _increaseVert; //Up
    public List<KeyCode> _decreaseVert; //Down
    public List<KeyCode> _increaseHoriz; //Right
    public List<KeyCode> _decreaseHoriz; //Left
    public List<KeyCode> _confirmButtons;

    MenuDefinition _activeMenuDefinition;
    int _activeButton = 0;

    // Runs when object is enabled
    void OnEnable()
    {
        _activeButton = 0; //Reset to index zero

        //Update active menu definition at start
        UpdateActiveMenuDefinition();
    }

    // Runs when object is disabled
    void OnDisable()
    {
        _activeMenu = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (_activeMenu != null)
        {
            switch (_activeMenuDefinition.GetMenuType())
            {
                case MenuType.HORIZONTAL:
                    MenuInput(_increaseHoriz, _decreaseHoriz);
                    break;
                case MenuType.VERTICAL:
                    MenuInput(_increaseVert, _decreaseVert);
                    break;
            }
        }
    }

    // Controls menu according to input
    void MenuInput(List<KeyCode> increase, List<KeyCode> decrease)
    {
        //Stops menu from running during gameplay
        bool temp = TryGetComponent<PauseMenu>(out PauseMenu pauseMenu);
        if (temp && !pauseMenu.GetIsActive())
        {
            return;
        }

        int newActive = _activeButton;

        for (int i = 0; i < increase.Count; i++)
        {
            if (Input.GetKeyDown(increase[i]))
            {
                newActive = SwitchCurrentButton(1);
            }
        }

        for (int i = 0; i < decrease.Count; i++)
        {
            if (Input.GetKeyDown(decrease[i]))
            {
                newActive = SwitchCurrentButton(-1);
            }
        }

        for (int i = 0; i < _confirmButtons.Count; i++)
        {
            if (Input.GetKeyDown(_confirmButtons[i]))
            {
                ClickCurrentButton();
            }
        }
        _activeButton = newActive;
    }

    //
    int SwitchCurrentButton(int increment)
    {
        if (!_activeMenuDefinition.GetButtonDefinitions()[_activeButton].GetDisableControls())
        {
            int newActive = Utility.WrapAround(_activeMenuDefinition.GetButtonCount(), _activeButton, increment);

            _activeMenuDefinition.GetButtonDefinitions()[_activeButton].SwappedOff();
            _activeMenuDefinition.GetButtonDefinitions()[newActive].SwappedTo();

            return newActive;
        }
        return _activeButton;
    }

    //
    void ClickCurrentButton()
    {
        if (!_activeMenuDefinition.GetButtonDefinitions()[_activeButton].GetDisableControls())
        {
            StartCoroutine(_activeMenuDefinition.GetButtonDefinitions()[_activeButton].ClickButton());
        }
    }

    //
    public void UpdateActiveMenuDefinition()
    {
        _activeMenuDefinition = _activeMenu.GetComponent<MenuDefinition>();

        if (_activeMenuDefinition._menuMusic != null)
        {
            _backgroundAudio.clip = _activeMenuDefinition._menuMusic;
            _backgroundAudio.Play();
        }
        else if (!_activeMenuDefinition._continuePrevMusic)
        {
            _backgroundAudio.Stop();
        }
    }

    //
    public void SetActiveMenu(GameObject activeMenu)
    {
        //Set active menu
        _activeMenu = activeMenu;

        _activeMenuDefinition.GetButtonDefinitions()[_activeButton].SwappedOff();
        _activeMenuDefinition.GetButtonDefinitions()[0].SwappedTo();
        _activeButton = 0;

        //Make sure to update definition
        UpdateActiveMenuDefinition();
    }
}
