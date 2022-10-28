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
    [SerializeField] Button button_ability_one;
    [SerializeField] Button button_ability_two;
    [SerializeField] Button button_ability_three;
    [SerializeField] Button button_ability_four;
    [SerializeField] Button ability_rest;
    [SerializeField] Button ability_basicBash;
    [SerializeField] Image playerHealthBar;
    [SerializeField] Image enemyHealthBar;
    [SerializeField] Image playerManaBar;
    [SerializeField] Image enemyManaBar;

    //in combat variables
    [SerializeField] TextMeshProUGUI combatText;
    private void Awake() {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    private void Start() {

        battleButton.onClick.AddListener(() => GameDirector.instance.ClosePreviewToCombat(true));
        backButton.onClick.AddListener(() => GameDirector.instance.ClosePreviewToCombat(false));
    }

    public void OpenPreview(bool randomize, AnimalPartsObject opponent) {
        if (randomize) enemy.RandomizeBuild();
        else enemy.BuildAnimal(opponent);
        previewAnim.SetBool("Status", true);
        playerObjPreview.CreateAnimal(GameManager.instance.playerdata.GetActiveAnimal(), true, true, AnimalPrefabBuilder.AnimationType.Bob);
        enemyObjPreview.CreateAnimal(enemy.animal, true, true, AnimalPrefabBuilder.AnimationType.Bob);
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
        player.animalName = playerObj.NameGenerator(player.animal);
        enemy.animalName = enemyObj.NameGenerator(enemy.animal);

        InitializeCombat();

        combatAnim.SetBool("Status", true);
        playerObj.CreateAnimal(player.animal, true, true, AnimalPrefabBuilder.AnimationType.Bob);
        enemyObj.CreateAnimal(enemy.animal, true, true, AnimalPrefabBuilder.AnimationType.Bob);

    }

    public void CloseCombat() {
        combatAnim.SetBool("Status", false);
        playerObj.DestroyAnimal();
        enemyObj.DestroyAnimal();

        ability_rest.onClick.RemoveAllListeners();
        ability_basicBash.onClick.RemoveAllListeners();
        button_ability_one.onClick.RemoveAllListeners();
        button_ability_two.onClick.RemoveAllListeners();
        button_ability_three.onClick.RemoveAllListeners();
        button_ability_four.onClick.RemoveAllListeners();
    }

    public void InitializeCombat() {
        GainPassiveStats(player);
        GainPassiveStats(enemy);

        ability_rest.onClick.AddListener(() => AbilityManager.instance.UseAbility("Rest", player));
        ability_basicBash.onClick.AddListener(() => AbilityManager.instance.UseAbility("BasicBash", enemy));
        button_ability_one.onClick.AddListener(() => UseAbility(player.abilityList[0]));
        button_ability_two.onClick.AddListener(() => UseAbility(player.abilityList[1]));
        button_ability_three.onClick.AddListener(() => UseAbility(player.abilityList[2]));
        button_ability_four.onClick.AddListener(() => UseAbility(player.abilityList[3]));

        

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
        Debug.Log(e.animalName + "took " + amount + " damage!");
        e.AffectHealth(-amount);

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
       // Debug.Log("Calling passive function");
        
        entity.abilityList.Clear();
        entity.useableAbilityList.Clear();

        Ability ability1 = DataManager.instance.GetAnimalPart(entity.animal.headID).GetAbility();
        Ability ability2 = DataManager.instance.GetAnimalPart(entity.animal.bodyID).GetAbility();
        Ability ability3 = DataManager.instance.GetAnimalPart(entity.animal.legsID).GetAbility();
        Ability ability4 = DataManager.instance.GetAnimalPart(entity.animal.tailID).GetAbility();

        entity.abilityList.Add(ability1); if (ability1.abilityData.type != AbilityType.passive) entity.useableAbilityList.Add(ability1);
        entity.abilityList.Add(ability2); if (ability2.abilityData.type != AbilityType.passive) entity.useableAbilityList.Add(ability2);
        entity.abilityList.Add(ability3); if (ability3.abilityData.type != AbilityType.passive) entity.useableAbilityList.Add(ability3);
        entity.abilityList.Add(ability4); if (ability4.abilityData.type != AbilityType.passive) entity.useableAbilityList.Add(ability4);

        if (entity == player) {
            button_ability_one.GetComponentInChildren<TextMeshProUGUI>().text = entity.abilityList[0].abilityData.name;
            button_ability_two.GetComponentInChildren<TextMeshProUGUI>().text = entity.abilityList[1].abilityData.name;
            button_ability_three.GetComponentInChildren<TextMeshProUGUI>().text = entity.abilityList[2].abilityData.name;
            button_ability_four.GetComponentInChildren<TextMeshProUGUI>().text = entity.abilityList[3].abilityData.name;
        }
        

        foreach (Ability a in entity.abilityList) {
            if(a.abilityData.type == AbilityType.passive) {
                entity.healthMax = 100 + (a.abilityData.health);
                entity.manaMax = 100 + (a.abilityData.mana);
                entity.speedMax = 50 + (a.abilityData.speed);
                entity.dodgeMax = 20 + (a.abilityData.dodge);
                entity.critMax = (a.abilityData.crit);
                entity.attackMax = 5 + (a.abilityData.attack);
            }
        }

        entity.ResetStatsToMax();
    }
    public void UseAbility(Ability a) {
        //when we use an ability, check speed differences
        if(player.speed >= enemy.speed) {
            //player will go first in turn order
            //AbilityManager.instance.UseAbility(a.abilityData.name, a.abilityData.type == AbilityType.attack ? enemy : player);
            //enemy.UseRandomAbility();

            StartCoroutine(TurnOne(player, a));

        } else {
            //enemy will go first in turn order
           // enemy.UseRandomAbility();
            //AbilityManager.instance.UseAbility(a.abilityData.name, a.abilityData.type == AbilityType.attack ? enemy : player);

            StartCoroutine(TurnOne(enemy, a));
        }


    }


    public IEnumerator TurnOne(Entity entity, Ability a) {

        Entity opponent;
        var i = 0f;
        float speed = 0.2f;
        if (entity == player) {

            if(a.abilityData.type == AbilityType.attack || a.abilityData.targetOpponent == "TRUE") {
                AbilityManager.instance.UseAbility(a.abilityData.name, enemy);
                SetAnimation(playerObj, AnimalPrefabBuilder.AnimationType.Attack);
            } else if(a.abilityData.type == AbilityType.support || a.abilityData.targetOpponent == "FALSE") {
                AbilityManager.instance.UseAbility(a.abilityData.name, player);
                SetAnimation(playerObj, AnimalPrefabBuilder.AnimationType.Support);
            }

            //AbilityManager.instance.UseAbility(a.abilityData.name, a.abilityData.type == AbilityType.attack ? enemy : player);

            combatText.text = player.animalName + " used " + a.abilityData.name + "!";

            opponent = enemy;
        }
        else {
            var enemyAbility = enemy.GetRandomAbility();
            if (enemyAbility != null) {
                if (enemyAbility.abilityData.type == AbilityType.attack || enemyAbility.abilityData.targetOpponent == "TRUE") {
                    AbilityManager.instance.UseAbility(enemyAbility.abilityData.name, player);
                    SetAnimation(enemyObj, AnimalPrefabBuilder.AnimationType.InverseAttack);
                }
                else if (enemyAbility.abilityData.type == AbilityType.support || enemyAbility.abilityData.targetOpponent == "FALSE") {
                    AbilityManager.instance.UseAbility(enemyAbility.abilityData.name, enemy);
                    SetAnimation(enemyObj, AnimalPrefabBuilder.AnimationType.Support);
                }
            }
            else {
                AbilityManager.instance.UseAbility("BasicBash", player);
            }

            combatText.text = enemy.animalName + " used " + enemyAbility.abilityData.name + "!";

            opponent = player;
        }

        yield return new WaitForSeconds(1F);
        //while(i < 1f) {
        //    i += Time.deltaTime * speed;
        //    yield return null;
        //}

        if(player.health <= 0) {
            //player died
            
        }

        if(enemy.health <= 0) {
            //enemy died
            
        }


        StartCoroutine(TurnTwo(opponent, a));
    }

    public IEnumerator TurnTwo(Entity entity, Ability a) {
        var i = 0f;
        float speed = 0.2f;
        if (entity == player) {

            if (a.abilityData.type == AbilityType.attack || a.abilityData.targetOpponent == "TRUE") {
                AbilityManager.instance.UseAbility(a.abilityData.name, enemy);
                SetAnimation(playerObj, AnimalPrefabBuilder.AnimationType.Attack);
            }
            else if (a.abilityData.type == AbilityType.support || a.abilityData.targetOpponent == "FALSE") {
                AbilityManager.instance.UseAbility(a.abilityData.name, player);
                SetAnimation(playerObj, AnimalPrefabBuilder.AnimationType.Support);
            }

            //AbilityManager.instance.UseAbility(a.abilityData.name, a.abilityData.type == AbilityType.attack ? enemy : player);


            combatText.text = player.animalName + " used " + a.abilityData.name + "!";

        }
        else {
            var enemyAbility = enemy.GetRandomAbility();
            if(enemyAbility != null) {
                if (enemyAbility.abilityData.type == AbilityType.attack || enemyAbility.abilityData.targetOpponent == "TRUE") {
                    AbilityManager.instance.UseAbility(enemyAbility.abilityData.name, player);
                    SetAnimation(enemyObj, AnimalPrefabBuilder.AnimationType.InverseAttack);
                }
                else if (enemyAbility.abilityData.type == AbilityType.support || enemyAbility.abilityData.targetOpponent == "FALSE") {
                    AbilityManager.instance.UseAbility(enemyAbility.abilityData.name, enemy);
                    SetAnimation(enemyObj, AnimalPrefabBuilder.AnimationType.Support);
                }
            } else {
                AbilityManager.instance.UseAbility("BasicBash", player);
            }
            
            combatText.text = enemy.animalName + " used " + enemyAbility.abilityData.name + "!";

        }



        while (i < 1f) {
            i += Time.deltaTime * speed;
            yield return null;
        }

        if (player.health <= 0) {
            //player died

        }

        if (enemy.health <= 0) {
            //enemy died

        }
    }

    void SetAnimation(AnimalPrefabBuilder animal, AnimalPrefabBuilder.AnimationType anim)
    {
        animal.SetAnimationState(anim);
    }
}
