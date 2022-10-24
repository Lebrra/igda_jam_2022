using UnityEngine;
using BeauRoutine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static string SAVE_NAME = "saveGame";
    public static string DEFAULT_INVENTORY = "alligator_body,False,alligator_head,False,alligator_legs,False,alligator_tail,False,badger_body,False,badger_head,False,badger_legs,False,badger_tail,False,beetle_body,False,beetle_head,False,beetle_legs,False,booby_body,False,booby_head,False,booby_legs,False,booby_tail,False,cardinal_body,False,cardinal_head,False,cardinal_legs,False,cardinal_tail,False,cat_body,True,cat_head,True,cat_legs,True,cat_tail,True,clownfish_body,False,clownfish_head,False,clownfish_legs,False,clownfish_tail,False,dog_body,True,dog_head,True,dog_legs,True,dog_tail,True,elephant_body,False,elephant_head,False,elephant_legs,False,elephant_tail,False,fox_body,False,fox_head,False,fox_legs,False,fox_tail,False,frog_body,False,frog_head,False,frog_legs,False,goat_body,False,goat_head,False,goat_legs,False,goat_tail,False,hawk_body,False,hawk_head,False,hawk_legs,False,hawk_tail,False,hornet_body,False,hornet_head,False,hornet_legs,False,hornet_tail,False,hyena_body,False,hyena_head,False,hyena_legs,False,hyena_tail,False,isopod_body,False,isopod_head,False,isopod_legs,False,isopod_tail,False,lemur_body,False,lemur_head,False,lemur_legs,False,lemur_tail,False,lion_body,False,lion_head,False,lion_legs,False,lion_tail,False,manatee_body,False,manatee_head,False,manatee_legs,False,manatee_tail,False,moose_body,False,moose_head,False,moose_legs,False,moose_tail,False,phoenix_body,False,phoenix_head,False,phoenix_legs,False,phoenix_tail,False,polarbear_body,False,polarbear_head,False,polarbear_legs,False,polarbear_tail,False,ram_body,False,ram_head,False,ram_legs,False,ram_tail,False,raptor_body,False,raptor_head,False,raptor_legs,False,raptor_tail,False,redpanda_body,False,redpanda_head,False,redpanda_legs,False,redpanda_tail,False,sealion_body,False,sealion_head,False,sealion_legs,False,sealion_tail,False,shark_body,False,shark_head,False,shark_legs,False,shark_tail,False,snowleopard_body,False,snowleopard_head,False,snowleopard_legs,False,snowleopard_tail,False,spinosarus_body,False,spinosarus_head,False,spinosarus_legs,False,spinosarus_tail,False,tapejara_body,False,tapejara_head,False,tapejara_legs,False,tapejara_tail,False,tapir_body,False,tapir_head,False,tapir_legs,False,tapir_tail,False,turkey_body,False,turkey_head,False,turkey_legs,False,turkey_tail,False,turtle_body,False,turtle_head,False,turtle_legs,False,turtle_tail,False,unicorn_body,False,unicorn_head,False,unicorn_legs,False,unicorn_tail,False,wyvern_body,False,wyvern_head,False,wyvern_legs,False,wyvern_tail,False,";

    public SaveData playerdata;

    public GenericPopupLogic GeneralPopup;
    [SerializeField]
    Animator LoadingScreen;
    Routine loading;

    void Awake()
    {
        if (instance) Destroy(instance);
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        loading.Replace(LoadInitialData());
    }

    public static void SaveData()
    {
        JSONEditor.DataToJSON(instance.playerdata, SAVE_NAME);
        Debug.Log("PLAYER DATA SAVED");
    }

    #region Menu Transitions

    public static void ToMainMenu()
    {
        instance.loading.Replace(LoadMainMenu());
        
    }

    public static void ToLevel()
    {

    }

    static IEnumerator LoadMainMenu()
    {
        yield return GameDirector.instance.LoadingStuff();
        yield return 0.7F;
        GameDirector.instance.OpenMainMenu();
    }

    static IEnumerator LoadInitialData()
    {
        SetLoadingScreen(true);

        Debug.Log("Loading all game data...");
        yield return DataManager.instance.LoadAllData();


        Debug.Log("Loading inventory...");
        if (JSONEditor.DoesFileExist(SAVE_NAME)) instance.playerdata = JSONEditor.JSONToData<SaveData>(SAVE_NAME);
        else
        {
            instance.playerdata = new SaveData();
            instance.playerdata.inventoryStr = DEFAULT_INVENTORY;
        }
        yield return GameDirector.instance.LoadingStuff();

        SetLoadingScreen(false);
        ToTitle();
    }

    public static void ToTitle(bool delayed = false)
    {
        GameDirector.instance.CloseMainMenu();
        if (delayed) instance.loading.Replace(DelayedLoadTitle());
        else GameDirector.instance.OpenTitle();
    }

    static IEnumerator DelayedLoadTitle()
    {
        yield return 0.8F;
        GameDirector.instance.OpenTitle();
    }

    public static void SetLoadingScreen(bool enabled)
    {
        //instance.LoadingScreen.gameObject.SetActive(enabled);
        instance.LoadingScreen.SetBool("Status", enabled);
    }

    #endregion
}
