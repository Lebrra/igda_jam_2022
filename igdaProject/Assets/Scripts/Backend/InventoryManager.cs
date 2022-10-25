using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [Header("Stats")]
    [SerializeField]
    EditorStat[] stats; // head = 0, body = 1, legs = 2, tail = 3 (set bonus = 4?)


    [Serializable]
    public struct EditorStat
    {
        public TextMeshProUGUI header;
        public TextMeshProUGUI description;

        public GameObject healthIcon;
        public TextMeshProUGUI healthText;
        public GameObject manaIcon;
        public TextMeshProUGUI manaText;
        public GameObject speedIcon;
        public TextMeshProUGUI speedText;
        public GameObject dodgeIcon;
        public TextMeshProUGUI dodgeText;
    }

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
        var animal = GameManager.instance.playerdata.GetActiveAnimal();
        generator.LoadAnimal(animal);

        // set all stats
        var partData = DataManager.instance.GetAnimalPart(animal.headID);
        SetStat(partData.partData.bodyPart, partData.GetAbility());
        partData = DataManager.instance.GetAnimalPart(animal.bodyID);
        SetStat(partData.partData.bodyPart, partData.GetAbility());
        partData = DataManager.instance.GetAnimalPart(animal.legsID);
        SetStat(partData.partData.bodyPart, partData.GetAbility());
        partData = DataManager.instance.GetAnimalPart(animal.tailID);
        SetStat(partData.partData.bodyPart, partData.GetAbility());
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

    public void SetStat(BodyPart part, Ability ability)
    {
        var stat = stats[(int)part];

        stat.header.text = ability.abilityData.name + " - " + ability.abilityData.type;
        stat.description.text = ability.abilityData.description;

        stat.healthIcon.SetActive(false);
        stat.manaIcon.SetActive(false);
        stat.speedIcon.SetActive(false);
        stat.dodgeIcon.SetActive(false);

        if (ability.abilityData.type == AbilityType.passive)
        {
            // enable any icons
            if (ability.abilityData.health != 0)
            {
                stat.healthIcon.SetActive(true);
                var plus = ability.abilityData.health > 0 ? "+" : "";
                stat.healthText.text = plus + ability.abilityData.health;
            }
            if (ability.abilityData.mana != 0)
            {
                stat.manaIcon.SetActive(true);
                var plus = ability.abilityData.mana > 0 ? "+" : "";
                stat.manaText.text = plus + ability.abilityData.mana;
            }
            if (ability.abilityData.speed != 0)
            {
                stat.speedIcon.SetActive(true);
                var plus = ability.abilityData.speed > 0 ? "+" : "";
                stat.speedText.text = plus + ability.abilityData.speed;
            }
            if (ability.abilityData.dodge != 0)
            {
                stat.dodgeIcon.SetActive(true);
                var plus = ability.abilityData.dodge > 0 ? "+" : "";
                stat.dodgeText.text = plus + ability.abilityData.dodge;
            }
        }
    }
}
