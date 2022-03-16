/* Script to control the player object's healthbar
 * Programmer: Brandon Bunting
 * Date Created: 01/28/2022
 * Date Modified: 02/19/2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    //
    [SerializeField] Image mask;
    [SerializeField] GameObject playerObj;

    Player playerProperties;

    // Start is called before the first frame update
    void Start()
    {
        playerProperties = playerObj.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        float fillAmount = playerProperties.GetHealth() / playerProperties.GetMaxHealth();

        mask.fillAmount = fillAmount;
    }
}
