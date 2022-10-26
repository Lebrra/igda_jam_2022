using BeauRoutine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalPrefabBuilder : MonoBehaviour
{
    public RectTransform animalTransform;

    [SerializeField]
    TMPro.TextMeshProUGUI nameText;

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

    [SerializeField]
    bool easyTesting = false;
    [SerializeField]
    string animal;

    [SerializeField]
    bool partsTesting = false;
    [SerializeField]
    public AnimalPartsObject testAnimal;

    Routine animator;

    IEnumerator CreateWithValidation(AnimalPartsObject animal, bool animated, bool zeroOut)
    {
        if (bodyPart != null)
        {
            if (animated) yield return ShrinkDestroyAll();
            else DestroyAnimal();
        }

        GenerateAnimal(animal);

        if (zeroOut)
        {
            if (!animated) Debug.LogWarning("All animal parts will be too small to see! Don't check zeroOut without animated!");

            bodyPart.transform.localScale = Vector2.zero;
            headPart.transform.localScale = Vector2.zero;
            tailPart.transform.localScale = Vector2.zero;
            legsFLPart.transform.localScale = Vector2.zero;
            legsBLPart.transform.localScale = Vector2.zero;
            legsFRPart.transform.localScale = Vector2.zero;
            legsBRPart.transform.localScale = Vector2.zero;
        }

        if (animated) yield return GrowSpawnAll();
        else if (nameText) nameText.text = NameGenerator(GetCreatedAnimal());
    }

    void GenerateAnimal(AnimalPartsObject animal)
    {
        // get all prefab references
        headPart = DataManager.instance.GetAnimalPartUI(animal.headID);
        bodyPart = DataManager.instance.GetBodyPartUI(animal.bodyID);
        tailPart = DataManager.instance.GetAnimalPartUI(animal.tailID);
        legsFLPart = DataManager.instance.GetAnimalPartUI(animal.legsID + "_FL");
        legsBLPart = DataManager.instance.GetAnimalPartUI(animal.legsID + "_BL");
        legsFRPart = DataManager.instance.GetAnimalPartUI(animal.legsID + "_FR");
        legsBRPart = DataManager.instance.GetAnimalPartUI(animal.legsID + "_BR");

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

    public void CreateAnimal(AnimalPartsObject animal, bool animated = true, bool zeroOut = false, AnimationType anim = AnimationType.None)
    {
        partSwapper.Replace(CreateWithValidation(animal, animated, zeroOut));
        partSwapper.OnComplete(() => SetAnimationState(anim));
    }

    public void ChangeBodyPart(string newpart)
    {
        // get part type
        var data = DataManager.instance.GetAnimalPart(newpart);
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
        partSwapper.OnComplete(() => SetAnimationState(AnimationType.Bob));
    }

    IEnumerator ChangeHead(string part)
    {
        if (headPart)
        {
            // destroy it!
            yield return ShrinkDestroyObject(headPart.GetComponent<RectTransform>());
        }

        headPart = DataManager.instance.GetAnimalPartUI(part);
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
        CreateAnimal(recreatedAnim, true, true, AnimationType.Bob);
        yield return GrowSpawnAll();
    }

    IEnumerator ChangeTail(string part)
    {
        if (tailPart)
        {
            // destroy it!
            yield return ShrinkDestroyObject(tailPart.GetComponent<RectTransform>());
        }

        tailPart = DataManager.instance.GetAnimalPartUI(part);
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

        legsFLPart = DataManager.instance.GetAnimalPartUI(part + "_FL");
        legsFLPart = Instantiate(legsFLPart, bodyPart.legFL);
        legsFLPart.FindMyPart(part);
        legsFLPart.transform.SetParent(animalTransform);
        legsFLPart.transform.SetSiblingIndex(5);
        legsFLPart.transform.localScale = Vector2.zero;

        legsBLPart = DataManager.instance.GetAnimalPartUI(part + "_BL");
        legsBLPart = Instantiate(legsBLPart, bodyPart.legBL);
        legsBLPart.FindMyPart(part);
        legsBLPart.transform.SetParent(animalTransform);
        legsBLPart.transform.SetSiblingIndex(6);
        legsBLPart.transform.localScale = Vector2.zero;

        legsFRPart = DataManager.instance.GetAnimalPartUI(part + "_FR");
        legsFRPart = Instantiate(legsFRPart, bodyPart.legFR);
        legsFRPart.FindMyPart(part);
        legsFRPart.transform.SetParent(animalTransform);
        legsFRPart.transform.SetSiblingIndex(0);
        legsFRPart.transform.localScale = Vector2.zero;

        legsBRPart = DataManager.instance.GetAnimalPartUI(part + "_BR");
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

        headPart = tailPart = legsFLPart = legsBLPart = legsFRPart = legsBRPart = null;
        bodyPart = null;
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
        if (animator.Exists()) animator.Stop();
        if (obj == null) yield break;
        yield return obj.ScaleTo(Vector2.zero, duration);
        Destroy(obj.gameObject);
    }

    IEnumerator GrowSpawnObject(RectTransform obj, float duration = 0.3F)
    {
        if (nameText) nameText.text = NameGenerator(GetCreatedAnimal());
        yield return obj.ScaleTo(Vector2.one, duration);
    }

    public AnimalPartsObject GetCreatedAnimal()
    {
        AnimalPartsObject animal = new AnimalPartsObject();
        animal.headID = headPart.id;
        animal.bodyID = bodyPart.id;
        animal.legsID = legsBLPart.id;
        animal.tailID = tailPart.id;

        return animal;
    }

    public string NameGenerator(AnimalPartsObject animal)
    {


        string name = "";

        AnimalPart head, body, legs, tail;
        head = DataManager.instance.GetAnimalPart(animal.headID);
        body = DataManager.instance.GetAnimalPart(animal.bodyID);
        legs = DataManager.instance.GetAnimalPart(animal.legsID);
        tail = DataManager.instance.GetAnimalPart(animal.tailID);

        if (head.partData.animal == body.partData.animal && head.partData.animal == legs.partData.animal
            && head.partData.animal == tail.partData.animal)
        {
            Debug.Log(head.partData.animal);
            return head.partData.animal; //when all parts are of all the same animal. 
        }

        char[] temp = head.partData.namePart.ToCharArray();
        string capital = "";

        for (int i = 0; i < temp.Length; i++)
        {
            if (i == 0)
            {
                capital += temp[i].ToString().ToUpper();
            }
            else
            {
                capital += temp[i].ToString();
            }
        }

        name += capital;
        name += body.partData.namePart;
        name += legs.partData.namePart;
        name += tail.partData.namePart;
        Debug.Log(name);

        return name;
    }

    private void Start()
    {
        if (partsTesting)
        {
            var newAnimal = AnimalPart.AnimalToPartsObj(animal);
            if (testAnimal.headID == "") testAnimal.headID = newAnimal.headID;
            if (testAnimal.bodyID == "") testAnimal.bodyID = newAnimal.bodyID;
            if (testAnimal.legsID == "") testAnimal.legsID = newAnimal.legsID;
            if (testAnimal.tailID == "") testAnimal.tailID = newAnimal.tailID;

            CreateAnimal(testAnimal, true, true, AnimationType.Bob);
        }
        else if (easyTesting)
        {
            CreateAnimal(AnimalPart.AnimalToPartsObj(animal), true, true, AnimationType.Bob);
        }
    }

    public void SetAnimationState(AnimationType anim)
    {
        Debug.Log("Setting animal animator to " + anim);
        animator.Replace(SetAnimState(anim));
    }

    IEnumerator SetAnimState(AnimationType anim)
    {
        yield return new WaitUntil(() => IsTransitioning == false);
        switch (anim)
        {
            case AnimationType.Bob: 
                yield return AnimBodyBob();
                break;
            case AnimationType.HeadBob:
                yield return AnimHeadBob();
                break;
            default:
                yield return null;
                break;
        }
    }

    IEnumerator AnimAttack()
    {
        yield return 2F;

        // don't use this
        var anims = new List<IEnumerator>();
        anims.Add(animalTransform.RotateTo(-75F, 1F, Axis.Z));
        anims.Add(legsBLPart.GetComponent<RectTransform>().RotateTo(-5F, 1F, Axis.Z));
        anims.Add(headPart.GetComponent<RectTransform>().RotateTo(5F, 1F, Axis.Z));
        anims.Add(legsBRPart.GetComponent<RectTransform>().RotateTo(-5F, 1F, Axis.Z));
        anims.Add(legsFLPart.GetComponent<RectTransform>().RotateTo(-15F, 1F, Axis.Z));
        anims.Add(legsFRPart.GetComponent<RectTransform>().RotateTo(-15F, 1F, Axis.Z));
        anims.Add(tailPart.GetComponent<RectTransform>().RotateTo(20F, 1F, Axis.Z));
        yield return Routine.Combine(anims);
        yield return 0.5F;
        yield return legsFLPart.GetComponent<RectTransform>().RotateTo(-180F, 1.5F, Axis.Z);
        yield return 0.3F;
        yield return legsFLPart.GetComponent<RectTransform>().RotateTo(-20F, 0.2F, Axis.Z);
        yield return 0.3F;
        anims = new List<IEnumerator>();
        anims.Add(animalTransform.RotateTo(0F, 1F, Axis.Z));
        anims.Add(legsBLPart.GetComponent<RectTransform>().RotateTo(0F, 1F, Axis.Z));
        anims.Add(headPart.GetComponent<RectTransform>().RotateTo(0F, 1F, Axis.Z));
        anims.Add(legsBRPart.GetComponent<RectTransform>().RotateTo(0F, 1F, Axis.Z));
        anims.Add(legsFLPart.GetComponent<RectTransform>().RotateTo(0F, 1F, Axis.Z));
        anims.Add(legsFRPart.GetComponent<RectTransform>().RotateTo(0F, 1F, Axis.Z));
        anims.Add(tailPart.GetComponent<RectTransform>().RotateTo(0F, 1F, Axis.Z));
        yield return Routine.Combine(anims);
    }

    IEnumerator AnimBodyBob()
    {
        float dif = 7F;
        float time = 0.5F;
        var bodyRect = bodyPart.GetComponent<RectTransform>();
        var bodyOriginalPos = bodyRect.position.y;
        var headRect = headPart.GetComponent<RectTransform>();
        var headOriginalPos = headRect.position.y;
        var tailRect = tailPart.GetComponent<RectTransform>();
        var tailOriginalPos = tailRect.position.y;
        
        while (true)
        {
            var bobs = new List<IEnumerator>();
            bobs.Add(bodyRect.MoveTo(bodyOriginalPos + dif, time, Axis.Y));
            bobs.Add(headRect.MoveTo(headOriginalPos + dif, time, Axis.Y));
            bobs.Add(tailRect.MoveTo(tailOriginalPos + dif, time, Axis.Y));

            //bobs.Add(legsFLPart.GetComponent<RectTransform>().RotateTo(1F, time, Axis.Z));
            //bobs.Add(legsFRPart.GetComponent<RectTransform>().RotateTo(1F, time, Axis.Z));
            //bobs.Add(legsBRPart.GetComponent<RectTransform>().RotateTo(-1F, time, Axis.Z));
            //bobs.Add(legsBLPart.GetComponent<RectTransform>().RotateTo(-1F, time, Axis.Z));
            yield return Routine.Combine(bobs);
        
            bobs = new List<IEnumerator>();
            bobs.Add(bodyRect.MoveTo(bodyOriginalPos, time, Axis.Y));
            bobs.Add(headRect.MoveTo(headOriginalPos, time, Axis.Y));
            bobs.Add(tailRect.MoveTo(tailOriginalPos, time, Axis.Y));

            //bobs.Add(legsFLPart.GetComponent<RectTransform>().RotateTo(0F, time, Axis.Z));
            //bobs.Add(legsFRPart.GetComponent<RectTransform>().RotateTo(0F, time, Axis.Z));
            //bobs.Add(legsBRPart.GetComponent<RectTransform>().RotateTo(0F, time, Axis.Z));
            //bobs.Add(legsBLPart.GetComponent<RectTransform>().RotateTo(0F, time, Axis.Z));
            yield return Routine.Combine(bobs);
        }
    }

    IEnumerator AnimHeadBob()
    {
        while (true)
        {
            var bobs = new List<IEnumerator>();
            bobs.Add(headPart.GetComponent<RectTransform>().RotateTo(2F, 0.3F, Axis.Z));
            bobs.Add(tailPart.GetComponent<RectTransform>().RotateTo(-2F, 0.3F, Axis.Z));
            yield return Routine.Combine(bobs);

            bobs = new List<IEnumerator>();
            bobs.Add(headPart.GetComponent<RectTransform>().RotateTo(-2F, 0.3F, Axis.Z));
            bobs.Add(tailPart.GetComponent<RectTransform>().RotateTo(2F, 0.3F, Axis.Z));
            yield return Routine.Combine(bobs);
        }
    }

    public enum AnimationType
    {
        None,
        HeadBob,
        Bob
    }
}
