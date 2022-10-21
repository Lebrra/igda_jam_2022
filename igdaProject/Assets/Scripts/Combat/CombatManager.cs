using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{

    public Player player;
    public Enemy enemy;
    public void Start() {
        Invoke("InitializeCombat", 2);
    }

    private void InitializeCombat() {
        enemy.RandomizeBuild();
    }

}
