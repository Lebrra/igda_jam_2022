using BeauRoutine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles all menu system functionality
/// </summary>
public class GameDirector : MonoBehaviour
{
    public static GameDirector instance;

    [SerializeField]
    TitleManager titleMan;
    [SerializeField]
    MainMenuManager menuMan;
    [SerializeField]
    SettingsManager settingsMan;

    private void Awake()
    {
        if (instance) Destroy(instance);
        instance = this;
    }

    public void OpenMainMenu()
    {
        menuMan.Open();
    }

    public void CloseMainMenu()
    {
        menuMan.Close();
    }

    public void OpenTitle()
    {
        titleMan.Open();
    }

    public void OpenSettings()
    {
        settingsMan.Open();
    }

    public void OpenEditor()
    {
        // open editor
    }
}
