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

    //string[] headerNames = new string[8] { "id", "Animal", "Part", "AnimalNamePart", "Biome", "AbilityName", "AbilityCost", "Health", "Description" };
    static AnimalPart.AnimalPartData ArrayToAnimalPartData(string[] lineList)
    {
        AnimalPart.AnimalPartData newPartData;

        newPartData.id = lineList[0];
        newPartData.animal = lineList[1];
        newPartData.namePart = lineList[3];
        newPartData.bodyPart = AnimalPart.StringToPart(lineList[2]);
        newPartData.abilityName = lineList[5];
        int.TryParse(lineList[6], out newPartData.abilityCost);
        int.TryParse(lineList[7], out newPartData.health);
        newPartData.description = lineList[8];

        return newPartData;
    }
}
#else
public class DataImporter { }
#endif
