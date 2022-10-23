using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
    public Player player;
    public Enemy enemy;

    List<AnimalPart> headList = new List<AnimalPart>();
    List<AnimalPart> bodyList = new List<AnimalPart>();
    List<AnimalPart> legsList = new List<AnimalPart>();
    List<AnimalPart> tailList = new List<AnimalPart>();

    private void Awake() {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    public void Start() {
        InitializeLists();
    }

    private void InitializeLists() {
        var animalPartList = Resources.LoadAll<AnimalPart>("Parts/Data/");

        //generate all 4 lists
        foreach (AnimalPart part in animalPartList) {
            switch (part.partData.bodyPart) {
                case BodyPart.Head:
                    headList.Add(part);
                    break;
                case BodyPart.Body:
                    bodyList.Add(part);
                    break;
                case BodyPart.Legs:
                    legsList.Add(part);
                    break;
                case BodyPart.Tail:
                    tailList.Add(part);
                    break;
            }
        }
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
