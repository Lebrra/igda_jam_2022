using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float healthMax, manaMax, dodgeMax, speedMax, accuracyMax; //max values

    float health, mana, dodge, speed, crit, attack; //current values

    [Header("Ability Names")]
    [SerializeField] private string HeadAbility;
    [SerializeField] private string BodyAbility;
    [SerializeField] private string LegsAbility;
    [SerializeField] private string TailAbility;


    /// <summary>
    /// Affects this entity's health. For reducing the value, use a negative int.
    /// </summary>
    /// <param name="num"></param>
    public void AffectHealth(int num) {
        health += num;
    }

    /// <summary>
    /// Affects this entity's mana. For reducing the value, use a negative int.
    /// </summary>
    /// <param name="num"></param>
    public void AffectMana(int num) {
        mana += num;
    }

    /// <summary>
    /// Affects this entity's dodge value. For reducing the value, use a negative int.
    /// </summary>
    /// <param name="num"></param>
    public void AffectDodge(int num) {
        dodge += num;
    }

    /// <summary>
    /// Affects this entity's speed value. For reducing the value, use a negative int.
    /// </summary>
    /// <param name="num"></param>
    public void AffectSpeed(int num) {
        speed += num;
    }

    public void ResetStats() {
        health = healthMax;
        mana = manaMax;
        dodge = dodgeMax;
        speed = speedMax;
    }

    public void UpdateAbility(AnimalPart part) {
        switch (part.partData.bodyPart) {
            case BodyPart.Head:
                HeadAbility = part.partData.abilityName;
                break;
            case BodyPart.Body:
                BodyAbility = part.partData.abilityName;
                break;
            case BodyPart.Legs:
                LegsAbility = part.partData.abilityName;
                break;
            case BodyPart.Tail:
                TailAbility = part.partData.abilityName;
                break;
            default:
                Debug.Log("Error, no body part");
                break;
        }
    }

    public void Use_HeadAbility() {
        Debug.Log("I used " + HeadAbility);
    }
    public void Use_BodyAbility() {
        Debug.Log("I used " + BodyAbility);
    }
    public void Use_LegsAbility() {
        Debug.Log("I used " + LegsAbility);
    }
    public void Use_TailAbility() {
        Debug.Log("I used " + TailAbility);
    }



}
