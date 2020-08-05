using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuManager : MonoSingleton<UIMenuManager>
{
    [SerializeField] private LevelButton[] _levelButtons = null;

    private void Start()
    {
        for (int i = 1; i < _levelButtons.Length; i++)
        {
            _levelButtons[i].LockLevel();
        }

        for (int i = 0; i < SaveManager.UNLOCKED_LEVELS.Count; i++)
        {
            if (SaveManager.UNLOCKED_LEVELS[i] < _levelButtons.Length)
                _levelButtons[SaveManager.UNLOCKED_LEVELS[i]].UnlockLevel();
        }

        for (int i = 0; i < SaveManager.COMPLETED_LEVELS.Count; i++)
        {
            if (SaveManager.COMPLETED_LEVELS[i] < _levelButtons.Length)
                _levelButtons[SaveManager.COMPLETED_LEVELS[i]].CompleteLevel();
        }
    }
}
