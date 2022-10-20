using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    
    public static void UseAbility(string abilityName) {
        switch (abilityName.ToLower()) {
            case "gobble": 
                Gobble();
                break;
            case "chomp":
                Chomp();
                break;
            case "fire breath":
                FireBreath();
                break;
            case "tough skin":
                ToughSkin();
                break;
        }
    }

    static void Gobble() {

    }

    static void Chomp() {

    }

    static void FireBreath() {

    }

    static void ToughSkin() {

    }
}
