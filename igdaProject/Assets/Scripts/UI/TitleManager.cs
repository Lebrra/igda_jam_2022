using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    Animator titleAnim;

    [SerializeField]
    Button continueButton;
    [SerializeField]
    Button newButton;

    // animal loader for continue button

    [SerializeField]
    SaveData defaultSave;

    bool hasSave = false;

    public void Open()
    {
        titleAnim.SetBool("Status", true);

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

    public void Close()
    {
        continueButton.onClick.RemoveListener(ContinueGame);
        newButton.onClick.RemoveListener(NewGameWarning);

        titleAnim.SetBool("Status", false);
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
            var popup = GameManager.instance.GeneralPopup;
            popup.FillContent("Starting a new game will delete your old save, are you sure?", () => {
                NewGame();
                popup.Close();
                }, popup.Close);
            popup.Open();
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
        Close();
        GameManager.ToGame();
    }
}
