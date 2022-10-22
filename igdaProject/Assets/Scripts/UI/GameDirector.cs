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
    Animator menuAnim;
    [SerializeField]
    MainMenuManager menuMan;

    private void Awake()
    {
        if (instance) Destroy(instance);
        instance = this;
    }

    public void OpenMainMenu()
    {
        menuAnim.SetBool("UiStatus", true);
        menuMan.LoadMenu();
    }

    public void OpenTitle()
    {
        menuAnim.SetBool("UiStatus", false);
        titleMan.Open();
    }
}
