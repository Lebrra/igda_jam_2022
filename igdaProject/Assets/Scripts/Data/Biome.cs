using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "Biome", menuName = "ScriptableObjects/Biome", order = 2)]
public class Biome : ScriptableObject
{
    public List<string> animalCodes;
    public Color32 color;

    public List<AnimalPartsObject> GetAllAnimalParts()
    {
        if (animalCodes == null) return null;
        else if (animalCodes.Count == 0) return null;

        List<AnimalPartsObject> animalList = new List<AnimalPartsObject>();
        foreach (var animal in animalCodes) animalList.Add(AnimalPart.AnimalToPartsObj(animal));

        return animalList;
    }
}
