/* Script to manage the player's score
 * Programmer: Brandon Bunting
 * Date Created: 03/31/2022
 * Date Modified: 03/31/2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    //
    public Text _scoreObj;
    public Text _timeObj;
    public ButtonDefinition _button;

    //
    public static int _score = 0;
    public static float _time = 0;

    public static int[] _bestScore = new int[3];
    public static float[] _bestTime = new float[3];

    public bool _isStatic = false;
    public int _highScoreIndex;

    //
    void Update()
    {
        if (!_isStatic)
        {
            UpdateTime();
            UpdateDisplay();
        }
        else
        {
            DisplayHighScore(_highScoreIndex);
            if (_button != null)
            {
                _scoreObj.color = _button._selected ? _button._selectedTint : _button._unselectedTint;
                _timeObj.color = _button._selected ? _button._selectedTint : _button._unselectedTint;
            }
        }
    }

    // Updates Text objects
    void UpdateDisplay()
    {
        _scoreObj.text = "Score: "+ _score.ToString();
        _timeObj.text = "Time: "+  System.Decimal.Round((decimal)_time,2).ToString();
    }

    // Displays High Score
    void DisplayHighScore(int index)
    {
        _scoreObj.text = "Score: " + _bestScore[index].ToString();
        _timeObj.text = "Time: " + System.Decimal.Round((decimal)_bestTime[index], 2).ToString();
    }

    // Changes current score by value
    public void AddScore(int score)
    {
        _score += score;
    }

    // Updates current time to time since level was loaded
    void UpdateTime()
    {
        _time = Time.timeSinceLevelLoad;
    }

    // Resets current score and time
    public void ResetScore()
    {
        _score = 0;
        _time = 0;
    }

    // Sets current score as high score
    public void SetHighScore()
    {
        int i = GetLevelIndex();
        if (i != -1)
        {
            _bestScore[i] = _score;
            _bestTime[i] = _time;
        }
    }

    //
    public static int GetLevelIndex()
    {
        int i = -1;
        switch (SceneManager.GetActiveScene().name)
        {
            case "Level1":
                i = 0;
                break;
            case "Level2":
                i = 1;
                break;
            case "Level3":
                i = 2;
                break;
        }
        return i;
    }
}
