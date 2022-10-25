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

    public void UseAbility(string abilityName, Entity target) {
        this.target = target;
        switch (abilityName) {
            case "Rest": Rest(); break;
            case "BasicBash": BasicBash(); break;
            case "ClawSwipe": ClawSwipe(); break;
        }

    }

    public void Rest() {
        target.AffectMana(10);
    }
    public void BasicBash() {
        target.AffectHealth(-5);
    }
    private void Bark() {

        target.AffectAttack(1);
    }
    private void ClawSwipe() {
        target.AffectHealth((5 + target.attack) * -1f);
    }

}
