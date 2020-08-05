using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoSingleton<SaveManager>
{
    [SerializeField] private LevelData[] _levels = new LevelData[1];

    public static int CURRENT_LEVEL = 0;
    public static List<int> UNLOCKED_LEVELS = new List<int>();
    public static List<int> COMPLETED_LEVELS = new List<int>();

    private static int COMPLETED_LEVELS_COUNT = 0;
    private static int UNLOCKED_LEVELS_COUNT = 1;

    public LevelData CurrentLevelData { get => _levels[CURRENT_LEVEL];}

    public override void Init()
    {
        base.Init();

        if (PlayerPrefs.HasKey("CURRENT_LEVEL"))
        {
            // Initialize level states
            for (int i = 0; i < COMPLETED_LEVELS_COUNT; i++)
            {
                COMPLETED_LEVELS.Add(PlayerPrefs.GetInt("COMPLETED_LEVEL" + i));
            }

            for (int i = 0; i < UNLOCKED_LEVELS_COUNT; i++)
            {
                UNLOCKED_LEVELS.Add(PlayerPrefs.GetInt("UNLOCKED_LEVEL" + i));
            }
        }
    }

    private void LoadData()
    {
        CURRENT_LEVEL = PlayerPrefs.GetInt("CURRENT_LEVEL");

        COMPLETED_LEVELS_COUNT = PlayerPrefs.GetInt("COMPLETED_LEVELS_COUNT");
        UNLOCKED_LEVELS_COUNT = PlayerPrefs.GetInt("UNLOCKED_LEVELS_COUNT");
    }

    public static void SaveData()
    {
        PlayerPrefs.SetInt("CURRENT_LEVEL", CURRENT_LEVEL);

        COMPLETED_LEVELS_COUNT = COMPLETED_LEVELS.Count;
        UNLOCKED_LEVELS_COUNT = UNLOCKED_LEVELS.Count;
        PlayerPrefs.SetInt("COMPLETED_LEVELS_COUNT", COMPLETED_LEVELS_COUNT);
        PlayerPrefs.SetInt("UNLOCKED_LEVELS_COUNT", UNLOCKED_LEVELS_COUNT);

        for (int i = 0; i < COMPLETED_LEVELS_COUNT; i++)
        {
            PlayerPrefs.SetInt("COMPLETED_LEVEL" + i, COMPLETED_LEVELS[i]);
        }

        for (int i = 0; i < UNLOCKED_LEVELS_COUNT; i++)
        {
            PlayerPrefs.SetInt("UNLOCKED_LEVEL" + i, UNLOCKED_LEVELS[i]);
        }

        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        SaveData();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveData();
    }
}
