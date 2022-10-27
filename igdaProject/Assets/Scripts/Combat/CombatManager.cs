using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
    public Player player;
    public Enemy enemy;

    [Header("Combat Preview")]
    [SerializeField] Animator previewAnim;
    [SerializeField] Button battleButton;
    [SerializeField] Button backButton;
    [SerializeField] AnimalPrefabBuilder enemyObjPreview;
    [SerializeField] AnimalPrefabBuilder playerObjPreview;


    [Header("Combat Scene")]
    [SerializeField] Animator combatAnim;
    [SerializeField] AnimalPrefabBuilder enemyObj;
    [SerializeField] AnimalPrefabBuilder playerObj;
    [SerializeField] Button ability_one;
    [SerializeField] Button ability_two;
    [SerializeField] Button ability_three;
    [SerializeField] Button ability_four;
    [SerializeField] Button ability_rest;
    [SerializeField] Button ability_basicBash;
    [SerializeField] Image playerHealthBar;
    [SerializeField] Image enemyHealthBar;
    [SerializeField] Image playerManaBar;
    [SerializeField] Image enemyManaBar;

    //in combat variables
    List<Ability> playerAbilities = new List<Ability>();
    List<Ability> enemyAbilities = new List<Ability>();

    private void Awake() {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    private void Start() {

        battleButton.onClick.AddListener(() => GameDirector.instance.ClosePreviewToCombat(true));
        backButton.onClick.AddListener(() => GameDirector.instance.ClosePreviewToCombat(false));
    }

    public void OpenPreview() {
        enemy.RandomizeBuild();
        previewAnim.SetBool("Status", true);
        playerObjPreview.CreateAnimal(GameManager.instance.playerdata.GetActiveAnimal(), true, true);
        enemyObjPreview.CreateAnimal(enemy.animal, true, true);
    }

    public void ClosePreview() {
        previewAnim.SetBool("Status", false);
        playerObjPreview.DestroyAnimal();
        enemyObjPreview.DestroyAnimal();
    }

    /// <summary>
    /// Starts Combat. Beginning of match. 
    /// </summary>
    public void OpenCombat() {

        player.animal = GameManager.instance.playerdata.GetActiveAnimal();
        player.name = playerObj.NameGenerator(player.animal);
        enemy.name = enemyObj.NameGenerator(enemy.animal);

        InitializeCombat();

        combatAnim.SetBool("Status", true);
        playerObj.CreateAnimal(player.animal, true, true);
        enemyObj.CreateAnimal(enemy.animal, true, true);

    }

    public void CloseCombat() {
        combatAnim.SetBool("Status", false);
        playerObj.DestroyAnimal();
        enemyObj.DestroyAnimal();
    }

    public void InitializeCombat() {
        GainPassiveStats(player);
        //GainPassiveStats(enemy);

        ability_rest.onClick.AddListener(() => AbilityManager.instance.UseAbility("Rest", player));
        ability_basicBash.onClick.AddListener(() => AbilityManager.instance.UseAbility("BasicBash", enemy));
    }

    /// <summary>
    /// When a pet is dealt damage, this will change the UI of their healthbar.
    /// </summary>
    public void DealtDamage(Entity entity, float amount) {
        if(entity == player) {

        
            StartCoroutine(UIHealthbarScaler(entity, playerHealthBar, player.health, amount));
        } else {

         
            StartCoroutine(UIHealthbarScaler(entity, enemyHealthBar, enemy.health, amount));
        }
    }

    public void SpentMana(Entity entity, float amount) {
        if (entity == player) {

        
            StartCoroutine(UIManabarScaler(playerManaBar, player.mana, amount));
        }
        else {

           
            StartCoroutine(UIManabarScaler(enemyManaBar, enemy.mana, amount));
        }
    }

    private IEnumerator UIHealthbarScaler(Entity e, Image healthBar, float currentHealth, float amount) {
        var i = 0f;
        float speed = 4f;
        float targetHealth = currentHealth - amount;
        while(i < 1) {
            i += Time.deltaTime * speed;
            healthBar.fillAmount = Mathf.Lerp((currentHealth / 100), (targetHealth / 100), i);
            yield return null;
        }

        if(currentHealth <= GainPercentage(e, 60) && currentHealth >= GainPercentage(e, 20)) {
            // #f5cd79
            healthBar.color = new Color32(245, 205, 121, 255);
        } else if (currentHealth < GainPercentage(e, 20)) {
            //#e66767
            healthBar.color = new Color32(230, 103, 103, 255);
        }

    }

    private IEnumerator UIManabarScaler(Image manaBar, float currentMana, float amount) {
        var i = 0f;
        float speed = 4f;
        float targetMana = currentMana - amount;
        while (i < 1) {
            i += Time.deltaTime * speed;
            manaBar.fillAmount = Mathf.Lerp((currentMana / 100), (targetMana / 100), i);
            yield return null;
        }
    }

    private float GainPercentage(Entity entity, float num) {
        var h = entity.healthMax;

        return h * ( num / 100);
    }
    private void GainPassiveStats(Entity entity) {
        Debug.Log("Calling passive function");
        entity.ResetStats();
        entity.abilityList.Clear();

        Ability ability1 = DataManager.instance.GetAnimalPart(entity.animal.headID).GetAbility();
        Ability ability2 = DataManager.instance.GetAnimalPart(entity.animal.bodyID).GetAbility();
        Ability ability3 = DataManager.instance.GetAnimalPart(entity.animal.legsID).GetAbility();
        Ability ability4 = DataManager.instance.GetAnimalPart(entity.animal.tailID).GetAbility();

        entity.abilityList.Add(ability1);
        entity.abilityList.Add(ability2);
        entity.abilityList.Add(ability3);
        entity.abilityList.Add(ability4);

        foreach(Ability a in entity.abilityList) {
            if(a.abilityData.type == AbilityType.passive) {
                entity.AffectHealth(a.abilityData.health);
                entity.AffectMana(a.abilityData.mana);
                entity.AffectSpeed(a.abilityData.speed);
                entity.AffectDodge(a.abilityData.dodge);
                entity.AffectCrit(a.abilityData.crit);
                entity.AffectAttack(a.abilityData.attack);
            }
        }

    }
    public void UseAbility(Ability a) {
        //when we use an ability, check speed differences
        if(player.speed >= enemy.speed) {
            //player will go first in turn order
            AbilityManager.instance.UseAbility(a.abilityData.name, a.abilityData.type == AbilityType.attack ? enemy : player);
            enemy.UseRandomAbility();
        } else {
            //enemy will go first in turn order
            enemy.UseRandomAbility();
            AbilityManager.instance.UseAbility(a.abilityData.name, a.abilityData.type == AbilityType.attack ? enemy : player);
        }
    }

}
