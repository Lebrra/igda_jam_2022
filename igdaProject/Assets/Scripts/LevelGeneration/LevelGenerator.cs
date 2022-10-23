using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    List<Biome> stage1;
    [SerializeField]
    List<Biome> stage2;
    [SerializeField]
    List<Biome> stage3;
    [SerializeField]
    List<Biome> stage4;
    [SerializeField]
    List<Biome> stage5;
    [SerializeField]
    Biome finalStage;

    public void GenerateNewLevel()
    {

    }
}

public struct LevelData
{
    string generatedLevel;
}
