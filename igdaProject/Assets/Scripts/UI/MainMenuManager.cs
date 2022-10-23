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

        // TODO: button handling for new/continue/edit/random buttons
        // if new and has continue, warning popup
        // random, popup to explain what it is
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

        animalBuilder.CreateAnimal(presets[index], true, true);

        // load tooltips
        var animalPart = Resources.Load<AnimalPart>("Parts/Data/" + presets[index].headID);
        headHeader.text = GetAnimalPartTerm(animalPart);
        headDesc.text = animalPart.partData.description;

        animalPart = Resources.Load<AnimalPart>("Parts/Data/" + presets[index].bodyID);
        bodyHeader.text = GetAnimalPartTerm(animalPart);
        bodyDesc.text = animalPart.partData.description;

        animalPart = Resources.Load<AnimalPart>("Parts/Data/" + presets[index].legsID);
        legsHeader.text = GetAnimalPartTerm(animalPart);
        legsDesc.text = animalPart.partData.description;

        animalPart = Resources.Load<AnimalPart>("Parts/Data/" + presets[index].tailID);
        tailHeader.text = GetAnimalPartTerm(animalPart);
        tailDesc.text = animalPart.partData.description;
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
}
