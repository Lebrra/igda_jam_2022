using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    public void BuildAnimal(AnimalPartsObject a) {
        Debug.Log(a.headID + ", " + a.bodyID + ", " + a.legsID + ", " + a.tailID);
        animal = a;
    }

    public void RandomizeBuild() {

        int headIndex = Random.Range(0, DataManager.instance.headList.Count);
        int bodyIndex = Random.Range(0, DataManager.instance.bodyList.Count);
        int legsIndex = Random.Range(0, DataManager.instance.legsList.Count);
        int tailIndex = Random.Range(0, DataManager.instance.tailList.Count);

        //UpdateAbility(CombatManager.instance.headList[headIndex]);
        //UpdateAbility(CombatManager.instance.bodyList[bodyIndex]);
        //UpdateAbility(CombatManager.instance.legsList[legsIndex]);
        //UpdateAbility(CombatManager.instance.tailList[tailIndex]);

        AnimalPartsObject a = new AnimalPartsObject();
        a.headID = DataManager.instance.headList[headIndex].partData.id;
        a.bodyID = DataManager.instance.bodyList[bodyIndex].partData.id;
        a.legsID = DataManager.instance.legsList[legsIndex].partData.id;
        a.tailID = DataManager.instance.tailList[tailIndex].partData.id;

        Debug.Log(a.headID + ", " + a.bodyID + ", " + a.legsID + ", " + a.tailID);

        animal = a; //found in entity script;
    }

    public void UseRandomAbility() {

    }


}
