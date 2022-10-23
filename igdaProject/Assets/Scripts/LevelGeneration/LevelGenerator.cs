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

    [SerializeField]
    LevelData testLevel;

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
        newLevel.currentRound = 0;

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

        randomChosen = UnityEngine.Random.Range(0, biome.animalCodes.Count);
        newAnimal.tailID = biome.animalCodes[randomChosen] + "_tail";

        Debug.Log("Created new level");
        return newAnimal;
    }

    private void Start()
    {
        testLevel = GenerateNewLevel();
    }
}

[System.Serializable]
public struct LevelData
{
    public List<Biome> selectedStages;
    public AnimalPartsObject currentGeneratedOpponent;
    public int currentStage;
    public int currentRound;    // if round == 2 then BOSS
}
