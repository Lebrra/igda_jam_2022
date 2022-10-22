using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public Button continueButton;
    public Button newButton;
    public GenericPopupLogic newGameWarning;

    // animal loader for continue button

    [SerializeField]
    SaveData defaultSave;

    bool hasSave = false;

    private void Start()
    {
        hasSave = JSONEditor.DoesFileExist(GameManager.SAVE_NAME);

        if (hasSave)
        {
            // do the cool animal thing here
        }
        else
        {
            // disable the animal thing, only new game button
            continueButton.gameObject.SetActive(false);
        }

        continueButton.onClick.AddListener(ContinueGame);
        newButton.onClick.AddListener(NewGameWarning);
    }

    void ContinueGame()
    {
        GameManager.instance.playerdata = JSONEditor.JSONToData<SaveData>(GameManager.SAVE_NAME);
        LoadGame();
    }

    void NewGameWarning()
    {
        if (!hasSave) NewGame();
        else
        {
            newGameWarning.FillContent("Starting a new game will delete your old save, are you sure?", NewGame, newGameWarning.Close);
            newGameWarning.Open();
        }
    }

    void NewGame()
    {
        GameManager.instance.playerdata = defaultSave;
        GameManager.SaveData();
        LoadGame();
    }

    void LoadGame()
    {
        // go to game scene 
        Debug.Log("GO TO GAME");
    }
}
