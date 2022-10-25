using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    public AnimalPartsObject animal;
    public string animalName;
    [Header("Stats")]
    [SerializeField] float healthDefault, manaDefault, dodgeDefault, speedDefault, critDefault, attackDefault;

    public float health, mana, dodge, speed, crit, attack; //current values

    [Header("Ability Names")]
    public List<Ability> abilityList = new List<Ability>();
    


    /// <summary>
    /// Affects this entity's health. For reducing the value, use a negative int.
    /// </summary>
    /// <param name="num"></param>
    public void AffectHealth(float num) {
        health += num;
    }

    /// <summary>
    /// Affects this entity's mana. For reducing the value, use a negative int.
    /// </summary>
    /// <param name="num"></param>
    public void AffectMana(float num) {
        mana += num;
    }

    /// <summary>
    /// Affects this entity's dodge value. For reducing the value, use a negative int.
    /// </summary>
    /// <param name="num"></param>
    public void AffectDodge(float num) {
        dodge += num;
    }

    /// <summary>
    /// Affects this entity's speed value. For reducing the value, use a negative int.
    /// </summary>
    /// <param name="num"></param>
    public void AffectSpeed(float num) {
        speed += num;
    }

    /// <summary>
    /// Affects this entity's Crit value. For reducing the value, use a negative int.
    /// </summary>
    /// <param name="num"></param>
    public void AffectCrit(float num) {
        crit += num;
    }

    /// <summary>
    /// Affects this entity's Attack value. For reducing the value, use a negative int.
    /// </summary>
    /// <param name="num"></param>
    public void AffectAttack(float num) {
        attack += num;
    }

    public void ResetStats() {
        health = healthDefault;
        mana = manaDefault;
        dodge = dodgeDefault;
        speed = speedDefault;
        crit = critDefault;
        attack = attackDefault;
    }

}
