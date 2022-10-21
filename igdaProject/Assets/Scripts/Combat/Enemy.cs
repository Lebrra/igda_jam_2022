using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    List<AnimalPart> headList = new List<AnimalPart>();
    List<AnimalPart> bodyList = new List<AnimalPart>();
    List<AnimalPart> legsList = new List<AnimalPart>();
    List<AnimalPart> tailList = new List<AnimalPart>();


    public void Start() {
        InitializeEnemy();
    }

    private void InitializeEnemy() {
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
    public void RandomizeBuild() {

        int headIndex = Random.Range(0, headList.Count);
        int bodyIndex = Random.Range(0, bodyList.Count);
        int legsIndex = Random.Range(0, legsList.Count);
        int tailIndex = Random.Range(0, tailList.Count);

        UpdateAbility(headList[headIndex]);
        UpdateAbility(bodyList[bodyIndex]);
        UpdateAbility(legsList[legsIndex]);
        UpdateAbility(tailList[tailIndex]);

        Debug.Log("Enemy has been randomized!");
    }

}
