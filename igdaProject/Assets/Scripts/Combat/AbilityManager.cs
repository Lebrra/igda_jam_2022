using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;

    public Entity attacker; // who's stats should I use?
    public Entity target; //the current entity that abilities will target. 
    private void Awake() {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    public void UseAbility(string abilityName, Entity target, Entity attacker = null) {
        this.target = target;
        this.attacker = attacker;
        abilityName = abilityName.Replace(" ", "").ToLower();
        switch (abilityName) {
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
            case "charge": break;

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
            default: Debug.LogError("Ability not found! " + abilityName); break;
        }

    }
    
    public void Rest() {
        target.AffectMana(10);
        CombatManager.instance.ShowSupportText(target.animalName + " has increased mana!");
    }
    public void BasicBash() {
       // target.AffectHealth(-5);
        CombatManager.instance.DealtDamage(target, 5, false, target.DodgeCheck());
    }

    #region ATTACK ABILITIES
    public void ClawSwipe() {
        CombatManager.instance.DealtDamage(target, (5 + attacker.attack), attacker.CritCheck(), target.DodgeCheck());
    }

    public void Chomp() {
        CombatManager.instance.DealtDamage(target, (4 + attacker.attack), attacker.CritCheck(), target.DodgeCheck());
    }

    public void Peck() {
        CombatManager.instance.DealtDamage(target, (2 + attacker.attack), attacker.CritCheck(), target.DodgeCheck());
    }

    public void Bite() {
        CombatManager.instance.DealtDamage(target, (1 + attacker.attack), attacker.CritCheck(), target.DodgeCheck());
    }

    public void FireBreath() {
        CombatManager.instance.DealtDamage(target, (7 + attacker.attack), attacker.CritCheck(), target.DodgeCheck());
    }

    public void Stomp() {
        CombatManager.instance.DealtDamage(target, (4 + attacker.attack), attacker.CritCheck(), false);
    }

    public void Kick() {
        CombatManager.instance.DealtDamage(target, (3 + attacker.attack), attacker.CritCheck(), target.DodgeCheck());
    }

    public void Slap() {
        CombatManager.instance.DealtDamage(target, (2 + attacker.attack), attacker.CritCheck(), target.DodgeCheck());
    }

    public void WingSlash() {
        CombatManager.instance.DealtDamage(target, (4 + attacker.attack), attacker.CritCheck(), target.DodgeCheck());
    }

    public void Sting() {
        CombatManager.instance.DealtDamage(target, (2 + attacker.attack), attacker.CritCheck(), false);
    }

    public void RollyPolly() {
        CombatManager.instance.DealtDamage(target, (5 + attacker.attack), attacker.CritCheck(), target.DodgeCheck());
    }

    public void RainbowBeam() {
        CombatManager.instance.DealtDamage(target, (30 + attacker.attack), attacker.CritCheck(), target.DodgeCheck());
    }

    public void Charge()
    {

    }

    #endregion

    #region SUPPORT ABILITIES
    public void Bark() {

        target.AffectAttack(1);
        CombatManager.instance.ShowSupportText(target.animalName + " has increased attack!");
    }

    public void SoothingSong() {

        target.AffectAttack(-5);
        CombatManager.instance.ShowSupportText(target.animalName + " has decreased attack!");
    }

    public void Roar() {
        target.AffectDodge(-5);
        CombatManager.instance.ShowSupportText(target.animalName + " has decreased dodge!");
    }

    public void SharkBait() {
        target.AffectSpeed(-6);
        CombatManager.instance.ShowSupportText(target.animalName + " has decreased speed!");
    }

    public void TurtleShield() {
        CombatManager.instance.ShowSupportText(target.animalName + " does nothing!");
    }

    public void TongueLash() {
        target.AffectDodge(8);
        CombatManager.instance.ShowSupportText(target.animalName + " has increased dodge!");
    }

    public void MagicHorn() {
        target.AffectHealth(5);
        CombatManager.instance.ShowSupportText(target.animalName + " has increased health!");
    }

    public void Jump() {
        CombatManager.instance.ShowSupportText(target.animalName + " does nothing!");
    }

    public void GoodJoke() {
        target.AffectDodge(-6);
        CombatManager.instance.ShowSupportText(target.animalName + " has decreased dodge!");
    }

    public void Flaunt() {
        target.AffectCrit(5);
        CombatManager.instance.ShowSupportText(target.animalName + " has decreased crit!");
    }

    public void BirdOfPrey() {
        CombatManager.instance.ShowSupportText(target.animalName + " does nothing!");
    }

    public void FromTheAshes() {
        CombatManager.instance.ShowSupportText(target.animalName + " does nothing!");
    }

    public void BearHug() {
        target.AffectDodge(-5);
        CombatManager.instance.ShowSupportText(target.animalName + " has decreased dodge!");
    }

    public void Cackle() {
        target.AffectMana(-10);
        CombatManager.instance.ShowSupportText(target.animalName + " has decreased mana!");
    }

    public void TailWhip() {
        target.AffectSpeed(-5);
        CombatManager.instance.ShowSupportText(target.animalName + " has decreased speed!");
    }

    public void Tickle() {
        target.AffectDodge(5);
        CombatManager.instance.ShowSupportText(target.animalName + " has decreased dodge!");
    }

    public void Gobble()
    {
        target.AffectMana(10);
        CombatManager.instance.ShowSupportText(target.animalName + " has increased mana!");
    }

    #endregion
}
