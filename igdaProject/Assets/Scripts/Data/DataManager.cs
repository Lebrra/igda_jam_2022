using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public static DataManager instance;

    [Header("Body Part Lists")]
    public List<AnimalPart> masterList = new List<AnimalPart>();
    public List<AnimalPart> headList = new List<AnimalPart>();
    public List<AnimalPart> bodyList = new List<AnimalPart>();
    public List<AnimalPart> legsList = new List<AnimalPart>();
    public List<AnimalPart> tailList = new List<AnimalPart>();

    private void Awake() {
        if(instance == null) instance = this;
        else Destroy(this);
    }
    private void Start() {
        InitializeLists();
    }
    private void InitializeLists() {
        var animalPartList = Resources.LoadAll<AnimalPart>("Parts/Data/");

        //generate all 4 lists
        foreach (AnimalPart part in animalPartList) {
            masterList.Add(part);
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

}
