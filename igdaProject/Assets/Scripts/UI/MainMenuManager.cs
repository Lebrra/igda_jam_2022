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
    }

    public void Open()
    {
        if (isOpen) return;
        isOpen = true;

        menuAnim.SetBool("UiStatus", true);
        presets = GameManager.instance.playerdata.animalPresets;

        currentPreset = GameManager.instance.playerdata.selectedPreset;
        LoadAnimalPreset(currentPreset, true);
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
}
