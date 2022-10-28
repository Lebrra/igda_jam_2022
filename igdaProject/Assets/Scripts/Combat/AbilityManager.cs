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
    }
    public void BasicBash() {
       // target.AffectHealth(-5);
        CombatManager.instance.DealtDamage(target, 5);
    }

    #region ATTACK ABILITIES
    public void ClawSwipe() {
       // target.AffectHealth((5 + target.attack) * -1f);
       if (attacker.CritCheck()) CombatManager.instance.DealtDamage(target, (5 + attacker.attack) * 1.5F);
       else CombatManager.instance.DealtDamage(target, (5 + attacker.attack));
    }

    public void Chomp() {
        // target.AffectHealth((4 + target.attack) * -1f);
        if (attacker.CritCheck()) CombatManager.instance.DealtDamage(target, (4 + attacker.attack) * 1.5F);
        else CombatManager.instance.DealtDamage(target, (4 + attacker.attack));
    }

    public void Peck() {
        //   target.AffectHealth((2 + target.attack) * -1f);
        if (attacker.CritCheck()) CombatManager.instance.DealtDamage(target, (2 + attacker.attack) * 1.5F);
        else CombatManager.instance.DealtDamage(target, (2 + attacker.attack));
    }

    public void Bite() {
        // target.AffectHealth((3 + target.attack) * -1f);
        if (attacker.CritCheck()) CombatManager.instance.DealtDamage(target, (1 + attacker.attack) * 1.5F);
        else CombatManager.instance.DealtDamage(target, (1 + attacker.attack));
    }

    public void FireBreath() {
        //  target.AffectHealth((7 + target.attack) * -1f);
        if (attacker.CritCheck()) CombatManager.instance.DealtDamage(target, (7 + attacker.attack) * 1.5F);
        else CombatManager.instance.DealtDamage(target, (7 + attacker.attack));
    }

    public void Stomp() {
        //  target.AffectHealth((4 + target.attack) * -1f);
        if (attacker.CritCheck()) CombatManager.instance.DealtDamage(target, (4 + attacker.attack) * 1.5F);
        else CombatManager.instance.DealtDamage(target, (4 + attacker.attack));
    }

    public void Kick() {
      //  target.AffectHealth((3 + target.attack) * -1f);
        if (attacker.CritCheck()) CombatManager.instance.DealtDamage(target, (3 + attacker.attack) * 1.5F);
        else CombatManager.instance.DealtDamage(target, (3 + attacker.attack));
    }

    public void Slap() {
        // target.AffectHealth((2 + target.attack) * -1f);
        if (attacker.CritCheck()) CombatManager.instance.DealtDamage(target, (2 + attacker.attack) * 1.5F);
        else CombatManager.instance.DealtDamage(target, (2 + attacker.attack));
    }

    public void WingSlash() {
        // target.AffectHealth((4 + target.attack) * -1f);
        if (attacker.CritCheck()) CombatManager.instance.DealtDamage(target, (4 + attacker.attack) * 1.5F);
        else CombatManager.instance.DealtDamage(target, (4 + attacker.attack));
    }

    public void Sting() {
        // target.AffectHealth((2 + target.attack) * -1f);
        if (attacker.CritCheck()) CombatManager.instance.DealtDamage(target, (2 + attacker.attack) * 1.5F);
        else CombatManager.instance.DealtDamage(target, (2 + attacker.attack));
    }

    public void RollyPolly() {
        //target.AffectHealth((5 + target.attack) * -1f);
        if (attacker.CritCheck()) CombatManager.instance.DealtDamage(target, (5 + attacker.attack) * 1.5F);
        else CombatManager.instance.DealtDamage(target, (5 + attacker.attack));
    }

    public void RainbowBeam() {
        //target.AffectHealth((30 + target.attack) * -1f);
        if (attacker.CritCheck()) CombatManager.instance.DealtDamage(target, (30 + attacker.attack) * 1.5F);
        else CombatManager.instance.DealtDamage(target, (30 + attacker.attack));
    }

    public void Charge()
    {

    }

    #endregion

    #region SUPPORT ABILITIES
    public void Bark() {

        target.AffectAttack(1);
    }

    public void SoothingSong() {

        target.AffectAttack(-5);
    }

    public void Roar() {
        target.AffectDodge(-5);
    }

    public void SharkBait() {
        target.AffectSpeed(-6);
    }

    public void TurtleShield() {

    }

    public void TongueLash() {
        target.AffectDodge(8);
    }

    public void MagicHorn() {
        target.AffectHealth(5);
    }

    public void Jump() {

    }

    public void GoodJoke() {
        target.AffectDodge(-6);
    }

    public void Flaunt() {
        target.AffectCrit(5);
    }

    public void BirdOfPrey() {

    }

    public void FromTheAshes() {

    }

    public void BearHug() {
        target.AffectDodge(-5);
    }

    public void Cackle() {
        target.AffectMana(-10);
    }

    public void TailWhip() {
        target.AffectSpeed(-5);
    }

    public void Tickle() {
        target.AffectDodge(5);
    }

    public void Gobble()
    {
        target.AffectMana(10);
    }

    #endregion
}
