/* Script to hold, set, and get item type for interactables
 * Programmer: Brandon Bunting
 * Date Created: 02/27/2022
 * Date Modified: 02/28/2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    // Have these variables be controlled by a script, rather than being public
    public string _itemType = null;
    public bool _permanentItem = false;
    private int _ammo = 15;

    //
    public void SetItemType(string itemType)
    {
        _itemType = itemType;
    }

    //
    public string GetItemType()
    {
        return _itemType;
    }

    //
    public void DestroyOnEquip()
    {
        if (!_permanentItem)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    public void SetAmmo(int ammo)
    {
        _ammo = ammo;
    }

    public int GetAmmo()
    {
        return _ammo;
    }
}
