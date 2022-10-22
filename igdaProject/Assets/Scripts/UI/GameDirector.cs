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

    private void Awake()
    {
        if (instance) Destroy(instance);
        instance = this;
    }

    IEnumerator LoadGameAsync()
    {
        // write a script for importing data async
        yield return 1F;

        menuAnim.SetBool("UiStatus", true);
    }

    public void LoadTitle()
    {
        // TODO: change this to animator triggers when existing
        titleMan.gameObject.SetActive(true);
        titleMan.Open();
    }
}
