using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;

    public Entity target; //the current entity that abilities will target. 
    private void Awake() {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    public void UseAbility(string abilityName) {
      
        Invoke(abilityName, 1);

    }

    private void Gobble() {

        

    }

    private void Chomp() {

    }

    private void FireBreath() {

    }

    private void ToughSkin() {

    }
}
