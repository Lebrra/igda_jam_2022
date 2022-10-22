using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    Dictionary<String, bool> partDict = new Dictionary<String, bool>();
    [SerializeField]
    bool Testing;
    [SerializeField]
    bool hasPart;
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
            //ConvertFromString(); // String gotten from
        }
        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        //Destroys itself if in menu
        /*
        if (SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("Title")))
        {
            Destroy(this);
        }
        */

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
            //Debug.Log(partlist[i]);
            partDict.Add(partlist[i].partData.id, hasPart);
        }/*
        foreach (KeyValuePair<String, bool> i in partDict)
        {
            //Debug.Log("Dictionary " +i.Key + "," + i.Value);
        }*/
       
            
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
            str += i.Key + "," + i.Value + ",";
        }
        
        return str;
    }
    public void ConvertFromString(string str)
    {
        
        string[] strrr = str.ToString().Split(',');
        print(strrr.Length + "Strrrr Length");
        bool tf;
        for (int i = 0; i < strrr.Length; i = i+2)
        {
            if (strrr[i + 1] == "true")
                tf = true;
            else
                tf = false;
            partDict.Add(strrr[i],  tf);
        }
        
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
