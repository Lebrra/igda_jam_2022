using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public bool sfxEnabled = true;
    public bool musicEnabled = true;

    // everything that needs to be saved here

    public string inventoryStr;
    public AnimalPartsObject[] animalPresets;   // default these, always 3

    // progress saving
}
