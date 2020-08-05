using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public static Action OnLose;
    public static Action OnWin;

    [SerializeField] private int _score = 0;

    public int Score { 
        get => _score;
        set
        {
            _score = value;

            if (_score >= LevelController.Instance.MaxScore)
            {
                OnWin();
            }
            UIGameManager.OnUpdateUI();
        }
    }

    public static bool IsPause { get; private set; }

    private void Start()
    {
        OnLose += UIGameManager.Instance.ShowLoseScreen;
        OnLose += Pause;
        OnWin += UIGameManager.Instance.ShowWinScreen;
        OnWin += Pause;
        OnWin += LevelComplete;
    }

    private void LevelComplete()
    {
        SaveManager.COMPLETED_LEVELS.Add(SaveManager.CURRENT_LEVEL);
        SaveManager.UNLOCKED_LEVELS.Add(SaveManager.CURRENT_LEVEL + 1);
    }

    private void Pause()
    {
        IsPause = true;
        Time.timeScale = 0;
    }

    private void Unpause()
    {
        IsPause = false;
        Time.timeScale = 1;
    }

    public void NextLevel()
    {
        SaveManager.CURRENT_LEVEL += 1;
        Unpause();
        SaveManager.SaveData();
        SceneManager.LoadScene(1);
    }

    public void RepeatLevel()
    {
        Unpause();
        SaveManager.SaveData();
        SceneManager.LoadScene(1);
    }

    public void GoToMenu()
    {
        Unpause();
        SaveManager.SaveData();
        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        OnLose -= UIGameManager.Instance.ShowLoseScreen;
        OnLose -= Pause;
        OnWin -= UIGameManager.Instance.ShowWinScreen;
        OnWin -= Pause;
        OnWin -= LevelComplete;
    }
}
