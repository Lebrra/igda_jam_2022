using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    AnimalPart[] partlist;
    Dictionary<string,AnimalPart> partListdict= new Dictionary<string, AnimalPart>();
    GameObject[] partObjectList;
    Dictionary<string,AnimalPartUI> partListObjectDict = new Dictionary<string, AnimalPartUI>();
    [SerializeField]
    GameObject ButtonTemplate;
    // Start is called before the first frame update
    void Start()
    {
        partlist = Resources.LoadAll<AnimalPart>("Parts/Data");
        partObjectList = Resources.LoadAll<GameObject>("Parts/Prefabs");
        bool done = false;
        foreach(RectTransform o in head.GetComponentsInChildren<RectTransform>())
        {
            if (!done)
                done = true;
            else
            {
                Destroy(o.gameObject);
                done = true;
            }
        }
        done = false;
        foreach (RectTransform o in tail.GetComponentsInChildren<RectTransform>())
        {
            if (!done)
                done = true;
            else
            {
                Destroy(o.gameObject);
                done = true;
            }
        }
        done = false;
        foreach (RectTransform o in body.GetComponentsInChildren<RectTransform>())
        {
            if (!done)
                done = true;
            else
            {
                Destroy(o.gameObject);
                done = true;
            }
        }
        done = false;
        foreach (RectTransform o in Leg.GetComponentsInChildren<RectTransform>())
        {
            if (!done)
                done = true;
            else
            {
                Destroy(o.gameObject);
                done = true;
            }
        }
        fillDictionaries();
        Setup();
        //UpdatePetParts();
    }
    
    void fillDictionaries()
    {
        try
        {
            for (int i = 0; i < partlist.Length; i++)
            {
                if(!partListdict.ContainsKey(partlist[i].partData.id))
                partListdict.Add(partlist[i].partData.id, partlist[i]);
            }
            for (int j = 0; j < partObjectList.Length; j++)
            {
                if(!partListObjectDict.ContainsKey(partObjectList[j].GetComponent<AnimalPartUI>().id))
                partListObjectDict.Add(partObjectList[j].GetComponent<AnimalPartUI>().id, partObjectList[j].GetComponent<AnimalPartUI>());
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

    }
    void Setup()
    {
        try
        {
            AnimalPart part= new AnimalPart();
            AnimalPartUI partUI = new AnimalPartUI();
            print("made it to setup.");
            print(InventoryManager.instance.getDict());
            foreach (KeyValuePair<string, bool> inst in InventoryManager.instance.getDict())
            {
                if (inst.Value)
                {
                    //print(inst);
                    //if you have the part
                    string id = inst.Key;
                    if(partListdict.ContainsKey(id))
                    part = partListdict[id];
                    if(partListObjectDict.ContainsKey(id))
                    partUI = partListObjectDict[id];
                    UpdatePetParts(part, partUI);
                    
                }
            }
        }
        catch(Exception ex)
        {
            Debug.LogException(ex);
        }
    }
    void UpdatePetParts(AnimalPart part, AnimalPartUI partUI)
    {
        print("update pet parts");
        switch(part.partData.bodyPart)
        {
            case BodyPart.Body:
                instantiateButton(part, partUI, body);
                break;
            case BodyPart.Head:
                instantiateButton(part, partUI, head);
                break;
            case BodyPart.Legs:
                instantiateButton(part, partUI, Leg);
                break;
            case BodyPart.Tail:
                instantiateButton(part, partUI, tail);
                break;
            default:
                Debug.Log("this doesn't exist");
                break;

        }
    }
    void instantiateButton(AnimalPart p , AnimalPartUI pUI, GameObject place)
    {
        try
        {
            print("I made it");
            print(ButtonTemplate);
            GameObject tmp = Instantiate(ButtonTemplate);
            tmp.transform.parent = place.transform;
            tmp.GetComponent<Toggle>().group = place.GetComponent<ToggleGroup>();
            GameObject[] children = tmp.GetComponentsInChildren<GameObject>();
            GameObject child = new GameObject();
            for (int i = 0; i < children.Length; i++)
            {
                if (children[i].name.Equals("Icon"))
                {
                    child = children[i];
                }
            }
            child.GetComponent<Image>().sprite = pUI.animalIcon;
        }
        catch (Exception e)
        {
            //In the case that pUI doesn't exist'
            print(e);
            print("Image Doesn't Exist yet");
        }
    }
}
