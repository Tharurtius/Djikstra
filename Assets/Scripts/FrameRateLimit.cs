using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameRateLimit : MonoBehaviour
{
    public Dropdown dd;
    private void Start()
    {
        QualitySettings.vSyncCount = 1;//2 or 3 or 4

        Application.targetFrameRate = 60;
    }

    public void UpdateFramerate()
    {
        Application.targetFrameRate = int.Parse(dd.options[dd.value].text);
        switch (Application.targetFrameRate)
        {
            case 60:
                QualitySettings.vSyncCount = 1;
                break;
            case 30:
                QualitySettings.vSyncCount = 2;
                break;
            case 20:
                QualitySettings.vSyncCount = 3;
                break;
        }
    }
}
