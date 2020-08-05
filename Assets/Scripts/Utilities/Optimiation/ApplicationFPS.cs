using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationFPS : MonoBehaviour
{
    [SerializeField]
    private int _xResolution = 720;
    [SerializeField]
    private int _yResolution = 1280;

    private void Awake()
    {
        // -1 - max frame rate for current platform
        Application.targetFrameRate = 60;

        Screen.SetResolution(_xResolution, _yResolution, true);

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
