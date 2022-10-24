using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    [SerializeField]
    MonsterMakerGenerator generator;
    Dictionary<String, bool> partDict = new Dictionary<String, bool>();
    [SerializeField]
    bool Testing;
    [SerializeField]
    bool GenerateDefaultInventory = false;
    [SerializeField]
    bool hasPart;
    List<AnimalPart> partlist;
    bool newGame = true;
    bool initalized = false;

    [SerializeField]
    Button closeButton;
    [SerializeField]
    Button saveButton;
    [SerializeField]
    Animator anim;

    //Exportable as String for Save Data
    //Use String Methods with Comma as Delimiter
    // Start is called before the first frame update
    public IEnumerator Initialize(string inventory)
    {
        if (initalized) yield break;

        if (saveButton) saveButton.onClick.AddListener(() => GameDirector.instance.CloseEditor(true));
        if (closeButton) closeButton.onClick.AddListener(Close);

        if(instance == null)
        instance = this;
        else Destroy(this);

        if(Testing)
        {
            testFill();
        }
        else if (GenerateDefaultInventory)
        {
            getDefaultPartResource();
        }
        else
        {
            //productionFill();
            ConvertFromString(inventory);
        }
        yield return null;
        if (!newGame)
        {
            //ConvertFromString(); // String gotten from
        }
        DontDestroyOnLoad(this);

        yield return generator.Initialize();
        initalized = true;

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
        partlist = DataManager.instance.masterList;
        for (int i = 0; i < partlist.Count; i++)
        {
            //Debug.Log(partlist[i]);
            partDict.Add(partlist[i].partData.id, hasPart);
        }/*
        foreach (KeyValuePair<String, bool> i in partDict)
        {
            //Debug.Log("Dictionary " +i.Key + "," + i.Value);
        }*/
    }

    private void getDefaultPartResource()
    {
        partlist = DataManager.instance.masterList;
        for (int i = 0; i < partlist.Count; i++)
        {
            //Debug.Log(partlist[i]);
            var animal = partlist[i].partData.animal.ToLower().Trim(' ');
            if (animal == "cat" || animal == "dog") partDict.Add(partlist[i].partData.id, true);
            else partDict.Add(partlist[i].partData.id, false);
        }
        Debug.Log(ConvertToString());
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
        for (int i = 0; i < strrr.Length - 1; i = i+2)
        {
            if (strrr[i + 1].ToLower() == "true")
                tf = true;
            else
                tf = false;
            if (!partDict.ContainsKey(strrr[i])) partDict.Add(strrr[i], tf);
            else partDict[strrr[i]] = tf;
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


    public void Open()
    {
        //load animal
        anim.SetBool("Status", true);
        generator.LoadAnimal(GameManager.instance.playerdata.GetActiveAnimal());
    }

    public void SaveAndClose()
    {
        // save animal
        anim.SetBool("Status", false);
        generator.CloseAnimal(true);
    }

    public void Close()
    {
        var popup = GameManager.instance.GeneralPopup;
        popup.FillContent("You have unsaved changes!\nAre you sure you want to leave?",
            () => {
                GameDirector.instance.CloseEditor(false);
                popup.Close();
                },
            popup.Close);
        popup.Open();
    }

    public void ForceClose()
    {
        anim.SetBool("Status", false);
        generator.CloseAnimal(false);
    }
}
