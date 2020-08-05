using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame(int levelID)
    {
        SaveManager.CURRENT_LEVEL = levelID;
        SaveManager.SaveData();
        SceneManager.LoadScene(1);
    }
}
