using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class MainMenuManager : MonoBehaviour
{
    bool isOpen = false;

    [SerializeField]
    Animator menuAnim;

    [SerializeField]
    Button settingsButton;

    [SerializeField]
    Button editButton;
    [SerializeField]
    Button newLevelButton;
    [SerializeField]
    Button continueButton;
    [SerializeField]
    TextMeshProUGUI continueText;
    bool runActive = false;
    [SerializeField]
    Button randomButton;

    [Header("Preset Swapping")]
    [SerializeField]
    Toggle[] presetSnapButtons;
    [SerializeField]
    Button presetLeft;
    [SerializeField]
    Button presetRight;

    [Header("Part Tooltips")]
    [SerializeField]
    TextMeshProUGUI headHeader;
    [SerializeField]
    TextMeshProUGUI headDesc;
    [SerializeField]
    TextMeshProUGUI bodyHeader;
    [SerializeField]
    TextMeshProUGUI bodyDesc;
    [SerializeField]
    TextMeshProUGUI legsHeader;
    [SerializeField]
    TextMeshProUGUI legsDesc;
    [SerializeField]
    TextMeshProUGUI tailHeader;
    [SerializeField]
    TextMeshProUGUI tailDesc;

    [Header("Stats")]
    [SerializeField]
    TextMeshProUGUI healthStat;
    [SerializeField]
    TextMeshProUGUI manaStat;
    [SerializeField]
    TextMeshProUGUI speedStat;
    [SerializeField]
    TextMeshProUGUI dodgeStat;
    [Space]
    [SerializeField]
    AnimalPrefabBuilder animalBuilder;

    AnimalPartsObject[] presets;
    int currentPreset = 0;

    private void Start()
    {
        presetSnapButtons[0].onValueChanged.AddListener((value) => LoadAnimalPreset(0));
        presetSnapButtons[1].onValueChanged.AddListener((value) => LoadAnimalPreset(1));
        presetSnapButtons[2].onValueChanged.AddListener((value) => LoadAnimalPreset(2));
        presetLeft.onClick.AddListener(DecrementPreset);
        presetRight.onClick.AddListener(IncrementPreset);

        settingsButton.onClick.AddListener(() => GameDirector.instance.OpenSettings());
        randomButton.onClick.AddListener(() => GameDirector.instance.OpenCombatPreview());
        // TODO: button handling for new/continue buttons
        // if new and has continue, warning popup
        editButton.onClick.AddListener(() => GameDirector.instance.OpenEditor());
        newLevelButton.onClick.AddListener(NewLevel);
        continueButton.onClick.AddListener(ContinueLevel);
    }

    public void Open()
    {
        if (isOpen) return;
        isOpen = true;

        menuAnim.SetBool("UiStatus", true);
        presets = GameManager.instance.playerdata.animalPresets;

        currentPreset = GameManager.instance.playerdata.selectedPreset;
        LoadAnimalPreset(currentPreset, true);

        runActive = GameManager.instance.playerdata.currentLevel.activeRun;
        continueButton.gameObject.SetActive(runActive);
        continueText.text = GetContinueText();
    }

    public void Close()
    {
        if (!isOpen) return;

        menuAnim.SetBool("UiStatus", false);
        animalBuilder.DestroyAnimal();

        isOpen = false;
    }

    public void IncrementPreset()
    {
        LoadAnimalPreset((currentPreset + 1) % 3);
    }

    public void DecrementPreset()
    {
        LoadAnimalPreset((currentPreset + 2) % 3);
    }

    public void ForceToggleState(int index)
    {
        for (int i = 0; i < presetSnapButtons.Length; i++)
        {
            if (i == index) presetSnapButtons[i].SetIsOnWithoutNotify(true);
            else presetSnapButtons[i].SetIsOnWithoutNotify(false);
        }
    }

    public void LoadAnimalPreset(int index, bool forceLoad = true)
    {
        if (!forceLoad)
        {
            if (index < 0 || index > 2)
            {
                Debug.LogError("Invalid preset index: " + index);
                return;
            }
            else if (index == currentPreset) return;
        }

        Debug.Log("Setting toggle index : " + index);
        currentPreset = GameManager.instance.playerdata.selectedPreset = index;
        ForceToggleState(currentPreset);

        animalBuilder.CreateAnimal(presets[index], true, true, AnimalPrefabBuilder.AnimationType.Bob);

        // load tooltips
        Ability[] abilities = new Ability[4];
        var animalPart = DataManager.instance.GetAnimalPart(presets[index].headID);
        abilities[0] = animalPart.GetAbility();
        headHeader.text = GetAnimalPartTerm(animalPart) + " - " + abilities[0].abilityData.name;
        headDesc.text = abilities[0].abilityData.description;

        animalPart = DataManager.instance.GetAnimalPart(presets[index].bodyID);
        abilities[1] = animalPart.GetAbility();
        bodyHeader.text = GetAnimalPartTerm(animalPart) + " - " + abilities[1].abilityData.name;
        bodyDesc.text = abilities[1].abilityData.description;

        animalPart = DataManager.instance.GetAnimalPart(presets[index].legsID);
        abilities[2] = animalPart.GetAbility();
        legsHeader.text = GetAnimalPartTerm(animalPart) + " - " + abilities[2].abilityData.name;
        legsDesc.text = abilities[2].abilityData.description;

        animalPart = DataManager.instance.GetAnimalPart(presets[index].tailID);
        abilities[3] = animalPart.GetAbility();
        tailHeader.text = GetAnimalPartTerm(animalPart) + " - " + abilities[3].abilityData.name;
        tailDesc.text = abilities[3].abilityData.description;
        SetStats(abilities);
    }

    string GetAnimalPartTerm(AnimalPart data)
    {
        return data.partData.animal + " " + data.partData.bodyPart;
    }

    string GetContinueText()
    {
        var currentRun = GameManager.instance.playerdata.currentLevel;
        if (!currentRun.activeRun) return "INVALID";

        string match;
        switch (currentRun.currentMatch)
        {
            case 0:
                match = "1";
                break;
            case 1:
                match = "2";
                break;
            default:
                match = "BOSS";
                break;
        }

        return "Back to Battle\n" + currentRun.selectedStages[currentRun.currentStage].name + ": " + match;
    }

    void SetStats(Ability[] abilities)
    {
        int health = GameManager.BASE_HEALTH;
        int mana = GameManager.BASE_MANA;
        int speed = GameManager.BASE_SPEED;
        int dodge = GameManager.BASE_DODGE;
        foreach(var ability in abilities)
        {
            health += ability.abilityData.health;
            mana += ability.abilityData.mana;
            speed += ability.abilityData.speed;
            dodge += ability.abilityData.dodge;
        }
        healthStat.text = health.ToString();
        manaStat.text = mana.ToString();
        speedStat.text = speed.ToString();
        dodgeStat.text = dodge.ToString();
    }

    public void NewLevel()
    {
        if (runActive)
        {
            // popup
            var popup = GameManager.instance.GeneralPopup;
            popup.FillContent("Starting a new level will delete your progress on the currently save one, are you sure?", () => {
                GameDirector.instance.OpenLevel(true);
                popup.Close();
            }, popup.Close);
            popup.Open();
        }
        else
        {
            // no popup
            GameDirector.instance.OpenLevel(true);
        }
    }

    public void ContinueLevel()
    {
        if (!runActive)
        {
            Debug.LogError("Tried to continue a game but there isn't one!");
            continueButton.gameObject.SetActive(false);
        }
        else
        {
            // no popup
            GameDirector.instance.OpenLevel(false);
        }
    }
}
