using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameManager : MonoSingleton<UIGameManager>
{
    public static Action OnUpdateUI;

    [Header("Ship lifes"), Space(10)]
    [SerializeField] private GameObject _winScreen = null;
    [SerializeField] private GameObject _loseScreen = null;

    [Header("Progress level"), Space(10)]
    [SerializeField] private Slider _sliderLevelProgress = null;
    [SerializeField] private TextMeshProUGUI _textScore = null;
    [SerializeField] private TextMeshProUGUI _textMaxScore = null;

    [Header("Ship lifes"), Space(10)]
    [SerializeField] private Image[] _lifeImages = new Image[3];

    public Image[] LifeImages { get => _lifeImages; set => _lifeImages = value; }

    public void Start()
    {
        OnUpdateUI += UpdatePlayerLifes;
        OnUpdateUI += UpdateScore;

        OnUpdateUI();
    }

    private void UpdatePlayerLifes()
    {
        foreach(Image img in LifeImages)
        {
            img.gameObject.SetActive(false);
        }

        for (int i =0; i < LevelController.Instance.PlayerShip.Lifes; i++)
        {
            LifeImages[i].gameObject.SetActive(true);
        }
    }

    private void UpdateScore()
    {
        _sliderLevelProgress.maxValue = LevelController.Instance.MaxScore;
        _sliderLevelProgress.value = GameManager.Instance.Score;

        _textScore.text = _sliderLevelProgress.value.ToString();
        _textMaxScore.text = _sliderLevelProgress.maxValue.ToString();
    }

    public void ShowWinScreen()
    {
        _winScreen.SetActive(true);
    }

    public void ShowLoseScreen()
    {
        _loseScreen.SetActive(true);
    }

    private void OnDestroy()
    {
        OnUpdateUI -= UpdatePlayerLifes;
        OnUpdateUI -= UpdateScore;
    }
}
