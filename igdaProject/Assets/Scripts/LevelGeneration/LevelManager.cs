using BeauRoutine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    // connects main menu -> load level -> level UI -> combat manager with level generated animal
    [SerializeField]
    LevelGenerator levelGen;
    LevelData currentLevel;

    [SerializeField]
    Animator mapAnim;
    [SerializeField]
    Image[] mapSquares;
    [SerializeField]
    RectTransform pawnPref;

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
        
        for (int i = 0; i < mapSquares.Length; i += 3)
        {
            var color = levelGen.StringToBiome(currentLevel.selectedStages[i / 3]).color;
            mapSquares[i].color = color;
            mapSquares[i + 1].color = color;
            mapSquares[i + 2].color = color;
        }

        mapAnim.SetBool("Status", true);
        Routine.Start(LevelAnims());
    }

    IEnumerator LevelAnims()
    {
        int lastSquare = (currentLevel.currentStage * 3) + currentLevel.currentMatch - 1;
        RectTransform pawn;
        if (lastSquare < 0) pawn = Instantiate(pawnPref, mapSquares[0].GetComponent<Transform>());
        else pawn = Instantiate(pawnPref, mapSquares[lastSquare].GetComponent<Transform>());
        Instantiate(DataManager.instance.GetAnimalPartUI(GameManager.instance.playerdata.GetActiveAnimal().headID), pawn.GetChild(0));
        pawn.SetRotation(Vector3.zero);
        //pawn.localRotation.eulerAngles = Vector3.zero;
        //if (lastSquare % 6 > 2) pawn.localScale = new Vector3(pawn.localScale.x * -1F, pawn.localScale.y, 1F);
        if (lastSquare < 0) yield return pawn.MoveTo(pawn.position.x - 200F, 0F, Axis.X);

        mapAnim.SetBool("Status", true);
        yield return 1F;

        pawn.SetParent(pawn.parent.parent.parent);
        List<IEnumerator> tweens = new List<IEnumerator>();
        tweens.Add(pawn.MoveTo(mapSquares[lastSquare + 1].GetComponent<RectTransform>(), 1.7F));
        tweens.Add(TwistPawn(pawn, 1.7F));
        yield return Routine.Race(tweens);
        yield return pawn.RotateTo(0F, 0.1F, Axis.Z);

        pawn.SetParent(mapSquares[lastSquare + 1].GetComponent<RectTransform>());
        yield return 0.4F;

        mapAnim.SetBool("Status", false);
        yield return 0.8F;
        GameDirector.instance.OpenPreviewFromLevel(currentLevel.currentGeneratedOpponent, currentLevel.currentMatch == 2);
        Destroy(pawn.gameObject);
    }

    IEnumerator TwistPawn(RectTransform pawn, float time)
    {
        float reducedTime = time / 7F;
        while (time >reducedTime)
        {
            yield return pawn.RotateTo(9F, reducedTime, Axis.Z);
            time -= reducedTime;
            yield return pawn.RotateTo(-9F, reducedTime, Axis.Z);
            time -= reducedTime;
        }
        yield return pawn.RotateTo(0F, reducedTime, Axis.Z);
    }

    public void SetNextOpponent()
    {
        var playerLevel = GameManager.instance.playerdata.currentLevel;
        GameManager.instance.playerdata.currentLevel.currentGeneratedOpponent = levelGen.CreateOpponent(playerLevel.selectedStages[playerLevel.currentStage]);
    }
}
