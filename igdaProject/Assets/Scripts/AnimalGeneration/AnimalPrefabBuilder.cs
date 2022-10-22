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

    Routine partSwapper;
    public bool IsTransitioning { get => partSwapper.Exists(); }

    public void CreateAnimal(AnimalPartsObject animal, bool zeroOut = false)
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

        if (zeroOut)
        {
            bodyPart.transform.localScale = Vector2.zero;
            headPart.transform.localScale = Vector2.zero;
            tailPart.transform.localScale = Vector2.zero;
            legsFLPart.transform.localScale = Vector2.zero;
            legsBLPart.transform.localScale = Vector2.zero;
            legsFRPart.transform.localScale = Vector2.zero;
            legsBRPart.transform.localScale = Vector2.zero;
        }
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
                partSwapper.Replace(ChangeHead(newpart));
                break;
            case BodyPart.Body:
                partSwapper.Replace(ChangeBody(newpart));
                break;
            case BodyPart.Tail:
                partSwapper.Replace(ChangeTail(newpart));
                break;
            case BodyPart.Legs:
                partSwapper.Replace(ChangeLegs(newpart));
                break;
            default:
                Debug.LogError($"Part {newpart} not found, cannot import. ");
                return;
        }
    }

    IEnumerator ChangeHead(string part)
    {
        if (headPart)
        {
            // destroy it!
            yield return ShrinkDestroyObject(headPart.GetComponent<RectTransform>());
        }

        headPart = Resources.Load<AnimalPartUI>("Parts/Prefabs/" + part);
        headPart = Instantiate(headPart, bodyPart.headPoint);
        headPart.FindMyPart(part);
        headPart.transform.SetParent(animalTransform);
        headPart.transform.SetSiblingIndex(3);
        headPart.transform.localScale = Vector2.zero;
        yield return GrowSpawnObject(headPart.GetComponent<RectTransform>());
    }

    IEnumerator ChangeBody(string part)
    {
        // oof

        AnimalPartsObject recreatedAnim = new AnimalPartsObject();
        recreatedAnim.headID = headPart.id;
        recreatedAnim.tailID = tailPart.id;
        recreatedAnim.legsID = legsFLPart.id;
        recreatedAnim.bodyID = part;

        yield return ShrinkDestroyAll();
        CreateAnimal(recreatedAnim, true);
        yield return GrowSpawnAll();
    }

    IEnumerator ChangeTail(string part)
    {
        if (tailPart)
        {
            // destroy it!
            yield return ShrinkDestroyObject(tailPart.GetComponent<RectTransform>());
        }

        tailPart = Resources.Load<AnimalPartUI>("Parts/Prefabs/" + part);
        tailPart = Instantiate(tailPart, bodyPart.tailPoint);
        tailPart.FindMyPart(part);
        tailPart.transform.SetParent(animalTransform);
        tailPart.transform.SetSiblingIndex(4);
        tailPart.transform.localScale = Vector2.zero;
        yield return GrowSpawnObject(tailPart.GetComponent<RectTransform>());
    }

    IEnumerator ChangeLegs(string part)
    {
        // x4

        yield return Routine.Combine(ShrinkDestroyObject(legsFLPart.GetComponent<RectTransform>()),
            ShrinkDestroyObject(legsBLPart.GetComponent<RectTransform>()),
            ShrinkDestroyObject(legsFRPart.GetComponent<RectTransform>()),
            ShrinkDestroyObject(legsBRPart.GetComponent<RectTransform>()));

        legsFLPart = Resources.Load<AnimalPartUI>("Parts/Prefabs/" + part + "_FL");
        legsFLPart = Instantiate(legsFLPart, bodyPart.legFL);
        legsFLPart.FindMyPart(part);
        legsFLPart.transform.SetParent(animalTransform);
        legsFLPart.transform.SetSiblingIndex(6);
        legsFLPart.transform.localScale = Vector2.zero;

        legsBLPart = Resources.Load<AnimalPartUI>("Parts/Prefabs/" + part + "_BL");
        legsBLPart = Instantiate(legsBLPart, bodyPart.legBL);
        legsBLPart.FindMyPart(part);
        legsBLPart.transform.SetParent(animalTransform);
        legsBLPart.transform.SetSiblingIndex(5);
        legsBLPart.transform.localScale = Vector2.zero;

        legsFRPart = Resources.Load<AnimalPartUI>("Parts/Prefabs/" + part + "_FR");
        legsFRPart = Instantiate(legsFRPart, bodyPart.legFR);
        legsFRPart.FindMyPart(part);
        legsFRPart.transform.SetParent(animalTransform);
        legsFRPart.transform.SetSiblingIndex(0);
        legsFRPart.transform.localScale = Vector2.zero;

        legsBRPart = Resources.Load<AnimalPartUI>("Parts/Prefabs/" + part + "_BR");
        legsBRPart = Instantiate(legsBRPart, bodyPart.legBR);
        legsBRPart.FindMyPart(part);
        legsBRPart.transform.SetParent(animalTransform);
        legsBRPart.transform.SetSiblingIndex(1);
        legsBRPart.transform.localScale = Vector2.zero;

        yield return Routine.Combine(GrowSpawnObject(legsFLPart.GetComponent<RectTransform>()),
            GrowSpawnObject(legsBLPart.GetComponent<RectTransform>()),
            GrowSpawnObject(legsFRPart.GetComponent<RectTransform>()),
            GrowSpawnObject(legsBRPart.GetComponent<RectTransform>()));
    }

    public void DestroyAnimal()
    {
        partSwapper.Replace(ShrinkDestroyAll());

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
        CreateAnimal(testAnimal);
        //Routine.Start(TestingAllParts());
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
        //partSwapper.Replace(ShrinkDestroyObject(tailPart.GetComponent<RectTransform>()));
    }

    IEnumerator ShrinkDestroyAll()
    {
        yield return Routine.Combine(ShrinkDestroyObject(headPart.GetComponent<RectTransform>()),
            ShrinkDestroyObject(bodyPart.GetComponent<RectTransform>()),
            ShrinkDestroyObject(tailPart.GetComponent<RectTransform>()),
            ShrinkDestroyObject(legsBLPart.GetComponent<RectTransform>()),
            ShrinkDestroyObject(legsBRPart.GetComponent<RectTransform>()),
            ShrinkDestroyObject(legsFLPart.GetComponent<RectTransform>()),
            ShrinkDestroyObject(legsFRPart.GetComponent<RectTransform>()));
    }

    IEnumerator GrowSpawnAll()
    {
        yield return Routine.Combine(GrowSpawnObject(headPart.GetComponent<RectTransform>()),
            GrowSpawnObject(bodyPart.GetComponent<RectTransform>()),
            GrowSpawnObject(tailPart.GetComponent<RectTransform>()),
            GrowSpawnObject(legsBLPart.GetComponent<RectTransform>()),
            GrowSpawnObject(legsBRPart.GetComponent<RectTransform>()),
            GrowSpawnObject(legsFLPart.GetComponent<RectTransform>()),
            GrowSpawnObject(legsFRPart.GetComponent<RectTransform>()));
    }

    IEnumerator ShrinkDestroyObject(RectTransform obj, float duration = 0.2F)
    {
        yield return obj.ScaleTo(Vector2.zero, duration);
        Destroy(obj.gameObject);
    }

    IEnumerator GrowSpawnObject(RectTransform obj, float duration = 0.3F)
    {
        yield return obj.ScaleTo(Vector2.one, duration);
    }
}
