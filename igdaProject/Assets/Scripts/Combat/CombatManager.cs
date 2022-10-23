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
    private void NewRound() {
        /*
         * DEFINITION OF ROUND
         * - A round consists of both players determining their selected move to use.
         * - These moves will occur during this time period known as a round. 
         * 
         * I need to first have both players select their move inside of a turn.
         * After both moves are selected, the moves will occur in order of highest to lowest speed of each monster
         * 
         * IE: Pet A has 10 speed, Pet B has 3 speed. When the round begins, Pet A will go first. 
         * - This means that if Pet A defeats Pet B during its move, Pet B's move will nullify. 
         * - The importance of speed is to determine who gets to hit first, which can mean the difference in a match. 
         * 
         */

    }

}
