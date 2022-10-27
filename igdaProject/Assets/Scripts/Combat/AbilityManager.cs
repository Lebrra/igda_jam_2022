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

            case "Chomp": Chomp(); break;
            case "Peck": Peck(); break;
            case "Bite": Bite(); break;
            case "FireBreath": FireBreath(); break;
            case "Stomp": Stomp(); break;
            case "Kick": Kick(); break;
            case "Slap": Slap(); break;
            case "WingSlash": WingSlash(); break;
            case "Sting": Sting(); break;
            case "RollyPolly": RollyPolly(); break;
            case "RainbowBeam": RainbowBeam(); break;

            case "SoothingSong": SoothingSong(); break;
            case "Roar": Roar(); break;
            case "SharkBait": SharkBait(); break;
            case "TurtleShield": TurtleShield(); break;
            case "TongueLash": TongueLash(); break;
            case "MagicHorn": MagicHorn(); break;
            case "Jump": Jump(); break;
            case "GoodJoke": GoodJoke(); break;
            case "Flaunt": Flaunt(); break;
            case "BirdOfPrey": BirdOfPrey(); break;
            case "FromTheAshes": FromTheAshes(); break;
            case "BearHug": BearHug(); break;
            case "Bark": Bark(); break;
            case "Cackle": Cackle(); break;
            case "TailWhip": TailWhip(); break;
            case "Tickle": Tickle(); break;
        }

    }
    #region ATTACK ABILITIES
    public void Rest() {
        target.AffectMana(10);
    }
    public void BasicBash() {
        target.AffectHealth(-5);
        CombatManager.instance.DealtDamage(target, 5);
    }
    
    public void ClawSwipe() {
        target.AffectHealth((5 + target.attack) * -1f);
    }

    public void Chomp() {
        target.AffectHealth((4 + target.attack) * -1f);
    }

    public void Peck() {
        target.AffectHealth((2 + target.attack) * -1f);
    }

    public void Bite() {
        target.AffectHealth((3 + target.attack) * -1f);
    }

    public void FireBreath() {
        target.AffectHealth((7 + target.attack) * -1f);
    }

    public void Stomp() {
        target.AffectHealth((4 + target.attack) * -1f);
    }

    public void Kick() {
        target.AffectHealth((3 + target.attack) * -1f);
    }

    public void Slap() {
        target.AffectHealth((2 + target.attack) * -1f);
    }

    public void WingSlash() {
        target.AffectHealth((4 + target.attack) * -1f);
    }

    public void Sting() {
        target.AffectHealth((2 + target.attack) * -1f);
    }

    public void RollyPolly() {
        target.AffectHealth((5 + target.attack) * -1f);
    }

    public void RainbowBeam() {
        target.AffectHealth((30 + target.attack) * -1f);
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

    #endregion
}
