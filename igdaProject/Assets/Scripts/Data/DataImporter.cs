using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

#if UNITY_EDITOR
public class DataImporter : Editor
{
    [MenuItem("Custom Editor/Reload Animal Parts")]
    public static void ReloadParts()
    {
        Debug.Log("Importing Data...");

        StreamReader sr = new StreamReader(Application.dataPath + "/Data/partData.csv");
        sr.ReadLine();  // remove headers

        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine().Split(',');
            for (int i = 0; i < line.Length; i++) line[i] = line[i].Trim(' ');
            AnimalPart.CreateAsset(ArrayToAnimalPartData(line));
        }

        Debug.Log("Completed import.");
    }

    [MenuItem("Custom Editor/Reload Abilities")]
    public static void ReloadAbilities() {
        Debug.Log("Importing Data...");

        StreamReader sr = new StreamReader(Application.dataPath + "/Data/abilityData.csv");
        sr.ReadLine();  // remove headers
        sr.ReadLine();  // remove headers

        while (!sr.EndOfStream) {
            var line = sr.ReadLine().Split(',');
            for (int i = 0; i < line.Length; i++) line[i] = line[i].Trim(' ');
            if (line[0] == "") continue;
            Ability.CreateAsset(ArrayToAbilityData(line));
        }

        Debug.Log("Completed import.");
    }

    //string[] headerNames = new string[8] { "id", "Animal", "Part", "AnimalNamePart", "Biome", "AbilityName" };
    static AnimalPart.AnimalPartData ArrayToAnimalPartData(string[] lineList)
    {
        AnimalPart.AnimalPartData newPartData;

        newPartData.id = lineList[0];
        newPartData.animal = lineList[1];
        newPartData.bodyPart = AnimalPart.StringToPart(lineList[2]);
        newPartData.namePart = lineList[3];
        newPartData.abilityName = lineList[5];
        newPartData.image = null;

        return newPartData;
    }

    static Ability.AbilityData ArrayToAbilityData(string[] list) {
        Ability.AbilityData d = new Ability.AbilityData();

        d.name = list[0];
        int.TryParse(list[1], out d.abilityCost);
        d.description = list[2];
        d.type = Ability.StringToAbility(list[3]);
        int.TryParse(list[4], out d.health);
        int.TryParse(list[5], out d.mana);
        int.TryParse(list[6], out d.dodge);
        int.TryParse(list[7], out d.speed);
        int.TryParse(list[8], out d.crit);
        int.TryParse(list[9], out d.attack);
        //bool.TryParse(list[10], out d.targetOpponent);
        d.targetOpponent = list[10];

        return d;
    }

}
#else
public class DataImporter { }
#endif
