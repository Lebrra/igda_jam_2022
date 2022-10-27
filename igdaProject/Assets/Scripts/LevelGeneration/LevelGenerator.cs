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
        newLevel.selectedStages = new List<Biome>();
        for (int i = 0; i < allStages.Count; i++)
        {
            int randomSelect = UnityEngine.Random.Range(0, allStages[i].stages.Count);
            newLevel.selectedStages.Add(allStages[i].stages[randomSelect]);
        }

        newLevel.currentGeneratedOpponent = CreateOpponent(newLevel.selectedStages[0]);
        newLevel.currentStage = 0;
        newLevel.currentMatch = 0;
        newLevel.activeRun = true;

        return newLevel;
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
            newAnimal.legsID = biome.animalCodes[randomChosen] + "_tail";
        }

        Debug.Log("Created new level");
        return newAnimal;
    }
}

[System.Serializable]
public struct LevelData
{
    public bool activeRun;
    public List<Biome> selectedStages;
    public AnimalPartsObject currentGeneratedOpponent;
    public int currentStage;
    public int currentMatch;    // if round == 2 then BOSS
}
