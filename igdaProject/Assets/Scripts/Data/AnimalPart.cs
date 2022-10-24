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
        public Sprite image;
    }
    #endregion

    public AnimalPartData partData;

    public Ability GetAbility()
    {
        string parsedName = partData.abilityName.Trim(' ').Trim('!').Trim('?').ToLower();
        Ability ability = DataManager.instance.GetAbility(parsedName);
        if (ability) return ability;
        else
        {
            Debug.LogError("Unable to find ability: " + parsedName);
            return null;
        }
    }

    #region Asset Creation/Parsing
    void LoadData(AnimalPartData newPartData, Sprite sprite)
    {
        partData.id = newPartData.id;
        partData.animal = newPartData.animal;
        partData.namePart = newPartData.namePart;
        partData.bodyPart = newPartData.bodyPart;
        partData.abilityName = newPartData.abilityName;
        partData.image = sprite;
    }

    public static void CreateAsset(AnimalPartData newPartData)
    {
#if UNITY_EDITOR
        var prevData = Resources.Load<AnimalPart>("Parts/Data/" + newPartData.id);
        Sprite savedSprite = null;
        if (prevData) savedSprite = prevData.partData.image;

        // TODO: if asset exists save sprite and put it back 
        AnimalPart asset = CreateInstance<AnimalPart>();
        asset.LoadData(newPartData, savedSprite);
        AssetDatabase.CreateAsset(asset, "Assets/Resources/Parts/Data/" + asset.partData.id + ".asset");
        Debug.Log("Asset created: " + asset.partData.id);
#endif
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

