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

        public int health, mana, dodge, speed, crit, attack;

    }

    public AbilityData abilityData;
    private void LoadData(AbilityData data) {

        abilityData.name = data.name;
        abilityData.abilityCost = data.abilityCost;
        abilityData.description = data.description;
        abilityData.health = data.health;
        abilityData.mana = data.mana;
        abilityData.dodge = data.dodge;
        abilityData.speed = data.speed;
        abilityData.crit = data.crit;
        abilityData.attack = data.attack;

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