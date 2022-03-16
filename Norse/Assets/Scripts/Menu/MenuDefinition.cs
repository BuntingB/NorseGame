/* Script for menu functionality
 * Programmer: Brandon Bunting
 * Date Created: 03/15/2022
 * Date Modified: 03/15/2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MenuType
{
    HORIZONTAL,
    VERTICAL
}

public class MenuDefinition : MonoBehaviour
{
    //
    public MenuType _menuType = MenuType.HORIZONTAL;
    public AudioClip _menuMusic;
    public bool _continuePrevMusic = false;
    public List<GameObject> _menuButtonObjects = new List<GameObject>();

    List<ButtonDefinition> _menuButtonDefinitions = new List<ButtonDefinition>();
    List<Button> _menuButtons = new List<Button>();
    List<Animator> _menuAnimators = new List<Animator>();

    //
    void OnEnable()
    {
        //Searches and grabs components
        for (int i = 0; i < _menuButtonObjects.Count; i++)
        {
            _menuButtonDefinitions.Add(_menuButtonObjects[i].GetComponent<ButtonDefinition>());
            _menuButtons.Add(_menuButtonObjects[i].GetComponent<Button>());

            //Grab out animator if it exists
            Animator temp = null;
            _menuButtonObjects[i].TryGetComponent(out temp);

            //If no animator, then null
            _menuAnimators.Add(temp);
        }
        
    }

    //
    public MenuType GetMenuType()
    {
        return _menuType;
    }

    //
    public int GetButtonCount()
    {
        return _menuButtonObjects.Count;
    }

    //
    public List<ButtonDefinition> GetButtonDefinitions()
    {
        return _menuButtonDefinitions;
    }

    //
    public List<Button> GetButtons()
    {
        return _menuButtons;
    }

    //
    public List<Animator> GetAnimators()
    {
        return _menuAnimators;
    }
}
