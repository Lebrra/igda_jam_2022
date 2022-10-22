using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class AnimalPart : ScriptableObject
{
    #region Related Types
    

    [Serializable]
    public struct AnimalPartData
    {
        public string id;
        public string animal;
        public string namePart;
        public BodyPart bodyPart;
        public string abilityName;
        public int abilityCost;
        public int health;
        public string description;
        public Sprite image;
    }
    #endregion

    public AnimalPartData partData;

    #region Asset Creation/Parsing
    void LoadData(AnimalPartData newPartData)
    {
        partData.id = newPartData.id;
        partData.animal = newPartData.animal;
        partData.namePart = newPartData.namePart;
        partData.bodyPart = newPartData.bodyPart;
        partData.abilityName = newPartData.abilityName;
        partData.abilityCost = newPartData.abilityCost;
        partData.health = newPartData.health;
        partData.description = newPartData.description;
    }

    public static void CreateAsset(AnimalPartData newPartData)
    {
        AnimalPart asset = CreateInstance<AnimalPart>();
        asset.LoadData(newPartData);
        AssetDatabase.CreateAsset(asset, "Assets/Resources/Parts/Data/" + asset.partData.id + ".asset");
        Debug.Log("Asset created: " + asset.partData.id);
    }

    public static BodyPart StringToPart(string partString)
    {
        switch (partString.ToLower().Trim(' '))
        {
            case "head": return BodyPart.Head;
            case "body": return BodyPart.Body;
            case "legs": return BodyPart.Legs;
            case "tail": return BodyPart.Tail;
        }
        return BodyPart.None;
    }

    public static AnimalPartsObject AnimalToPartsObj(string animal)
    {
        string formatedAnim = animal.ToLower().Replace(" ", "");

        var animalObj = new AnimalPartsObject();
        animalObj.headID = formatedAnim + "_head";
        animalObj.bodyID = formatedAnim + "_body";
        animalObj.legsID = formatedAnim + "_legs";
        animalObj.tailID = formatedAnim + "_tail";

        return animalObj;
    }

    #endregion
}

