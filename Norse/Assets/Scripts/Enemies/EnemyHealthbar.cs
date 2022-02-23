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
