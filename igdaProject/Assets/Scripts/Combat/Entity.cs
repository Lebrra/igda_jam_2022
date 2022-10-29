using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    public AnimalPartsObject animal;
    public string animalName;
    [Header("Stats")]
    public float healthMax, manaMax, dodgeMax, speedMax, critMax, attackMax;

    public float health, mana, dodge, speed, crit, attack; //current values

    [Header("Ability Names")]
    public List<Ability> abilityList = new List<Ability>();
    public List<Ability> useableAbilityList = new List<Ability>();

    public Ability holdingAbility;

    private void Start()
    {
        healthMax = (float)GameManager.BASE_HEALTH;
        manaMax = (float)GameManager.BASE_MANA;
        dodgeMax = (float)GameManager.BASE_DODGE;
        speedMax = (float)GameManager.BASE_SPEED;
        critMax = (float)GameManager.BASE_CRIT;
        attackMax = (float)GameManager.BASE_DMG;
    }

    /// <summary>
    /// Affects this entity's health. For reducing the value, use a negative int.
    /// </summary>
    /// <param name="num"></param>
    public void AffectHealth(float num) {
        health += num;
        health = Mathf.Clamp(health, 0, healthMax);
    }

    /// <summary>
    /// Affects this entity's mana. For reducing the value, use a negative int.
    /// </summary>
    /// <param name="num"></param>
    public void AffectMana(float num) {
        mana += num;
        mana = Mathf.Clamp(mana, 0, manaMax);
    }

    /// <summary>
    /// Affects this entity's dodge value. For reducing the value, use a negative int.
    /// </summary>
    /// <param name="num"></param>
    public void AffectDodge(float num) {
        dodge += num;
        dodge = Mathf.Clamp(dodge, 0, 50);  // 50% is the max
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
        crit = Mathf.Clamp(crit, 0, 50); // 50% is the max
    }

    /// <summary>
    /// Affects this entity's Attack value. For reducing the value, use a negative int.
    /// </summary>
    /// <param name="num"></param>
    public void AffectAttack(float num) {
        attack += num;
        attack = Mathf.Clamp(attack, 0, 1000);
    }

    public void ResetStatsToMax() {
        health = healthMax;
        mana = manaMax;
        dodge = dodgeMax;
        speed = speedMax;
        crit = critMax;
        attack = attackMax;
    }

    public Ability GetRandomAbility(Ability bash, Ability rest) {
        List<Ability> allPossibilities = new List<Ability>();
        foreach (var ability in useableAbilityList) allPossibilities.Add(ability);
        allPossibilities.Add(bash);
        allPossibilities.Add(rest);

        while (allPossibilities.Count > 0)
        {
            int rand = Random.Range(0, allPossibilities.Count);
            if (allPossibilities[rand].abilityData.abilityCost <= mana) return allPossibilities[rand];
            else allPossibilities.RemoveAt(rand);
        }

        Debug.LogError("Something broke while randomly selecting an ability...");
        return null;
    }

    public bool CritCheck()
    {
        bool doesCrit = Random.Range(0, 100) < crit;
        if (doesCrit) Debug.Log("CRIT");
        return doesCrit;
    }

    public bool DodgeCheck()
    {
        bool doesDodge = Random.Range(0, 100) < dodge;
        if (doesDodge) Debug.Log("DODGE");
        return doesDodge;
    }
}
