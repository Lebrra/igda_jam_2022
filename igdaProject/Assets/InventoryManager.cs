using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    Dictionary<String, bool> partDict = new Dictionary<String, bool>();
    bool Testing;
    [SerializeField]
    bool hasPart = false;
    AnimalPart[] partlist;
    bool newGame = true;
    //Exportable as String for Save Data
    //Use String Methods with Comma as Delimiter
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        instance = this;
        else Destroy(this);

        if(Testing)
        {
            testFill();
        }
        
        else
        {
            productionFill();
        }
        if (!newGame)
        {
            ConvertFromString();
        }
        DontDestroyOnLoad(this);
    }

    private void productionFill()
    {
        hasPart = false;
        getPartResource();
    }

    private void testFill()
    {
        hasPart = true;
        getPartResource();
    }
    private void getPartResource()
    {
        partlist = Resources.LoadAll<AnimalPart>("Parts/Data");
        for (int i = 0; i < partlist.Length; i++)
        {
            Debug.Log(partlist[i]);
            partDict.Add(partlist[i].partData.id, hasPart);
        }
        foreach (KeyValuePair<String, bool> i in partDict)
        {
            Debug.Log("Dictionary " +i.Key + "," + i.Value);
        }
       
            
    }
    public void updateNewGame(bool a)
    {
        newGame = a;
    }
    //Used to Save the player inventory state
    public String ConvertToString()
    {
        String str = "";
        foreach (KeyValuePair<String, bool> i in partDict)
        {
           // Debug.Log("Dictionary " + i.Key + "," + i.Value);
            str += i.Key + "," + i.Value + ",";
        }
        /*
        String[] strrr = str.ToString().Split(',');
        print(strrr.Length + "Strrrr Length");
        for (int i = 0; i < strrr.Length; i++)
        {
            Debug.Log(strrr[i]);
        }
        */
        return str;
    }
    void ConvertFromString()
    {

    }
    public void updateInventory(String part)
    {
        partDict[part] = true;
    }
    public Dictionary<String, bool> getDict()
    {
        return partDict;
    }

}
