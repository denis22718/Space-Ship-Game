using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private GameObject _lockedState = null;
    [SerializeField] private GameObject _unlockedState = null;
    [SerializeField] private GameObject _completedState = null;

    private Button _button = null;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void UnlockLevel()
    {
        _button.interactable = true;
        _lockedState.SetActive(false);
        _unlockedState.SetActive(true);
    }
    public void LockLevel()
    {
        _button.interactable = false;
        _lockedState.SetActive(true);
        _completedState.SetActive(false);
        _unlockedState.SetActive(false);
    }
    public void CompleteLevel()
    {
        _button.interactable = true;
        _lockedState.SetActive(false);
        _completedState.SetActive(true);
        _unlockedState.SetActive(false);
    }
}
