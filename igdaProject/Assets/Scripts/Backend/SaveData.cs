using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public bool sfxEnabled = true;
    public bool musicEnabled = true;

    // everything that needs to be saved here
    public bool Tutorial {get;set;}
    public string inventoryStr;
    public AnimalPartsObject[] animalPresets;   // default these, always 3
    public int selectedPreset = 0;

    // progress saving
    public LevelData currentLevel;

    public AnimalPartsObject GetActiveAnimal()
    {
        return animalPresets[selectedPreset];
    }

    public void SaveActiveAnimal(AnimalPartsObject animal)
    {
        animalPresets[selectedPreset] = animal;
        GameManager.SaveData();
    }

    public List<Ability> GetAllAbilities()
    {
        List<Ability> abilities = new List<Ability>();
        var animal = GetActiveAnimal();
        abilities.Add(DataManager.instance.GetAnimalPart(animal.headID).GetAbility());
        abilities.Add(DataManager.instance.GetAnimalPart(animal.bodyID).GetAbility());
        abilities.Add(DataManager.instance.GetAnimalPart(animal.legsID).GetAbility());
        abilities.Add(DataManager.instance.GetAnimalPart(animal.tailID).GetAbility());

        return abilities;
    }
}
