using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class Ability : ScriptableObject
{
    public struct AbilityData {
        public string name;
        public int abilityCost;
        public string description;
        public AbilityType type;
        public int amount; //amount for the ability to affect
    }

    public AbilityData abilityData;
    private void LoadData(AbilityData data) {

        abilityData.name = data.name;
        abilityData.abilityCost = data.abilityCost;
        abilityData.description = data.description;
        abilityData.amount = data.amount;

    }

    public static void CreateAsset(AbilityData newabilityData) {
        Ability asset = CreateInstance<Ability>();
        asset.LoadData(newabilityData);
        AssetDatabase.CreateAsset(asset, "Assets/Resources/Parts/Data/" + asset.abilityData.name + ".asset");

        Debug.Log("Asset created: " + asset.abilityData.name);
    }

    public static AbilityType StringToAbility(string abilityString) {
        switch(abilityString.ToLower().Trim(' ')) {
            case "attack":  return AbilityType.attack;
            case "defense": return AbilityType.defense;
            case "passive": return AbilityType.passive;
        }

        return AbilityType.none;
    }



}


public enum AbilityType {
    attack, 
    defense, 
    passive, 
    none
}