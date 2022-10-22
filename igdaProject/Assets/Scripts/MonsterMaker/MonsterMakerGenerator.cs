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
    public static MonsterMakerGenerator instance;

    Dictionary< string, bool> apple = new Dictionary<string, bool>();
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        instance = this;
        else
            Destroy(this);
        DontDestroyOnLoad(this);
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
    void instantiateButton(AnimalPart p, GameObject place)
    {
        try
        {
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
            children[child].GetComponent<Image>().sprite = p.partData.image;
            
        }
        
        catch (Exception e)
        {
            //In the case that pUI doesn't exist'
            print(e);
            print("Image Doesn't Exist yet");
        }
    }
}