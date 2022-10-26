using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class Ability : ScriptableObject
{
    [System.Serializable]
    public struct AbilityData {
        public string name;
        public int abilityCost;
        public string description;
        public AbilityType type;

        public int health, mana, dodge, speed, crit, attack;
        public string targetOpponent;
    }

    public AbilityData abilityData;
    private void LoadData(AbilityData data) {

        abilityData.name = data.name;
        abilityData.abilityCost = data.abilityCost;
        abilityData.description = data.description;
        abilityData.type = data.type;
        abilityData.health = data.health;
        abilityData.mana = data.mana;
        abilityData.dodge = data.dodge;
        abilityData.speed = data.speed;
        abilityData.crit = data.crit;
        abilityData.attack = data.attack;
        abilityData.targetOpponent = data.targetOpponent;
    }

    public static void CreateAsset(AbilityData newabilityData) {
#if UNITY_EDITOR
        Ability asset = CreateInstance<Ability>();
        asset.LoadData(newabilityData);
        string assetName = asset.abilityData.name.Trim(' ').Trim('!').Trim('?').ToLower();
        AssetDatabase.CreateAsset(asset, "Assets/Resources/Abilities/" + assetName + ".asset");

        Debug.Log("Asset created: " + asset.abilityData.name);
#endif
    }

    public static AbilityType StringToAbility(string abilityString) {
        switch(abilityString.ToLower().Trim(' ')) {
            case "attack":  return AbilityType.attack;
            case "support": return AbilityType.support;
            case "passive": return AbilityType.passive;
        }

        return AbilityType.none;
    }

    public virtual void UseAbility() {

    }

}


public enum AbilityType {
    attack, 
    support, 
    passive, 
    none
}