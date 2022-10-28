using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Serializable]
    public struct StageContainer
    {
        public List<Biome> stages;
    }

    [SerializeField]
    List<StageContainer> allStages;

    public LevelData GenerateNewLevel()
    {
        LevelData newLevel = new LevelData();
        newLevel.selectedStages = new List<string>();
        for (int i = 0; i < allStages.Count; i++)
        {
            int randomSelect = UnityEngine.Random.Range(0, allStages[i].stages.Count);
            newLevel.selectedStages.Add(BiomeToString(allStages[i].stages[randomSelect]));
        }

        newLevel.currentGeneratedOpponent = CreateOpponent(StringToBiome(newLevel.selectedStages[0]));
        newLevel.currentStage = 0;
        newLevel.currentMatch = 0;
        newLevel.activeRun = true;

        return newLevel;
    }

    public AnimalPartsObject CreateOpponent(string biome)
    {
        return CreateOpponent(StringToBiome(biome));
    }

    public static AnimalPartsObject CreateOpponent(Biome biome)
    {
        AnimalPartsObject newAnimal = new AnimalPartsObject();
        int randomChosen = UnityEngine.Random.Range(0, biome.animalCodes.Count);
        newAnimal.headID = biome.animalCodes[randomChosen] + "_head";

        randomChosen = UnityEngine.Random.Range(0, biome.animalCodes.Count);
        newAnimal.bodyID = biome.animalCodes[randomChosen] + "_body";

        randomChosen = UnityEngine.Random.Range(0, biome.animalCodes.Count);
        newAnimal.legsID = biome.animalCodes[randomChosen] + "_legs";

        if (biome.animalCodes.Contains("frog"))
        {
            string notFrog = "frog_tail";
            while (notFrog == "frog_tail")
            {
                randomChosen = UnityEngine.Random.Range(0, biome.animalCodes.Count);
                notFrog = biome.animalCodes[randomChosen] + "_tail";
            }
            newAnimal.tailID = notFrog;
        }
        else if (biome.animalCodes.Contains("beetle"))
        {
            string notBeetle = "beetle_tail";
            while (notBeetle == "beetle_tail")
            {
                randomChosen = UnityEngine.Random.Range(0, biome.animalCodes.Count);
                notBeetle = biome.animalCodes[randomChosen] + "_tail";
            }
            newAnimal.tailID = notBeetle;
        }
        else
        {
            randomChosen = UnityEngine.Random.Range(0, biome.animalCodes.Count);
            newAnimal.tailID = biome.animalCodes[randomChosen] + "_tail";
        }

        Debug.Log("Created new level");
        return newAnimal;
    }

    /// <summary>
    /// Returns (stage, match)
    /// </summary>
    public static (int, int) IncrementMatch(int stage, int match)
    {
        if (match == 2) return (stage + 1, 0);
        else return (stage, match + 1);
    }

    public Biome StringToBiome(string biomeStr)
    {
        foreach (var stages in allStages)
        {
            foreach (var biome in stages.stages)
            {
                if (biome.name.ToLower() == biomeStr)
                {
                    return biome;
                }
            }
        }

        return null;
    }

    public string BiomeToString(Biome biome)
    {
        return biome.name.ToString().ToLower();
    }
}

[System.Serializable]
public struct LevelData
{
    public bool activeRun;
    public List<string> selectedStages;
    public AnimalPartsObject currentGeneratedOpponent;
    public int currentStage;
    public int currentMatch;    // if round == 2 then BOSS
}
