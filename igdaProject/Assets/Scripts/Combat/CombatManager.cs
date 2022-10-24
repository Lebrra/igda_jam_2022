using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
    public Player player;
    public Enemy enemy;

    [Header("Combat Preview")]
    [SerializeField] Animator previewAnim;
    [SerializeField] Button battleButton;
    [SerializeField] Button backButton;
    [SerializeField] AnimalPrefabBuilder enemyObjPreview;
    [SerializeField] AnimalPrefabBuilder playerObjPreview;


    [Header("Combat Scene")]
    [SerializeField] Animator combatAnim;
    [SerializeField] TextMeshProUGUI enemyName, playerName;
    [SerializeField] AnimalPrefabBuilder enemyObj;
    [SerializeField] AnimalPrefabBuilder playerObj;

    

    private void Awake() {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    private void Start() {

        battleButton.onClick.AddListener(() => GameDirector.instance.ClosePreviewToCombat(true));
        backButton.onClick.AddListener(() => GameDirector.instance.ClosePreviewToCombat(false));
    }

    public void OpenPreview() {
        enemy.RandomizeBuild();
        previewAnim.SetBool("Status", true);
        playerObjPreview.CreateAnimal(GameManager.instance.playerdata.GetActiveAnimal(), true, true);
        enemyObjPreview.CreateAnimal(enemy.myData, true, true);
    }

    public void ClosePreview() {
        previewAnim.SetBool("Status", false);
        playerObjPreview.DestroyAnimal();
        enemyObjPreview.DestroyAnimal();
    }
    public void OpenCombat() {
        combatAnim.SetBool("Status", true);
        playerObj.CreateAnimal(GameManager.instance.playerdata.GetActiveAnimal(), true, true);
        enemyObj.CreateAnimal(enemy.myData, true, true);
    }

    public void CloseCombat() {
        combatAnim.SetBool("Status", false);
        playerObj.DestroyAnimal();
        enemyObj.DestroyAnimal();
    }
    



    private void NewRound() {
        /*
         * DEFINITION OF ROUND
         * - A round consists of both players determining their selected move to use.
         * - These moves will occur during this time period known as a round. 
         * 
         * I need to first have both players select their move inside of a turn.
         * After both moves are selected, the moves will occur in order of highest to lowest speed of each monster
         * 
         * IE: Pet A has 10 speed, Pet B has 3 speed. When the round begins, Pet A will go first. 
         * - This means that if Pet A defeats Pet B during its move, Pet B's move will nullify. 
         * - The importance of speed is to determine who gets to hit first, which can mean the difference in a match. 
         * 
         */

    }

}
