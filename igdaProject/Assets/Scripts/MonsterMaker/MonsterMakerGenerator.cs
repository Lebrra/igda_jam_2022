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
    Dictionary<string,AnimalPart> partListdict;
    GameObject[] partObjectList;
    Dictionary<string,AnimalPartUI> partListObjectDict;
    [SerializeField]
    GameObject ButtonTemplate;
    // Start is called before the first frame update
    void Start()
    {
        partlist = Resources.LoadAll<AnimalPart>("Parts/Data");
        partObjectList = Resources.LoadAll<GameObject>("Parts/Prefabs");
        foreach(GameObject o in head.GetComponentsInChildren<GameObject>())
        {
            Destroy(this);
        }
        fillDictionaries();
        
        //UpdatePetParts();
    }
    
    void fillDictionaries()
    {
        for(int i = 0; i < partlist.Length; i++)
        {
            partListdict.Add(partlist[i].partData.id, partlist[i]);
        }
        for(int j = 0; j < partObjectList.Length; j++)
        {
            partListObjectDict.Add(partObjectList[j].GetComponent<AnimalPartUI>().id, partObjectList[j].GetComponent<AnimalPartUI>());
        }
    }
    void Setup()
    {
        foreach (KeyValuePair<string, bool> inst in InventoryManager.instance.getDict())
        {
            if (inst.Value)
            {
                //if you have the part
                string id = inst.Key;
                AnimalPart part = partListdict[id];
                AnimalPartUI partUI = partListObjectDict[id];
                UpdatePetParts(part, partUI);
            }
        }
    }
    void UpdatePetParts(AnimalPart part, AnimalPartUI partUI)
    {
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
            ButtonTemplate.GetComponent<Toggle>().group = place.GetComponent<ToggleGroup>();
            GameObject[] children = ButtonTemplate.GetComponentsInChildren<GameObject>();
            GameObject child = new GameObject();
            for (int i = 0; i < children.Length; i++)
            {
                if (children[i].name.Equals("Icon"))
                {
                    child = children[i];
                }
            }
            child.GetComponent<Image>().sprite = pUI.animalIcon.sprite;
            GameObject tmp = Instantiate(ButtonTemplate);
            tmp.transform.parent = place.transform;
        }
        catch (Exception e)
        {
            //In the case that pUI doesn't exist
            print("Image Doesn't Exist yet");
        }
    }
}
