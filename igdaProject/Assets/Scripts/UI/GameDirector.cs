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
    [SerializeField]
    InventoryManager inventoryMan;

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
        Routine.Start(DelayOpenEditor());
    }
    IEnumerator DelayOpenEditor()
    {
        CloseMainMenu();
        yield return 0.5F;
        inventoryMan.Open();
    }

    public void CloseEditor(bool save)
    {
        Routine.Start(DelayCloseEditor(save));
    }
    IEnumerator DelayCloseEditor(bool save)
    {
        if (save) inventoryMan.SaveAndClose();
        else inventoryMan.ForceClose();
        yield return 0.5F;
        OpenMainMenu();
    }

    public IEnumerator LoadingStuff()
    {
        yield return inventoryMan.Initialize(GameManager.instance.playerdata.inventoryStr);
    }
}
