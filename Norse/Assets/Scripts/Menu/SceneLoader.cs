/* Script to load specified Scene
 * Programmer: Brandon Bunting
 * Date Created: 03/15/2022
 * Date Modified: 03/15/2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // 
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
