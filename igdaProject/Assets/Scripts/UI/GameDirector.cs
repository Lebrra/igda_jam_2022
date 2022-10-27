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
    [SerializeField]
    CombatManager combatMan;
    [SerializeField]
    LevelManager levelMan;

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

    public void OpenCombatPreview() {
        Debug.Log("Combat has begun!!!");
        Routine.Start(DelayOpenCombatPreview());
    }
    IEnumerator DelayOpenCombatPreview() {
        CloseMainMenu();
        yield return 0.5f;
        combatMan.OpenPreview();
    }

    public void ClosePreviewToCombat(bool b) {
        Routine.Start(DelayCloseCombatPreview(b));
    }
    IEnumerator DelayCloseCombatPreview(bool inCombat) {
        combatMan.ClosePreview();
        yield return 0.5f;
        if (inCombat) {
            //going to combat
            OpenCombat();
        } else {
            OpenMainMenu();
        }
    }

    public void OpenCombat() {
        Debug.Log("Combat has begun!!!");
        Routine.Start(DelayOpenCombat());
    }
    IEnumerator DelayOpenCombat() {
        CloseMainMenu();
        yield return 0.5f;
        combatMan.OpenCombat();
    }

    IEnumerator DelayCloseCombat() {
        yield return 0.5f;
    }

    public void OpenLevel(bool newLevel)
    {
        if (newLevel) levelMan.NewLevel();
        else levelMan.ContinueLevel();

        Routine.Start(DelayOpenLevel());
    }
    IEnumerator DelayOpenLevel()
    {
        CloseMainMenu();
        yield return 0.5f;
        levelMan.LoadLevel();
    }


    public IEnumerator LoadingStuff()
    {
        yield return inventoryMan.Initialize(GameManager.instance.playerdata.inventoryStr);
    }
}
