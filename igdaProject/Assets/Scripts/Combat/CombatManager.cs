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
    bool isLevelBattle = false;

    [Header("Combat Preview")]
    [SerializeField] Animator previewAnim;
    [SerializeField] Button battleButton;
    [SerializeField] Button backButton;
    [SerializeField] AnimalPrefabBuilder enemyObjPreview;
    [SerializeField] AnimalPrefabBuilder playerObjPreview;
    [Header("Stats")]
    [SerializeField] TextMeshProUGUI healthStat;
    [SerializeField] TextMeshProUGUI manaStat;
    [SerializeField] TextMeshProUGUI speedStat;
    [SerializeField] TextMeshProUGUI dodgeStat;


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
    [SerializeField] GameObject clickBlocker;
    [SerializeField] GenericPopupLogic itemPopup;
    [SerializeField] Image itemImage;
    [SerializeField] Ability bash;
    [SerializeField] Ability rest;

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
        if (randomize)
        {
            enemy.RandomizeBuild();
            isLevelBattle = false;
        }
        else
        {
            enemy.BuildAnimal(opponent);
            isLevelBattle = true;
        }
        previewAnim.SetBool("Status", true);
        playerObjPreview.CreateAnimal(GameManager.instance.playerdata.GetActiveAnimal(), true, true, AnimalPrefabBuilder.AnimationType.Bob);
        enemyObjPreview.CreateAnimal(enemy.animal, true, true, AnimalPrefabBuilder.AnimationType.Bob);
        SetStats(GameManager.instance.playerdata.GetAllAbilities().ToArray());
    }

    void SetStats(Ability[] abilities)
    {
        int health = GameManager.BASE_HEALTH;
        int mana = GameManager.BASE_MANA;
        int speed = GameManager.BASE_SPEED;
        int dodge = GameManager.BASE_DODGE;
        foreach (var ability in abilities)
        {
            health += ability.abilityData.health;
            mana += ability.abilityData.mana;
            speed += ability.abilityData.speed;
            dodge += ability.abilityData.dodge;
        }
        healthStat.text = health.ToString();
        manaStat.text = mana.ToString();
        speedStat.text = speed.ToString();
        dodgeStat.text = dodge.ToString();
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
        clickBlocker.SetActive(false);
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

        ability_rest.onClick.AddListener(() => UseAbility(rest)); //AbilityManager.instance.UseAbility("Rest", player));
        ability_basicBash.onClick.AddListener(() => UseAbility(bash)); //AbilityManager.instance.UseAbility("BasicBash", enemy));
        button_ability_one.onClick.AddListener(() => UseAbility(player.abilityList[0]));
        button_ability_two.onClick.AddListener(() => UseAbility(player.abilityList[1]));
        button_ability_three.onClick.AddListener(() => UseAbility(player.abilityList[2]));
        button_ability_four.onClick.AddListener(() => UseAbility(player.abilityList[3]));

        combatText.text = "";

    }

    /// <summary>
    /// When a pet is dealt damage, this will change the UI of their healthbar.
    /// </summary>
    public void DealtDamage(Entity entity, float amount, bool crit, bool dodged) {
        if (crit) amount *= 1.5F;
        if (dodged) amount = 0F;

        if(entity == player) {

            StartCoroutine(UIHealthbarScaler(entity, playerHealthBar, player.health, amount));
        } else {

         
            StartCoroutine(UIHealthbarScaler(entity, enemyHealthBar, enemy.health, amount));
        }
        if (crit || dodged) StartCoroutine(CritOrDodgeText(entity, crit, dodged));
    }

    IEnumerator CritOrDodgeText(Entity entity, bool crit, bool dodged)
    {
        yield return new WaitForSeconds(0.8F);
        if (dodged)
        {
            combatText.text = entity.animalName + " has dodged the attack!";
        }
        else if (crit)
        {
            string name;
            if (entity == player) name = enemy.animalName;
            else name = player.animalName;
            combatText.text = name + " has crit for extra damage!";
        }
    }

    public void ShowSupportText(string message)
    {
        StartCoroutine(SupportText(message));
    }

    IEnumerator SupportText(string message)
    {
        yield return new WaitForSeconds(0.8F);
        combatText.text = message;
    }

    public void SpentMana(Entity entity, float amount) {
        if (entity == player) {

        
            StartCoroutine(UIManabarScaler(playerManaBar, player.mana, -amount));
        }
        else {
            entity.AffectMana(-amount);
           
            //StartCoroutine(UIManabarScaler(enemyManaBar, enemy.mana, amount));
        }
    }

    public void GainMana(Entity entity, float amount)
    {
        if (entity == player)
        {
            StartCoroutine(UIManabarScaler(playerManaBar, player.mana, amount));
        }
        else
        {
            entity.AffectMana(amount);
        }
    }

    private IEnumerator UIHealthbarScaler(Entity e, Image healthBar, float currentHealth, float amount) {
        var i = 0f;
        float speed = 4f;
        float targetHealth = currentHealth - amount;
        while(i < 1) {
            i += Time.deltaTime * speed;
            healthBar.fillAmount = Mathf.Lerp((currentHealth / e.healthMax), (targetHealth / e.healthMax), i);
            yield return null;
        }
        healthBar.fillAmount = targetHealth / e.healthMax;
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
        float targetMana = Mathf.Clamp(currentMana + amount, 0, player.manaMax);
        while (i < 1) {
            i += Time.deltaTime * speed;
            manaBar.fillAmount = Mathf.Lerp((currentMana / player.manaMax), (targetMana / player.manaMax), i);
            yield return null;
        }
        manaBar.fillAmount = targetMana / player.manaMax;
        player.AffectMana(amount);
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
            if (entity.abilityList[0].abilityData.type == AbilityType.passive) button_ability_one.interactable = false;
            else button_ability_one.interactable = true;
            button_ability_two.GetComponentInChildren<TextMeshProUGUI>().text = entity.abilityList[1].abilityData.name;
            if (entity.abilityList[1].abilityData.type == AbilityType.passive) button_ability_two.interactable = false;
            else button_ability_two.interactable = true;
            button_ability_three.GetComponentInChildren<TextMeshProUGUI>().text = entity.abilityList[2].abilityData.name;
            if (entity.abilityList[2].abilityData.type == AbilityType.passive) button_ability_three.interactable = false;
            else button_ability_three.interactable = true;
            button_ability_four.GetComponentInChildren<TextMeshProUGUI>().text = entity.abilityList[3].abilityData.name;
            if (entity.abilityList[3].abilityData.type == AbilityType.passive) button_ability_four.interactable = false;
            else button_ability_four.interactable = true;
        }
        

        foreach (Ability a in entity.abilityList) {
            if(a.abilityData.type == AbilityType.passive) {
                entity.healthMax += (a.abilityData.health);
                entity.manaMax += (a.abilityData.mana);
                entity.speedMax += (a.abilityData.speed);
                entity.dodgeMax += (a.abilityData.dodge);
                entity.critMax += (a.abilityData.crit);
                entity.attackMax += (a.abilityData.attack);
            }
        }

        entity.ResetStatsToMax();
        ResetBars();
    }

    void ResetBars()
    {
        enemyHealthBar.fillAmount = 1F;
        playerHealthBar.fillAmount = 1F;
        enemyHealthBar.color = new Color32(104, 191, 100, 255);
        playerHealthBar.color = new Color32(104, 191, 100, 255);
        playerManaBar.fillAmount = 1F;
        //enemyManaBar.fillAmount = 1F;
    }

    public void UseAbility(Ability a) {
        // first, check mana
        if (player.mana < a.abilityData.abilityCost)
        {
            combatText.text = "Not enough mana!  (Use Rest to gain mana)";
            return;
        }

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

        clickBlocker.SetActive(true);
        Entity opponent;
        var i = 0f;
        float speed = 0.2f;
        if (entity == player) {

            if(a.abilityData.type == AbilityType.attack || a.abilityData.targetOpponent == "TRUE") {
                AbilityManager.instance.UseAbility(a, enemy, player);
                SetAnimation(playerObj, AnimalPrefabBuilder.AnimationType.Attack);
                AudioManager.audioManager.playSoundClip("Attack", 1F);
            } else if(a.abilityData.type == AbilityType.support || a.abilityData.targetOpponent == "FALSE") {
                AbilityManager.instance.UseAbility(a, player);
                SetAnimation(playerObj, AnimalPrefabBuilder.AnimationType.Support);
                AudioManager.audioManager.playSoundClip("Support", 0.3F);
            }

            //AbilityManager.instance.UseAbility(a.abilityData.name, a.abilityData.type == AbilityType.attack ? enemy : player);
            SpentMana(player, a.abilityData.abilityCost);
            combatText.text = player.animalName + " used " + a.abilityData.name + "!";

            opponent = enemy;
        }
        else {
            var enemyAbility = enemy.GetRandomAbility(bash, rest);
            if (enemyAbility != null) {
                if (enemyAbility.abilityData.type == AbilityType.attack || enemyAbility.abilityData.targetOpponent == "TRUE") {
                    AbilityManager.instance.UseAbility(enemyAbility, player, enemy);
                    SetAnimation(enemyObj, AnimalPrefabBuilder.AnimationType.InverseAttack);
                    AudioManager.audioManager.playSoundClip("Attack", 0.5F);
                }
                else if (enemyAbility.abilityData.type == AbilityType.support || enemyAbility.abilityData.targetOpponent == "FALSE") {
                    AbilityManager.instance.UseAbility(enemyAbility, enemy);
                    SetAnimation(enemyObj, AnimalPrefabBuilder.AnimationType.Support);
                    AudioManager.audioManager.playSoundClip("Attack", 0.15F);
                }
            }
            else {
                AbilityManager.instance.UseAbility(bash, player);
                SetAnimation(enemyObj, AnimalPrefabBuilder.AnimationType.InverseAttack);
                AudioManager.audioManager.playSoundClip("Attack", 0.5F);

            }
            SpentMana(enemy, enemyAbility.abilityData.abilityCost);
            combatText.text = enemy.animalName + " used " + enemyAbility.abilityData.name + "!";

            opponent = player;
        }

        yield return new WaitForSeconds(2F);
        //while(i < 1f) {
        //    i += Time.deltaTime * speed;
        //    yield return null;
        //}

        if(player.health <= 0) {
            //player died
            EndBattle(false, isLevelBattle);
            yield break;
        }

        if(enemy.health <= 0) {
            //enemy died
            EndBattle(true, isLevelBattle);
            yield break;
        }


        StartCoroutine(TurnTwo(opponent, a));
    }

    public IEnumerator TurnTwo(Entity entity, Ability a) {
        var i = 0f;
        float speed = 0.2f;
        if (entity == player) {

            if (a.abilityData.type == AbilityType.attack || a.abilityData.targetOpponent == "TRUE") {
                AbilityManager.instance.UseAbility(a, enemy, player);
                SetAnimation(playerObj, AnimalPrefabBuilder.AnimationType.Attack);
                AudioManager.audioManager.playSoundClip("Attack", 1);
            }
            else if (a.abilityData.type == AbilityType.support || a.abilityData.targetOpponent == "FALSE") {
                AbilityManager.instance.UseAbility(a, player);
                SetAnimation(playerObj, AnimalPrefabBuilder.AnimationType.Support);
                AudioManager.audioManager.playSoundClip("Attack", 0.3F);
            }

            //AbilityManager.instance.UseAbility(a.abilityData.name, a.abilityData.type == AbilityType.attack ? enemy : player);

            SpentMana(player, a.abilityData.abilityCost);
            combatText.text = player.animalName + " used " + a.abilityData.name + "!";

        }
        else {
            var enemyAbility = enemy.GetRandomAbility(bash, rest);
            if(enemyAbility != null) {
                if (enemyAbility.abilityData.type == AbilityType.attack || enemyAbility.abilityData.targetOpponent == "TRUE") {
                    AbilityManager.instance.UseAbility(enemyAbility, player, enemy);
                    SetAnimation(enemyObj, AnimalPrefabBuilder.AnimationType.InverseAttack);
                    AudioManager.audioManager.playSoundClip("Attack", 0.5F);
                }
                else if (enemyAbility.abilityData.type == AbilityType.support || enemyAbility.abilityData.targetOpponent == "FALSE") {
                    AbilityManager.instance.UseAbility(enemyAbility, enemy);
                    SetAnimation(enemyObj, AnimalPrefabBuilder.AnimationType.Support);
                    AudioManager.audioManager.playSoundClip("Attack", 0.15F);
                }
            } else {
                AbilityManager.instance.UseAbility(bash, player);
                SetAnimation(enemyObj, AnimalPrefabBuilder.AnimationType.InverseAttack);
                AudioManager.audioManager.playSoundClip("Attack", 0.5F);
            }
            SpentMana(enemy, enemyAbility.abilityData.abilityCost);

            combatText.text = enemy.animalName + " used " + enemyAbility.abilityData.name + "!";

        }


        yield return new WaitForSeconds(2F);
        //while (i < 1f) {
        //    i += Time.deltaTime * speed;
        //    yield return null;
        //}

        if (player.health <= 0) {
            //player died
            EndBattle(false, isLevelBattle);
            yield break;
        }

        if (enemy.health <= 0) {
            //enemy died
            EndBattle(true, isLevelBattle);
            yield break;
        }
        clickBlocker.SetActive(false);
    }

    void SetAnimation(AnimalPrefabBuilder animal, AnimalPrefabBuilder.AnimationType anim)
    {
        animal.SetAnimationState(anim);
    }

    void EndBattle(bool win, bool isLevel)
    {
        if (isLevel)
        {
            if (win)
            {
                string newPart = GetRandomPart(enemyObj.GetCreatedAnimal());
                if (newPart == "none")
                {
                    // cannot gain a part
                    var popup = GameManager.instance.GeneralPopup;
                    popup.FillContent("Victory!\nNo new parts gained.", () => {
                        GameDirector.instance.CloseCombat();
                        popup.Close();
                        clickBlocker.SetActive(false);
                    });
                    popup.Open();
                }
                else
                {
                    // set up part window
                    itemImage.sprite = DataManager.instance.GetAnimalPart(newPart).partData.image;
                    itemPopup.FillContent($"{newPart.Replace("_", " ")} obtained!", () =>
                    {
                        GameDirector.instance.CloseCombat();
                        itemPopup.Close();
                        clickBlocker.SetActive(false);
                    }, "Sounds Good!");
                    itemPopup.Open();
                    MonsterMakerGenerator.instance.UpdatePetParts(DataManager.instance.GetAnimalPart(newPart));
                }

                GameManager.instance.IncrementProgression();

            }
            else
            {
                var popup = GameManager.instance.GeneralPopup;
                popup.FillContent("You Lost!\n\nCreate a new creature and try again!", () => {
                    GameDirector.instance.CloseCombat();
                    popup.Close();
                    clickBlocker.SetActive(false);
                }, "Darn");
                popup.Open();

                GameManager.instance.playerdata.currentLevel.activeRun = false;
                GameManager.SaveData();
            }
        }
        else
        {
            // use generic popup
            var popup = GameManager.instance.GeneralPopup;
            string message = "You Won!";
            string buttonMessage = "Hooray!";
            if (!win)
            {
                message = "You Lost!";
                buttonMessage = "Oh no!";
            }

            popup.FillContent(message, () => {
                GameDirector.instance.CloseCombat();
                popup.Close();
                clickBlocker.SetActive(false);
            }, buttonMessage);
            popup.Open();
        }
    }

    string GetRandomPart(AnimalPartsObject animal)
    {
        List<string> options = new List<string>();
        options.Add(animal.headID);
        options.Add(animal.bodyID);
        options.Add(animal.legsID);
        options.Add(animal.tailID);

        int choices = 4;
        while (choices > 0)
        {
            var chosen = Random.Range(0, choices);
            if (MonsterMakerGenerator.instance.DoIHaveThis(options[chosen]))
                options.RemoveAt(chosen);
            else return options[chosen];
        }
        return "none";
    }
}
