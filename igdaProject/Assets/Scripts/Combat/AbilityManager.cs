using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;

    public Entity attacker; // who's stats should I use?
    public Entity target; //the current entity that abilities will target. 
    Ability currentAbility = null;  // ability to pull data from
    private void Awake() {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    public void UseAbility(Ability ability, Entity target, Entity attacker = null) {
        this.target = target;
        this.attacker = attacker;
        currentAbility = ability;
        string abil = currentAbility.abilityData.name.Replace(" ", "").ToLower();
        switch (abil) {
            case "rest": Rest(); break;
            case "basicbash": BasicBash(); break;

            case "clawswipe": ClawSwipe(); break;
            case "chomp": Chomp(); break;
            case "peck": Peck(); break;
            case "bite": Bite(); break;
            case "firebreath": FireBreath(); break;
            case "stomp": Stomp(); break;
            case "kick": Kick(); break;
            case "slap": Slap(); break;
            case "wingslash": WingSlash(); break;
            case "sting": Sting(); break;
            case "rollypolly": RollyPolly(); break;
            case "rainbowbeam": RainbowBeam(); break;
            case "charge": Charge();  break;
            case "smash": SMASH(); break;
            case "bigbite": BigBite(); break;

            case "soothingsong": SoothingSong(); break;
            case "roar": Roar(); break;
            case "sharkbait": SharkBait(); break;
            case "turtleshield": TurtleShield(); break;
            case "tonguelash": TongueLash(); break;
            case "magichorn": MagicHorn(); break;
            case "jump": Jump(); break;
            case "goodjoke": GoodJoke(); break;
            case "flaunt": Flaunt(); break;
            case "birdofprey": BirdOfPrey(); break;
            case "fromtheashes": FromTheAshes(); break;
            case "bearhug": BearHug(); break;
            case "bark": Bark(); break;
            case "cackle": Cackle(); break;
            case "tailwhip": TailWhip(); break;
            case "tickle": Tickle(); break;
            case "gobble": break;
            default: Debug.LogError("Ability not found! " + ability); break;
        }

    }
    
    public void Rest() {
        CombatManager.instance.ShowSupportText(target.animalName + " has increased mana!");
        CombatManager.instance.GainMana(target, currentAbility.abilityData.mana);
    }
    public void BasicBash() {
       // target.AffectHealth(-5);
        CombatManager.instance.DealtDamage(target, currentAbility.abilityData.attack, false, target.DodgeCheck());
    }

    public void SMASH() {
       
        attacker.holdingAbility = null;
        CombatManager.instance.DealtDamage(target, (currentAbility.abilityData.attack + attacker.attack), attacker.CritCheck(), target.DodgeCheck());
    }

    #region ATTACK ABILITIES

    public void BigBite() {

        CombatManager.instance.DealtDamage(target, (currentAbility.abilityData.attack + attacker.attack), true, target.DodgeCheck());


    }

    public void Charge() {
        attacker.holdingAbility = DataManager.instance.GetAbility("SMASH");
        CombatManager.instance.DealtDamage(target, 0, false, false);
    }
    public void ClawSwipe() {
        CombatManager.instance.DealtDamage(target, (currentAbility.abilityData.attack + attacker.attack), attacker.CritCheck(), target.DodgeCheck());
    }

    public void Chomp() {
        CombatManager.instance.DealtDamage(target, (currentAbility.abilityData.attack + attacker.attack), attacker.CritCheck(), target.DodgeCheck());
    }

    public void Peck() {
        CombatManager.instance.DealtDamage(target, (currentAbility.abilityData.attack + attacker.attack), attacker.CritCheck(), target.DodgeCheck());
    }

    public void Bite() {
        CombatManager.instance.DealtDamage(target, (currentAbility.abilityData.attack + attacker.attack), attacker.CritCheck(), target.DodgeCheck());
    }

    public void FireBreath() {
        CombatManager.instance.DealtDamage(target, (currentAbility.abilityData.attack + attacker.attack), attacker.CritCheck(), target.DodgeCheck());
    }

    public void Stomp() {
        CombatManager.instance.DealtDamage(target, (currentAbility.abilityData.attack + attacker.attack), attacker.CritCheck(), false);
    }

    public void Kick() {
        CombatManager.instance.DealtDamage(target, (currentAbility.abilityData.attack + attacker.attack), attacker.CritCheck(), target.DodgeCheck());
    }

    public void Slap() {
        CombatManager.instance.DealtDamage(target, (currentAbility.abilityData.attack + attacker.attack), attacker.CritCheck(), target.DodgeCheck());
    }

    public void WingSlash() {
        CombatManager.instance.DealtDamage(target, (currentAbility.abilityData.attack + attacker.attack), attacker.CritCheck(), target.DodgeCheck());
    }

    public void Sting() {
        CombatManager.instance.DealtDamage(target, (currentAbility.abilityData.attack + attacker.attack), attacker.CritCheck(), false);
    }

    public void RollyPolly() {
        CombatManager.instance.DealtDamage(target, (currentAbility.abilityData.attack + attacker.attack), attacker.CritCheck(), target.DodgeCheck());
    }

    public void RainbowBeam() {
        CombatManager.instance.DealtDamage(target, (currentAbility.abilityData.attack + attacker.attack), attacker.CritCheck(), target.DodgeCheck());
    }

    

    #endregion

    #region SUPPORT ABILITIES
    public void Bark() {

        target.AffectAttack(currentAbility.abilityData.attack);
        CombatManager.instance.ShowSupportText(target.animalName + " has increased attack!");
    }

    public void SoothingSong() {

        target.AffectAttack(currentAbility.abilityData.attack);
        CombatManager.instance.ShowSupportText(target.animalName + " has decreased attack!");
    }

    public void Roar() {
        target.AffectDodge(currentAbility.abilityData.dodge);
        CombatManager.instance.ShowSupportText(target.animalName + " has decreased dodge!");
    }

    public void SharkBait() {
        target.AffectSpeed(currentAbility.abilityData.speed);
        CombatManager.instance.ShowSupportText(target.animalName + " has decreased speed!");
    }

    public void TurtleShield() {
        target.AffectDodge(currentAbility.abilityData.dodge);
        target.AffectSpeed(currentAbility.abilityData.speed);
        CombatManager.instance.ShowSupportText(target.animalName + " has increased dodge and lowered speed!");
    }

    public void TongueLash() {
        target.AffectDodge(currentAbility.abilityData.dodge);
        CombatManager.instance.ShowSupportText(target.animalName + " has increased dodge!");
    }

    public void MagicHorn() {
        target.AffectHealth(currentAbility.abilityData.health);
        CombatManager.instance.ShowSupportText(target.animalName + " has increased health!");
    }

    public void Jump() {
        CombatManager.instance.ShowSupportText(target.animalName + " does nothing!");
    }

    public void GoodJoke() {
        target.AffectDodge(currentAbility.abilityData.dodge);
        CombatManager.instance.ShowSupportText(target.animalName + " has decreased dodge!");
    }

    public void Flaunt() {
        target.AffectCrit(currentAbility.abilityData.crit);
        CombatManager.instance.ShowSupportText(target.animalName + " has increased crit!");
    }

    public void BirdOfPrey() {
        CombatManager.instance.ShowSupportText(target.animalName + " does nothing!");
    }

    public void FromTheAshes() {
        CombatManager.instance.ShowSupportText(target.animalName + " does nothing!");
    }

    public void BearHug() {
        target.AffectDodge(currentAbility.abilityData.dodge);
        CombatManager.instance.ShowSupportText(target.animalName + " has decreased dodge!");
    }

    public void Cackle() {
        CombatManager.instance.ShowSupportText(target.animalName + " has decreased mana!");
        CombatManager.instance.SpentMana(target, -currentAbility.abilityData.mana);
    }

    public void TailWhip() {
        target.AffectSpeed(currentAbility.abilityData.speed);
        CombatManager.instance.ShowSupportText(target.animalName + " has decreased speed!");
    }

    public void Tickle() {
        target.AffectDodge(currentAbility.abilityData.dodge);
        CombatManager.instance.ShowSupportText(target.animalName + " has decreased dodge!");
    }

    public void Gobble()
    {
        target.AffectMana(currentAbility.abilityData.mana);
        CombatManager.instance.ShowSupportText(target.animalName + " has increased mana!");
        CombatManager.instance.GainMana(target, currentAbility.abilityData.mana);
    }

    #endregion
}
