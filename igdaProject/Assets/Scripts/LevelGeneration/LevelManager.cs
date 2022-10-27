using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // connects main menu -> load level -> level UI -> combat manager with level generated animal
    [SerializeField]
    LevelGenerator levelGen;
    LevelData currentLevel;

    

    public void NewLevel()
    {
        // validation covered before this
        currentLevel = levelGen.GenerateNewLevel();
        GameManager.instance.playerdata.currentLevel = currentLevel;
        GameManager.SaveData();
    }

    public void ContinueLevel()
    {
        currentLevel = GameManager.instance.playerdata.currentLevel;
    }

    public void LoadLevel()
    {
        if (currentLevel.selectedStages == null) return;

        Debug.Log("show map screen, then open/load combat pew pew");
    }


}
