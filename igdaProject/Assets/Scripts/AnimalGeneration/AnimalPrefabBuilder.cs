using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalPrefabBuilder : MonoBehaviour
{
    public RectTransform animalTransform;
    public AnimalPartsObject testAnimal;

    public void CreateAnimal(AnimalPartsObject animal)
    {
        // get all prefab references
        var headPart = Resources.Load<AnimalPartUI>("Parts/Prefabs/" + animal.headID);
        var bodyPart = Resources.Load<BodyPartUI>("Parts/Prefabs/" + animal.bodyID);
        var tailPart = Resources.Load<AnimalPartUI>("Parts/Prefabs/" + animal.tailID);
        var legsFLPart = Resources.Load<AnimalPartUI>("Parts/Prefabs/" + animal.legsID + "_FL");
        var legsBLPart = Resources.Load<AnimalPartUI>("Parts/Prefabs/" + animal.legsID + "_BL");
        var legsFRPart = Resources.Load<AnimalPartUI>("Parts/Prefabs/" + animal.legsID + "_FR");
        var legsBRPart = Resources.Load<AnimalPartUI>("Parts/Prefabs/" + animal.legsID + "_BR");

        // instantiate in the correct positions
        bodyPart = Instantiate(bodyPart, animalTransform);
        headPart = Instantiate(headPart, bodyPart.headPoint);
        tailPart = Instantiate(tailPart, bodyPart.tailPoint);
        legsFLPart = Instantiate(legsFLPart, bodyPart.legFL);
        legsBLPart = Instantiate(legsBLPart, bodyPart.legBL);
        legsFRPart = Instantiate(legsFRPart, bodyPart.legFR);
        legsBRPart = Instantiate(legsBRPart, bodyPart.legBR);

        // set data for all
        bodyPart.FindMyPart(animal.bodyID);
        headPart.FindMyPart(animal.headID);
        tailPart.FindMyPart(animal.tailID);
        legsFLPart.FindMyPart(animal.legsID);
        legsBLPart.FindMyPart(animal.legsID);
        legsFRPart.FindMyPart(animal.legsID);
        legsBRPart.FindMyPart(animal.legsID);

        // deparent new things
        headPart.transform.SetParent(animalTransform);
        tailPart.transform.SetParent(animalTransform);
        legsFLPart.transform.SetParent(animalTransform);
        legsBLPart.transform.SetParent(animalTransform);
        legsFRPart.transform.SetParent(animalTransform);
        legsBRPart.transform.SetParent(animalTransform);

        // set correct order
        legsFRPart.transform.SetAsLastSibling();
        legsBRPart.transform.SetAsLastSibling();
        bodyPart.transform.SetAsLastSibling();
        headPart.transform.SetAsLastSibling();
        tailPart.transform.SetAsLastSibling();
        legsBLPart.transform.SetAsLastSibling();
        legsFLPart.transform.SetAsLastSibling();
    }

    private void Start()
    {
        CreateAnimal(testAnimal);
    }
}
