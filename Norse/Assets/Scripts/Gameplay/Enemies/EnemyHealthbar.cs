/* Script for enemy health bars
 * Programmer: Brandon Bunting
 * Date Created: 02/18/2022
 * Date Modified: 03/22/2022
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    //
    [SerializeField] Image mask;
    [SerializeField] GameObject enemyObj;

    Enemy enemyProperties;

    // Start is called before the first frame update
    void Start()
    {
        enemyProperties = enemyObj.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        float fillAmount = enemyProperties.GetHealth() / enemyProperties.GetMaxHealth();

        mask.fillAmount = fillAmount;
    }
}
