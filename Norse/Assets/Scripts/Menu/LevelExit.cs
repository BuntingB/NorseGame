/* Script for moving returning to the main menu
 * Programmer: Brandon Bunting
 * Date Created: 03/15/2022
 * Date Modified: 03/15/2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : Interactables
{
    //
    [SerializeField] SceneLoader _sceneLoader;
    [SerializeField] ScoreManager _scoreManager;

    public void ExitLevel()
    {
        if (ScoreManager._bestScore[ScoreManager.GetLevelIndex()] == 0 ||
            ScoreManager._score / ScoreManager._time > 
            ScoreManager._bestScore[ScoreManager.GetLevelIndex()] / ScoreManager._bestTime[ScoreManager.GetLevelIndex()]) 
        {
            _scoreManager.SetHighScore();
            Debug.Log("Highscore Updated");
        }
        _scoreManager.ResetScore();
        _sceneLoader.LoadLevel("MainMenu");
    }
}
