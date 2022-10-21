using BeauRoutine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalPrefabBuilder : MonoBehaviour
{
    public RectTransform animalTransform;
    public AnimalPartsObject testAnimal;

    // saved parts
    AnimalPartUI headPart;
    BodyPartUI bodyPart;
    AnimalPartUI tailPart;
    AnimalPartUI legsFLPart;
    AnimalPartUI legsBLPart;
    AnimalPartUI legsFRPart;
    AnimalPartUI legsBRPart;

    public void CreateAnimal(AnimalPartsObject animal)
    {
        // get all prefab references
        headPart = Resources.Load<AnimalPartUI>("Parts/Prefabs/" + animal.headID);
        bodyPart = Resources.Load<BodyPartUI>("Parts/Prefabs/" + animal.bodyID);
        tailPart = Resources.Load<AnimalPartUI>("Parts/Prefabs/" + animal.tailID);
        legsFLPart = Resources.Load<AnimalPartUI>("Parts/Prefabs/" + animal.legsID + "_FL");
        legsBLPart = Resources.Load<AnimalPartUI>("Parts/Prefabs/" + animal.legsID + "_BL");
        legsFRPart = Resources.Load<AnimalPartUI>("Parts/Prefabs/" + animal.legsID + "_FR");
        legsBRPart = Resources.Load<AnimalPartUI>("Parts/Prefabs/" + animal.legsID + "_BR");

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
        legsFRPart.transform.SetAsLastSibling();    // 0
        legsBRPart.transform.SetAsLastSibling();    // 1
        bodyPart.transform.SetAsLastSibling();      // 2
        headPart.transform.SetAsLastSibling();      // 3
        tailPart.transform.SetAsLastSibling();      // 4
        legsBLPart.transform.SetAsLastSibling();    // 5
        legsFLPart.transform.SetAsLastSibling();    // 6
    }

    public void ChangeBodyPart(string newpart)
    {
        // get part type
        var data = Resources.Load<AnimalPart>("Parts/Data/" + newpart);
        if (data == null)
        {
            Debug.Log("D: " + newpart);
            return;
        }
        switch (data.partData.bodyPart)
        {
            case BodyPart.Head: 
                ChangeHead(newpart);
                break;
            case BodyPart.Body:
                ChangeBody(newpart);
                break;
            case BodyPart.Tail:
                ChangeTail(newpart);
                break;
            case BodyPart.Legs:
                ChangeLeg(newpart);
                break;
            default:
                Debug.LogError($"Part {newpart} not found, cannot import. ");
                return;
        }
    }

    void ChangeHead(string part)
    {
        if (headPart)
        {
            // destroy it!
            Destroy(headPart.gameObject);
            headPart = null;
        }

        headPart = Resources.Load<AnimalPartUI>("Parts/Prefabs/" + part);
        headPart = Instantiate(headPart, bodyPart.headPoint);
        headPart.FindMyPart(part);
        headPart.transform.SetParent(animalTransform);
        headPart.transform.SetSiblingIndex(3);
    }

    void ChangeBody(string part)
    {
        // oof

        AnimalPartsObject recreatedAnim = new AnimalPartsObject();
        recreatedAnim.headID = headPart.id;
        recreatedAnim.tailID = tailPart.id;
        recreatedAnim.legsID = legsFLPart.id;
        recreatedAnim.bodyID = part;

        DestroyAnimal();
        CreateAnimal(recreatedAnim);
    }

    void ChangeTail(string part)
    {
        if (tailPart)
        {
            // destroy it!
            Destroy(tailPart.gameObject);
            tailPart = null;
        }

        tailPart = Resources.Load<AnimalPartUI>("Parts/Prefabs/" + part);
        tailPart = Instantiate(tailPart, bodyPart.tailPoint);
        tailPart.FindMyPart(part);
        tailPart.transform.SetParent(animalTransform);
        tailPart.transform.SetSiblingIndex(4);
    }

    void ChangeLeg(string part)
    {
        // x4

        if (legsFLPart)
        {
            // destroy it!
            Destroy(legsFLPart.gameObject);
            legsFLPart = null;
        }

        legsFLPart = Resources.Load<AnimalPartUI>("Parts/Prefabs/" + part + "_FL");
        legsFLPart = Instantiate(legsFLPart, bodyPart.legFL);
        legsFLPart.FindMyPart(part);
        legsFLPart.transform.SetParent(animalTransform);
        legsFLPart.transform.SetSiblingIndex(6);

        if (legsBLPart)
        {
            // destroy it!
            Destroy(legsBLPart.gameObject);
            legsBLPart = null;
        }

        legsBLPart = Resources.Load<AnimalPartUI>("Parts/Prefabs/" + part + "_BL");
        legsBLPart = Instantiate(legsBLPart, bodyPart.legBL);
        legsBLPart.FindMyPart(part);
        legsBLPart.transform.SetParent(animalTransform);
        legsBLPart.transform.SetSiblingIndex(5);

        if (legsFRPart)
        {
            // destroy it!
            Destroy(legsFRPart.gameObject);
            legsFRPart = null;
        }

        legsFRPart = Resources.Load<AnimalPartUI>("Parts/Prefabs/" + part + "_FR");
        legsFRPart = Instantiate(legsFRPart, bodyPart.legFR);
        legsFRPart.FindMyPart(part);
        legsFRPart.transform.SetParent(animalTransform);
        legsFRPart.transform.SetSiblingIndex(0);

        if (legsBRPart)
        {
            // destroy it!
            Destroy(legsBRPart.gameObject);
            legsBRPart = null;
        }

        legsBRPart = Resources.Load<AnimalPartUI>("Parts/Prefabs/" + part + "_BR");
        legsBRPart = Instantiate(legsBRPart, bodyPart.legBR);
        legsBRPart.FindMyPart(part);
        legsBRPart.transform.SetParent(animalTransform);
        legsBRPart.transform.SetSiblingIndex(1);
    }

    public void DestroyAnimal()
    {
        if (headPart) Destroy(headPart.gameObject);
        if (bodyPart) Destroy(bodyPart.gameObject);
        if (tailPart) Destroy(tailPart.gameObject);
        if (legsFLPart) Destroy(legsFLPart.gameObject);
        if (legsBLPart) Destroy(legsBLPart.gameObject);
        if (legsFRPart) Destroy(legsFRPart.gameObject);
        if (legsBRPart) Destroy(legsBRPart.gameObject);

        headPart =
        tailPart =
        legsFLPart =
        legsBLPart =
        legsFRPart =
        legsBRPart = null;
        bodyPart = null;
    }

    private void Start()
    {
        //CreateAnimal(testAnimal);
        Routine.Start(TestingAllParts());
    }

    IEnumerator TestingAllParts()
    {
        CreateAnimal(testAnimal);
        yield return 2F;
        ChangeBodyPart("alligator_head");
        yield return 2F;
        ChangeBodyPart("alligator_body");
        yield return 2F;
        ChangeBodyPart("alligator_legs");
        yield return 2F;
        ChangeBodyPart("alligator_tail");
    }
}
