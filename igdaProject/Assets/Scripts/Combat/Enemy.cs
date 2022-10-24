using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    
    public void RandomizeBuild() {

        int headIndex = Random.Range(0, CombatManager.instance.headList.Count);
        int bodyIndex = Random.Range(0, CombatManager.instance.bodyList.Count);
        int legsIndex = Random.Range(0, CombatManager.instance.legsList.Count);
        int tailIndex = Random.Range(0, CombatManager.instance.tailList.Count);

        UpdateAbility(CombatManager.instance.headList[headIndex]);
        UpdateAbility(CombatManager.instance.bodyList[bodyIndex]);
        UpdateAbility(CombatManager.instance.legsList[legsIndex]);
        UpdateAbility(CombatManager.instance.tailList[tailIndex]);

        Debug.Log("Enemy has been randomized!");
    }

}
