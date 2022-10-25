using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using BeauRoutine;

public class MonsterMakerGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject head;
    [SerializeField]
    GameObject tail;
    [SerializeField]
    GameObject body;
    [SerializeField]
    GameObject Leg;
    List<AnimalPart> partlist;
    Dictionary<string,AnimalPart> partListdict= new Dictionary<string, AnimalPart>();
    GameObject[] partObjectList;
    Dictionary<string, Toggle> inventoryToggles = new Dictionary<string, Toggle>();
    [SerializeField]
    GameObject ButtonTemplate;
    public static MonsterMakerGenerator instance;
    bool isFirst = true;
    bool head1 = true;
    bool body1 = true;
    bool tail1 = true;
    bool leg1 = true;
    AnimalPrefabBuilder thing;
    bool initialized = false;


    public IEnumerator Initialize()
    {
        thing = this.GetComponent<AnimalPrefabBuilder>();
        //thing.CreateAnimal(this.GetComponent<AnimalPrefabBuilder>().testAnimal);
        partlist = DataManager.instance.masterList;
        
        bool done = false;
        isFirst = true;
        List<GameObject> toDelete = new List<GameObject>();
        foreach(RectTransform o in head.GetComponentsInChildren<RectTransform>())
        {
            if (!done)
                done = true;
            else
            {
                toDelete.Add(o.gameObject);
                done = true;
            }
            yield return null;
        }
        done = false;
        foreach (RectTransform o in tail.GetComponentsInChildren<RectTransform>())
        {
            if (!done)
                done = true;
            else
            {
                toDelete.Add(o.gameObject);
                done = true;
            }
            yield return null;
        }
        done = false;
        foreach (RectTransform o in body.GetComponentsInChildren<RectTransform>())
        {
            if (!done)
                done = true;
            else
            {
                toDelete.Add(o.gameObject);
                done = true;
            }
            yield return null;
        }
        done = false;
        foreach (RectTransform o in Leg.GetComponentsInChildren<RectTransform>())
        {
            if (!done)
                done = true;
            else
            {
                toDelete.Add(o.gameObject);
                done = true;
            }
            yield return null;
        }
        foreach (var o in toDelete) Destroy(o);
        fillDictionaries();
        Setup();
        //UpdatePetParts();
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        initialized = true;
        DontDestroyOnLoad(this);
    }
    void fillDictionaries()
    {
        try
        {
            for (int i = 0; i < partlist.Count; i++)
            {
                if(!partListdict.ContainsKey(partlist[i].partData.id))
                partListdict.Add(partlist[i].partData.id, partlist[i]);
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

    }
    public void Setup()
    {
        try
        {
            AnimalPart part= new AnimalPart();
            print(InventoryManager.instance.getDict());
            foreach (KeyValuePair<string, bool> inst in InventoryManager.instance.getDict())
            {
                //if (inst.Key.Equals("redpanda_body"))
                    //break;
                if (inst.Value)
                {
                    //print(inst);
                    //if you have the part
                    string id = inst.Key;
                    if (partListdict.ContainsKey(id))
                    part = partListdict[id];
                    //if(partListObjectDict.ContainsKey(id))
                    //partUI = partListObjectDict[id];
                    UpdatePetParts(part);
                    
                }
            }
        }
        catch(Exception ex)
        {
            Debug.LogException(ex);
        }
    }
    public void UpdatePetParts(AnimalPart part)
    {
        savePart(part);
        switch(part.partData.bodyPart)
        {
            case BodyPart.Body:
                instantiateButton(part, body);
                break;
            case BodyPart.Head:
                instantiateButton(part, head);
                break;
            case BodyPart.Legs:
                instantiateButton(part, Leg);
                break;
            case BodyPart.Tail:
                instantiateButton(part, tail);
                break;
            default:
                Debug.Log("this doesn't exist");
                break;

        }
    }
    void savePart(AnimalPart part)
    {
        if (!InventoryManager.instance.getDict()[part.partData.id])
        {
            InventoryManager.instance.getDict()[part.partData.id] = !InventoryManager.instance.getDict()[part.partData.id];
            string str = InventoryManager.instance.ConvertToString();
            GameManager.instance.playerdata.inventoryStr = str;
            GameManager.SaveData();
        }
    }
    void instantiateButton(AnimalPart p, GameObject place)
    {
        try
        {
            if (inventoryToggles.ContainsKey(p.partData.id)) return;
            GameObject tmp = Instantiate(ButtonTemplate);
            tmp.transform.parent = place.transform;
            tmp.GetComponent<Toggle>().group = place.GetComponent<ToggleGroup>();
            Image[] children = tmp.transform.GetComponentsInChildren<Image>();

            print("got children");
            int child =0;
            for (int i = 0; i < children.Length; i++)
            {
                if (children[i].gameObject.name.Equals("Icon"))
                {
                    child = i;
                }
            }

            children[child].sprite = p.partData.image;

            tmp.GetComponent<Toggle>().onValueChanged.AddListener((e) =>
            {
                if (initialized) {
                    if (e)
                    {
                        Debug.Log(p.name);
                        thing.ChangeBodyPart(p.partData.id);
                        InventoryManager.instance.SetStat(p.partData.bodyPart, p.GetAbility());
                        //Routine.Start(NameGeneratorAsync());
                    }
                }
                
            });
            if (!inventoryToggles.ContainsKey(p.partData.id)) inventoryToggles.Add(p.partData.id, tmp.GetComponent<Toggle>());
            else Debug.LogWarning("Multiple buttons of same name found!");
        }
        catch (Exception e)
        {
            //In the case that pUI doesn't exist'
            print(e);
        }
    }

    //public IEnumerator NameGeneratorAsync() {
    //    yield return 0.7F;
    //    GameManager.instance.NameGenerator(thing.GetCreatedAnimal());
    //}

    public void LoadAnimal(AnimalPartsObject animal)
    {
        foreach (var toggle in inventoryToggles.Values) toggle.SetIsOnWithoutNotify(false);
        inventoryToggles[animal.headID].SetIsOnWithoutNotify(true);
        inventoryToggles[animal.bodyID].SetIsOnWithoutNotify(true);
        inventoryToggles[animal.legsID].SetIsOnWithoutNotify(true);
        inventoryToggles[animal.tailID].SetIsOnWithoutNotify(true);

        thing.CreateAnimal(animal, true, true, AnimalPrefabBuilder.AnimationType.Bob);
    }

    public void CloseAnimal(bool save)    // called when closed
    {
        if (GameManager.instance && save) GameManager.instance.playerdata.SaveActiveAnimal(thing.GetCreatedAnimal());
        thing.DestroyAnimal();
    }
}
